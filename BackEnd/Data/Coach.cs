using System;
using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Coach : EventsDTO.Coach
    {
        public virtual ICollection<SessionCoach> SessionCoaches { get; set; } = new List<SessionCoach>();

    }
}
