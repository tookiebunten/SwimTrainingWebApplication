using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventsDTO
{
    public class Session
    {
        public int Id { get; set; }
        //sessions needs a title - like Training or Competition 
        [Required]
        [StringLength(200)]
        public string SessionTitle { get; set; }
        //session description to add detail of training or competition
        [StringLength(4000)]
        public virtual string SessionDescription { get; set; }
        //so we know when the training or the competion starts
        public virtual DateTimeOffset? StartTime { get; set; }
        //so we know when the training or the competion ends
        public virtual DateTimeOffset? EndTime { get; set; }

        // Duration for working out total time spent training or competing 
        public TimeSpan Duration => EndTime?.Subtract(StartTime ?? EndTime ?? DateTimeOffset.MinValue) ?? TimeSpan.Zero;

        public int? SquadId { get; set; }

    }
        
}
