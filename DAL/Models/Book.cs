using System;
using System.Collections.Generic;
using DAL.Models.BasePoco;

namespace DAL.Models
{
    public class Book : AuditableEntity
    {
        public string Title { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
