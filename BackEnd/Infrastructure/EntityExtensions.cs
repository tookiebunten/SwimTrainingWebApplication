using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static EventsDTO.CoachResponse MapCoachResponse(this Coach coach) =>
            new EventsDTO.CoachResponse
            {
                Id = coach.Id,
                FirstName = coach.FirstName,
                LastName = coach.LastName,
                CoachDetails = coach.CoachDetails,
                Sessions = coach.SessionCoaches?
                    .Select(ss =>
                        new EventsDTO.Session
                        {
                            Id = ss.SessionId,
                            SessionTitle = ss.Session.SessionTitle
                        })
                    .ToList()
            };
    }
}
