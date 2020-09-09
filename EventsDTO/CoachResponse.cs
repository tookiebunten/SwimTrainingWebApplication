using System;
using System.Collections.Generic;
using System.Text;

namespace EventsDTO
{
    public class CoachResponse :Coach
    {
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
