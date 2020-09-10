using System;
using System.Collections.Generic;
using System.Text;

namespace EventsDTO
{
    public class TrackResponse : Track
    {

        public Event Event { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
