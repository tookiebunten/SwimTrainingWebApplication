using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class SessionCoach
    {

        public int SessionId { get; set; }

        public Session Sessions { get; set; }

        public int CoachId { get; set; }

        public Coach Coaches { get; set; }

    }
}
