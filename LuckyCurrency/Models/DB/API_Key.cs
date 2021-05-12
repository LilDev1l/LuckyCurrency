using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyCurrency.Models.DB
{
    public class API_Key
    {
        [Key]
        [ForeignKey("Account")]
        public int Id { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }

        public Account Account { get; set; }
    }
}
