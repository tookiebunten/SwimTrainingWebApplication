﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventsDTO
{
    class SessionResponse : Session
    {
        public Squad Squad { get; set; }

        public List<Coach> Coaches { get; set; } = new List<Coach>();
    }
}