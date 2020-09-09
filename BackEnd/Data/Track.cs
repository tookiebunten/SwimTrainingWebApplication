using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class Track : EventsDTO.Track
    {
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
