using System;
using System.Collections.Generic;
using System.Text;

namespace HomeLibrary.DatabaseModel
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public virtual ItemType ItemType {get;set;}
        public int ItemTypeId { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public int YearPublishment { get; set; }
        public bool Borrowed { get; set; }
        public Guid CoverGuid { get; set; }
    }
}