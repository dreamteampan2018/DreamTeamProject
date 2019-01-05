using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository
{
    public class ItemRepository : IItemRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public ItemRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
      

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DeleteItem(int itemId)
        {
            Item toDelete = context.Items.Find(itemId);
            context.Items.Remove(toDelete);
            SaveAsync();
        }

        public async  Task<Item> GetItemByIdAsync(int itemId)
        {
            return context.Items.Find(itemId);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return context.Items.ToList();
        }

        public Item InsertItem(Item item)
        {
            context.Items.Add(item);
            SaveAsync();
            return item;
        }

        public async Task SaveAsync()
        {
            context.SaveChangesAsync();
        }

        public void UpdateItem(Item item)
        {
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public List<Item> Search(int typeId, string title, string author)
        {
            var query = from items in context.Items
                        where
                        (
                            items.Author.Name == author ||
                            items.Title == title ||
                            items.Author.Surname == author ||
                            items.ItemType.ItemTypeId == typeId
                        )
                        select items;

            return query.ToList();
        }

        public async Task SetAsBorrowed(string borowedPerson,Item item,int userId)
        {
            Statistics statistics = new Statistics();
            statistics.ItemId = item.ItemId;
            statistics.BorrowedPerson = borowedPerson;
            statistics.BorrowedDate = DateTime.Now;
            item.Borrowed = true;
            UpdateItem(item);
            context.Statistics.Add(statistics);
            context.SaveChanges();
        }

        public async Task SetAsReturned(Item item)
        {
            item.Borrowed = false;
            UpdateItem(item);
            Statistics toChange = context.Statistics.FirstOrDefault(x => x.ItemId == item.ItemId && x.ReturnDate == null);
            toChange.ReturnDate = DateTime.Now;
       }


    }
}
