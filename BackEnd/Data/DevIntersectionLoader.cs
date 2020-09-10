using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd
{
    public class DevIntersectionLoader : DataLoader
    {
        public override async Task LoadDataAsync(Stream fileStream, ApplicationDbContext db)
        {
            var reader = new JsonTextReader(new StreamReader(fileStream));

            var coachNames = new Dictionary<string, Coach>();
            var tracks = new Dictionary<string, Track>();

            JArray doc = await JArray.LoadAsync(reader);

            foreach (JObject item in doc)
            {
                var theseCoaches = new List<Coach>();
                foreach (var thisCoachName in item["coachNames"])
                {
                    if (!coachNames.ContainsKey(thisCoachName.Value<string>()))
                    {
                        var thisCoach = new Coach { Name = thisCoachName.Value<string>() };
                        db.Coaches.Add(thisCoach);
                        coachNames.Add(thisCoachName.Value<string>(), thisCoach);
                        Console.WriteLine(thisCoachName.Value<string>());
                    }

                    var theseTracks = new List<Track>();
                    foreach (var thisTrackName in item["trackNames"])
                    {
                        if (!tracks.ContainsKey(thisTrackName.Value<string>()))
                        {
                            var thisTrack = new Track { Name = thisTrackName.Value<string>() };
                            db.Tracks.Add(thisTrack);
                            tracks.Add(thisTrackName.Value<string>(), thisTrack);
                        }
                        theseTracks.Add(tracks[thisTrackName.Value<string>()]);
                    }

                    var session = new Session
                    {
                        Title = item["title"].Value<string>(),
                        StartTime = item["startTime"].Value<DateTime>(),
                        EndTime = item["endTime"].Value<DateTime>(),
                        Track = theseTracks[0],
                        Abstract = item["abstract"].Value<string>()
                    };

                    session.SessionCoaches = new List<SessionCoach>();
                    foreach (var sp in theseCoaches)
                    {
                        session.SessionCoaches.Add(new SessionCoach
                        {
                            Session = session,
                            Coach = sp
                        });
                    }

                    db.Sessions.Add(session);
                }
            }
        }
    }
}
