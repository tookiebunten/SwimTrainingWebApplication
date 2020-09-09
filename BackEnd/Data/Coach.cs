using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class Coach : EventsDTO.Coach
{
    public virtual ICollection<SessionCoach> SessionCoaches { get; set; } = new List<SessionCoach>();
}
