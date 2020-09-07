using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Data;
using EventsDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd.Data
{
    public class DevIntersectionLoader : DataLoader
    {
        public override async Task LoadDataAsync(Stream fileStream, ApplicationDbContext db)
        {
            var reader = new JsonTextReader(new StreamReader(fileStream));

            var coachesName = new Dictionary<string, Coach>();
            var squad = new Dictionary<string, Squad>();

            JArray doc = await JArray.LoadAsync(reader);

            foreach (JObject item in doc)
            {
                var theseCoaches = new List<Coach>();
                foreach (var thisCoachName in item["coachNames"])
                {
                    if (!coachesName.ContainsKey(thisCoachName.Value<string>()))
                    {
                        var thisCoach = new Coach { CoachName = thisCoachName.Value<string>() };
                        db.Coaches.Add(thisCoach);
                        coachesName.Add(thisCoachName.Value<string>(), thisCoach);
                        Console.WriteLine(thisCoachName.Value<string>());
                    }
                    theseCoaches.Add(coachesName[thisCoachName.Value<string>()]);
                }

                var theseSquads = new List<Squad>();
                foreach (var thisSquadName in item["squadNames"])
                {
                    if (!squad.ContainsKey(thisSquadName.Value<string>()))
                    {
                        var thisSquad = new Squad { Name = thisSquadName.Value<string>() };
                        db.Squads.Add(thisSquad);
                        squad.Add(thisSquadName.Value<string>(), thisSquad);
                    }
                    theseSquads.Add(squad[thisSquadName.Value<string>()]);
                }

                var session = new Session
                {
                    SessionTitle = item["title"].Value<string>(),
                    StartTime = item["startTime"].Value<DateTime>(),
                    EndTime = item["endTime"].Value<DateTime>(),
                    Squads = theseSquads[0],
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
