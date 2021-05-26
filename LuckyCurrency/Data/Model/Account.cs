using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyCurrency.Data.Model
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
