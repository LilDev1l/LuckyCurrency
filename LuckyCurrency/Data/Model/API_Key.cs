using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuckyCurrency.Data.Model
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
