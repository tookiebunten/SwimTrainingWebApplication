using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class Squad : EventsDTO.Squad
    {
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
