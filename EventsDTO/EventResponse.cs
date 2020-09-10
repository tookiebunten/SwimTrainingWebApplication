using System;
using System.Collections.Generic;
using System.Text;

namespace EventsDTO
{
    public class EventResponse : Event
    {

        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        public ICollection<Track> Tracks { get; set; } = new List<Track>();

        public ICollection<Coach> Coaches { get; set; } = new List<Coach>();
    }
}
