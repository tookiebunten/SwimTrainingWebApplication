using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static EventsDTO.CoachResponse MapCoachResponse(this Coach coach) =>
            new EventsDTO.CoachResponse
            {
                Id = coach.Id,
                Name = coach.Name,
                Bio = coach.Bio,
                WebSite = coach.WebSite,
                Sessions = coach.SessionCoaches?
                    .Select(ss =>
                        new EventsDTO.Session
                        {
                            Id = ss.SessionId,
                            Title = ss.Session.Title
                        })
                    .ToList()
            };
    }
}
