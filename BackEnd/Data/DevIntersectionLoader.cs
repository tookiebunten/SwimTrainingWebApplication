using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Data;
using EventsDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd
{
    public class DevIntersectionLoader : DataLoader
    {
        public override async Task LoadDataAsync(Stream fileStream, ApplicationDbContext db)
        {
            var reader = new JsonTextReader(new StreamReader(fileStream));

            var speakerNames = new Dictionary<string, EventsDTO.Coach>();
            var squad = new Dictionary<string, EventsDTO.Squad>();

            JArray doc = await JArray.LoadAsync(reader);

            foreach (JObject item in doc)
            {
                var theseCoaches = new List<EventsDTO.Coach>();
                foreach (var thisCoachName in item["coachNames"])
                {
                    if (!coachNames.ContainsKey(thisCoachName.Value<string>()))
                    {
                        var thisCoach = new EventsDTO.Coach { Name = thisCoachName.Value<string>() };
                        db.Coaches.Add(thisCoach);
                        coachNames.Add(thisCoachName.Value<string>(), thisCoach);
                        Console.WriteLine(thisCoachName.Value<string>());
                    }
                    theseCoaches.Add(coachNames[thisCoachName.Value<string>()]);
                }

                var theseSquads = new List<EventsDTO.Squad>();
                foreach (var thisTrackName in item["trackNames"])
                {
                    if (!squads.ContainsKey(thisTrackName.Value<string>()))
                    {
                        var thisSquad = new EventsDTO.Squad { Name = thisSquadName.Value<string>() };
                        db.Squads.Add(thisSquad);
                        squads.Add(thisTrackName.Value<string>(), thisSquad);
                    }
                    theseSquads.Add(squads[thisSquadName.Value<string>()]);
                }

                var session = new EventsDTO.Session
                {
                    SessionTitle = item["title"].Value<string>(),
                    StartTime = item["startTime"].Value<DateTime>(),
                    EndTime = item["endTime"].Value<DateTime>(),
                    Squad = theseSquads[0],
                    SessionDescription = item["SessionDescription"].Value<string>()
                };

                session.SessionCoaches = new List<SessionCoach>();
                foreach (var sp in theseCoaches)
                {
                    session.SessionCoaches.Add(new SessionCoach
                    {
                        Session = session,
                        Coaches = sp
                    });
                }

                db.Sessions.Add(session);
            }
        }
    }
}
