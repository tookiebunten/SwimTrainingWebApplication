using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class SessionSwimmer
    {
        public int SessionId { get; set; }

        public Session Session { get; set; }

        public int SwimmerId { get; set; }

        public Swimmer Swimmer { get; set; }
    }
}
