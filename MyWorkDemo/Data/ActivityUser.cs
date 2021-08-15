using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Data
{
    public class ActivityUser
    {
        [Key]
        public long Id { get; set; }
        public long ActivityID { get; set; }
        public long UserID { get; set; }
        public Activity Activity { get; set; }
        public User User { get; set; }
    }
}
