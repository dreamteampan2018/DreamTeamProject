using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.HelperClass;
using HomeLibrary.WebApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository
{
    public class StatisticRepository : IStatisticRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public StatisticRepository(ApplicationDbContext context)
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

        public int GetAuthorsCount()
        {
            return context.Authors.Count();
        }

        public int GetItemsCount(int typeID)
        {

            int ilosc = context.Items.Where(x => x.ItemTypeId == typeID).Count();
            return ilosc;
        }

        public int GetBorrowedCountItem(int itemID)
        {
            int returnValue = 0;
            try
            {

                returnValue = context.Statistics.Where(x => x.ItemId == itemID && x.ReturnDate != Convert.ToDateTime("0001-01-01")).Count();
            }
            catch (Exception ex)
            {
                string expMessage = ex.Message;

            }
            return returnValue;
        }


        public List<MostTypeStatistics> GetMostTypeStatistics()
        {
            var listItems = from k in context.Statistics
                            join i in context.Items on k.ItemId equals i.ItemId
                            select i;

            var cos =
                    from p in listItems
                    group p by p.ItemTypeId into g
                    select new
                    {
                        name = g.Key,
                        count = g.Count()
                    };

            List<MostTypeStatistics> tempList = new List<MostTypeStatistics>();

            foreach (var i in cos)
            {
                tempList.Add(new MostTypeStatistics
                {
                    TypeCount = i.count,
                    TypeName = context.ItemTypes.FirstOrDefault(x => x.ItemTypeId == i.name).Name

                }

                    );
            }
            return tempList;

        }

        /*public int GetBorrowedItemsMonthCount()
         {

         }*/
    }
}