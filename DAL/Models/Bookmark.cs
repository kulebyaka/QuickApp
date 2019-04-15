using System;
using DAL.Models.BasePoco;

namespace DAL.Models
{
    public class Bookmark : AuditableEntity
    {
        public string Value { get; set; }
        public DateTime ImportDate { get; set; }
        public Book Book { get; set; }
    }
}
