using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.HelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository
{
    public class StatisticRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;
        
        public StatisticRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public StatisticRepository()
        {

        }

        public int GetAuthorsCount()
        {
            return context.Authors.Count();
        }

        public int GetItemsCount(int typeID)
        {
            return context.Items.Select(it => it.ItemTypeId == typeID).Count();
        }

        public int GetBorowedCountItem(int itemId)
        {
            return context.Statistics.Select(x => x.ItemId == itemId && x.ReturnDate>=Convert.ToDateTime("0001-01-01")).Count();
        }

        public int GetMostPopularTitle()
        {
            return 0;
           // return context.Statistics.GroupBy(g => g.ItemId).OrderBy(o => o.Key).SelectMany().ToList();
        }
 
    }
}
