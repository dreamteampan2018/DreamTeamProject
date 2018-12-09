using HomeLibrary.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository.Interface
{
    public interface IItemRepository:IDisposable
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemByIdAsync(int itemId);
        Item InsertItem(Item item);
        void DeleteItem(int itemId);
        void UpdateItem(Item item);
        Task SaveAsync();
    }
}
