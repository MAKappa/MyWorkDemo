using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Data
{
    public class Note
    {
        public long id { get; set; }
        public string Descrizione { get; set; }
        public DateTime NoteEntryDate { get; set; }
        public long ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
