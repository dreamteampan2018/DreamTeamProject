using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HomeLibrary.DatabaseModel
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public virtual ItemType ItemType { get; set; }
        public int ItemTypeId { get; set; }
        public virtual Author Author { get; set; }
        public int AuthorId { get; set; }
        public string Description { get; set; }
        public int YearPublishment { get; set; }
        public bool Borrowed { get; set; }
        public string CoverGuid { get; set; }
        [NotMapped]
        private string CreatorDescription { get; set; }
        [NotMapped]
        public string BorrowedPerson { get; set; }
        public Item(string creatorDescription)
        {
            this.CreatorDescription = creatorDescription;
        }
        public Item()
        {
            this.CreatorDescription = string.Empty;
        }
    }
}