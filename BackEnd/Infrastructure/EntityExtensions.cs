using System.Linq;

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
                    Id = session?.TrackId ?? 0,
                    Name = session.Track?.Name
                },
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
                EmailAddress = swimmer.EmailAddress,
                Sessions = swimmer.SessionSwimmers?
                    .Select(sa =>
                        new EventsDTO.Session
                        {
                            Id = sa.SessionId,
                            Title = sa.Session.Title,
                            StartTime = sa.Session.StartTime,
                            EndTime = sa.Session.EndTime
                        })
                    .ToList(),
            };
    }
}