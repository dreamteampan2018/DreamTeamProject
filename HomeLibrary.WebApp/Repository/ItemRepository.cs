using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return context.Items.Find(itemId);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return context.Items.Include(item=>item.Author).Include(item=>item.ItemType).ToList();
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
            context.SaveChanges();
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

        public List<Item> TitleSearch(string titlePostion)
        {
            StringBuilder titlePos = new StringBuilder();
            titlePos.Append("%");
            titlePos.Append(titlePostion);
            titlePos.Append("%");
            var query = from i in context.Items
                        where EF.Functions.Like(i.Title, titlePos.ToString())
                        select i;


            return query.Include(item => item.Author).Include(item => item.ItemType).ToList();
        }

        public List<Item> AuthorSearch(string authorName)
        {
            StringBuilder titlePos = new StringBuilder();
            titlePos.Append("%");
            titlePos.Append(authorName);
            titlePos.Append("%");
            var query = from i in context.Items
                        where EF.Functions.Like(i.Author.Name, titlePos.ToString()) ||
                        EF.Functions.Like(i.Author.Surname, titlePos.ToString())
                        
                        select i;

            return query.Include(item => item.Author).Include(item => item.ItemType).ToList();
        }

        public List<Item> TypeSearch (int typeId)
        {
            return context.Items.Where(it => it.ItemTypeId == typeId).Include(item => item.Author).Include(item => item.ItemType).ToList();
           
        }

        public async Task SetAsBorrowed(string borowedPerson, Item item, string userName)
        {
            Statistics statistics = new Statistics();
            statistics.ItemId = item.ItemId;
            statistics.BorrowedPerson = borowedPerson;
            statistics.BorrowedDate = DateTime.Now;
            statistics.ChangedUser = userName;
            context.Statistics.Add(statistics);

            Item toUpdate = await GetItemByIdAsync(item.ItemId);
            toUpdate.Borrowed = true;
            toUpdate.BorrowedPerson = borowedPerson;
            UpdateItem(toUpdate);
           
        }

        public async Task SetAsReturned(Item item)
        {
            Item toUpdate = await GetItemByIdAsync(item.ItemId);
            toUpdate.Borrowed = false;
            toUpdate.BorrowedPerson = string.Empty;
            UpdateItem(toUpdate);
            context.SaveChanges();
            Statistics toChange = context.Statistics.FirstOrDefault(x => x.ItemId == item.ItemId && x.ReturnDate == Convert.ToDateTime("01.01.0001"));
            toChange.ReturnDate = DateTime.Now;
            context.SaveChanges();
        }




    }
}