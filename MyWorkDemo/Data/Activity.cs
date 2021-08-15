using MyWorkDemo.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Data
{
    public class Activity
    {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public long Ore { get; set; }
        public DateTime ActivityDate { get; set; }
        public StateEnum State { get; set; }

        public ICollection<ActivityUser> ActivityUsers { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
