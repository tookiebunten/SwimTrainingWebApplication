using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsDTO
{
        public class Swimmer
        {
            public int Id { get; set; }
            //Swimmer's first name
            [Required]
            [StringLength(50)]
            public string FirstName { get; set; }
            //Swimmer's last name
            [Required]
            [StringLength(50)]
            public string LastName { get; set; }
            //username or full name of Swimmer
            [StringLength(200)]
            public string UserName { get; set; }
            //Swimmer's email address
            [StringLength(100)]
            public string EmailAddress { get; set; }
        }
    }
