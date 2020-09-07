using System.ComponentModel.DataAnnotations;

namespace EventsDTO
{
    public class Coach
    {
        public int Id { get; set; }
        //Coach's name
        [Required]
        [StringLength (200)]
        public string CoachName { get; set; }
        /*
        //Coach's first name
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        //Coach's last name
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        //username or full name of coach
        [StringLength(200)]
        public string UserName { get; set; }*/
        //Details of the coach
        [StringLength(2000)]
        public string CoachDetails { get; set; }
    }
}
