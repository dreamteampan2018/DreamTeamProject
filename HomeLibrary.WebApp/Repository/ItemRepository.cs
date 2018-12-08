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
        }

        public Item GetItemById(int itemId)
        {
            return context.Items.Find(itemId);
        }

        public IEnumerable<Item> GetItems()
        {
            return context.Items.ToList();
        }

        public Item InsertItem(Item item)
        {
            context.Items.Add(item);
            return item;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
