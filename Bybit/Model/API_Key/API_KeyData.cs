using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bybit.Model.API_Key
{
    public class API_KeyData
    {
        public string api_key { get; set; }
        public string type { get; set; }
        public int user_id { get; set; }
        public int inviter_id { get; set; }
        public List<string> ips { get; set; }
        public string note { get; set; }
        public List<string> permissions { get; set; }
        public DateTime created_at { get; set; }
        public DateTime expired_at { get; set; }
        public bool read_only { get; set; }
    }
}
