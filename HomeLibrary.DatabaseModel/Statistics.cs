using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLibrary.DatabaseModel
{
    public class Statistics
    {
        public int StatisticsId { get; set; }
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string BorrowedPerson { get; set; }
        public string ChangedUser { get; set; }
       
    }
}
