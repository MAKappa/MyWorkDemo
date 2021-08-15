using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorkDemo.Data
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }

        [InverseProperty("User")]
        public ICollection<ActivityUser> ActivityUsers { get; set; }
    }
}
