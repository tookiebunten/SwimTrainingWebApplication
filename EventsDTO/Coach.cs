using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsDTO
{
    public class Coach
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string FirstName { get; set; }

        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(4000)]
        public string Bio { get; set; }

        [StringLength(1000)]
        public virtual string WebSite { get; set; }

    }
}
