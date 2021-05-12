using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models.DB
{
    public class Account
    {
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public API_Key API_Key { get; set; }

        public User User { get; set; }
    }
}
