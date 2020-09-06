using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

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
        public static EventsDTO.SwimmerResponse MapSwimmerResponse(this Swimmer swimmer) =>
            new EventsDTO.SwimmerResponse
            {
                Id = swimmer.Id,
                FirstName = swimmer.FirstName,
                LastName = swimmer.LastName,
                UserName = swimmer.UserName,
                Sessions = swimmer.SessionsSwimmers?
                    .Select(sa =>
                        new EventsDTO.Session
                        {
                            Id = sa.SessionId,
                            SessionTitle = sa.Session.SessionTitle,
                            StartTime = sa.Session.StartTime,
                            EndTime = sa.Session.EndTime
                        })
                    .ToList()
            };

        public static EventsDTO.SessionResponse MapSessionResponse(this Session session) =>
            new EventsDTO.SessionResponse
            {
                Id = session.Id,
                SessionTitle = session.SessionTitle,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Coaches = session.SessionCoaches?
                                  .Select(ss => new EventsDTO.Coach
                                  {
                                      Id = ss.CoachId,
                                      FirstName = ss.Coaches.FirstName,
                                      LastName = ss.Coaches.LastName
                                  })
                                   .ToList(),
                SquadId = session.SquadId,
                Squad = new EventsDTO.Squad
                {
                    Id = session?.SquadId ?? 0,
                    Name = session.Squads?.Name

                },
                SessionDescription = session.SessionDescription
            };

    }
}
