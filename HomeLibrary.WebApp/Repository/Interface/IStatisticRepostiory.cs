using HomeLibrary.WebApp.HelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository.Interface
{
    public interface IStatisticRepository : IDisposable
    {
        int GetAuthorsCount();
        int GetItemsCount(int typeID);
        int GetBorrowedCountItem(int itemID);
        List<MostTypeStatistics> GetMostTypeStatistics();
    }
}