using System;
using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Swimmer : EventsDTO.Swimmer
    {
        public virtual ICollection<SessionSwimmer> SessionsSwimmers { get; set; }
    }
}
