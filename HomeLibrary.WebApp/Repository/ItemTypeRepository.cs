﻿using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public ItemTypeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void DeleteItemType(int itemTypeId)
        {
            ItemType toDelete = context.ItemTypes.Find(itemTypeId);
            context.ItemTypes.Remove(toDelete);
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

        public ItemType GetItemTypeById(int itemTypeId)
        {
            return context.ItemTypes.Find(itemTypeId);
        }

        public IEnumerable<ItemType> GetItemTypes()
        {
            return context.ItemTypes.ToList();
        }

        public void InsertItemType(ItemType itemType)
        {
            context.ItemTypes.Add(itemType);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateItemType(ItemType itemType)
        {
            context.Entry(itemType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}