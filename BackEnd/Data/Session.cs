using System;
using System.Collections;
using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Session : EventsDTO.Session
    {
        public virtual ICollection<SessionCoach> SessionCoaches { get; set; }

        public virtual ICollection<SessionSwimmer> SessionSwimmers { get; set; }

        public Squad Squads { get; set; }
    }
}
