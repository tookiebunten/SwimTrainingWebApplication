using System;
using System.Collections.Generic;
using System.Text;

namespace EventsDTO
{
    public class SwimmerResponse : Swimmer
    {
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
