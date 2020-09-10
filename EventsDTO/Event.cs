using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsDTO
{
    public class Event
    {

        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

    }
}
