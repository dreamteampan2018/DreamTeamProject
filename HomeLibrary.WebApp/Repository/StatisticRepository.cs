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
            context = new ApplicationDbContext(options =>
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-HomeLibrary.WebApp-B931AD64-A8C7-4A94-82D3-B2F74DBD441C;Trusted_Connection=True;MultipleActiveResultSets=true"));
        }

        public int GetAuthorsCount ()
        {
            return context.Authors.Count();
        }

        public int GetItemsCount(int typeID)
        {
            return context.Items.Select(x => x.ItemTypeId == typeID).Count();
        }

        public int GetBorrowedCountItem (int itemID)
        {
            return context.Statistics.Select(x => x.ItemId == itemID && x.ReturnDate !=Convert.ToDateTime ("0001-01-01")).Count();
        }


        public List<MostTypeStatistics> GetMostTypeStatistics()
        {
            var listItems = from k in context.Statistics
                            join i in context.Items on k.ItemId equals i.ItemId
                            select i;

        var cos = listItems.GroupBy(x => x.ItemTypeId);

        List<MostTypeStatistics> tempList = new List<MostTypeStatistics>();

            /*    foreach (var i in cos)
                {
                    tempList.Add(new MostTypeStatistics
                    {
                        TypeCount = i.Count(),
                        TypeName = context.ItemTypes.FirstOrDefault(x => x.ItemTypeId == i.).Name

                    }

                        );
                }*/
            return tempList;

        }

       /* public int GetBorrowedItemsMonthCount()
        {
            
        }*/
    }
}
