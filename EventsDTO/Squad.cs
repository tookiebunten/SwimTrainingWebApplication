using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsDTO
{
    public class Squad
    {

        public int Id { get; set; }
        //Squad needs a name - Development, Junior, Transition, Senior and Masters are the current sqaud names
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

    }
}
