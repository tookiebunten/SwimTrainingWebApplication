using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class Session : EventsDTO.Session
    {

        public virtual ICollection<SessionCoach> SessionCoaches { get; set; }

        public virtual ICollection<SessionSwimmer> SessionSwimmers { get; set; }

        public Track Track { get; set; }

    }
}
