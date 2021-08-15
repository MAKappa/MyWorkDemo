using Microsoft.AspNetCore.Mvc.Rendering;
using MyWorkDemo.Data;
using MyWorkDemo.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Models
{
    public class ActivityDto
    {
        public long Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public long Ore { get; set; }

        public ICollection<Note> Notes { get; set; }
        public StateEnum State { get; set; }

        public DateTime ActivityDate { get; set; }

        public List<SelectListItem> drpUser { get; set; }

        [Display(Name = "Users")]
        public long[] UsersIds { get; set; }
    }
}
