﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class Swimmer : EventsDTO.Swimmer
    {
        public virtual ICollection<SessionSwimmer> SessionSwimmers { get; set; }
    }
}

