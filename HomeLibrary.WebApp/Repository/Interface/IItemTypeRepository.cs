using HomeLibrary.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository.Interface
{
    public interface IItemTypeRepository:IDisposable
    {
        IEnumerable<ItemType> GetItemTypes();
        ItemType GetItemTypeById(int itemTypeId);
        void InsertItemType(ItemType itemType);
        void DeleteItemType(int itemTypeId);
        void UpdateItemType(ItemType itemType);
        void Save();
    }
}
