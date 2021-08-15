using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Models
{
    public class NotesDto
    {
        public long id { get; set; }
        public string Descrizione { get; set; }
        
        public long ActivityId { get; set; }
    }
}
