using BackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public static class EntityExtensions
    {
        public static EventsDTO.SessionResponse MapSessionResponse(this Session session) =>
            new EventsDTO.SessionResponse
            {
                Id = session.Id,
                Title = session.Title,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Tags = session.SessionTags?
                              .Select(st => new EventsDTO.Tag
                              {
                                  Id = st.TagId,
                                  Name = st.Tag.Name
                              })
                               .ToList(),
                Coaches = session.SessionCoaches?
                                  .Select(ss => new EventsDTO.Coach
                                  {
                                      Id = ss.CoachId,
                                      Name = ss.Coach.Name
                                  })
                                   .ToList(),
                TrackId = session.TrackId,
                Track = new EventsDTO.Track
                {
                    TrackId = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                },
                EventId = session.EventId,
                Abstract = session.Abstract
            };

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

        public static EventsDTO.SwimmerResponse MapSwimmerResponse(this Swimmer swimmer) =>
            new EventsDTO.SwimmerResponse
            {
                Id = swimmer.Id,
                FirstName = swimmer.FirstName,
                LastName = swimmer.LastName,
                UserName = swimmer.UserName,
                Sessions = swimmer.Sessions?
                    .Select(s =>
                        new EventsDTO.Session
                        {
                            Id = s.Id,
                            Title = s.Title,
                            StartTime = s.StartTime,
                            EndTime = s.EndTime
                        })
                    .ToList(),
                Event = Swimmer.Eventswimmers?
                    .Select(ca =>
                        new EventsDTO.Event
                        {
                            Id = ca.EventId,
                            Name = ca.Event.Name
                        })
                    .ToList(),
            };
    }
}