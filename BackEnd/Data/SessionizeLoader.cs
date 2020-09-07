using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BackEnd.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd.Data

{
    public class SessionizeLoader : DataLoader
    {
        public override async Task LoadDataAsync(Stream fileStream, ApplicationDbContext db)
        {
            // var blah = new RootObject().rooms[0].sessions[0].speakers[0].name;

            var addedCoaches = new Dictionary<string, Coach>();
            var addedSquads = new Dictionary<string, Squad>();

            var array = await JToken.LoadAsync(new JsonTextReader(new StreamReader(fileStream)));

            var root = array.ToObject<List<RootObject>>();

            foreach (var date in root)
            {
                foreach (var room in date.Rooms)
                {
                    if (!addedSquads.ContainsKey(room.Name))
                    {
                        var thisSquad = new Squad { Name = room.Name };
                        db.Squads.Add(thisSquad);
                        addedSquads.Add(thisSquad.Name, thisSquad);
                    }

                    foreach (var thisSession in room.Sessions)
                    {
                        foreach (var coach in thisSession.Coach)
                        {
                            if (!addedCoaches.ContainsKey(coach.Name))
                            {
                                var thisCoach = new Coach { CoachName = coach.Name };
                                db.Coaches.Add(thisCoach);
                                addedCoaches.Add(thisCoach.CoachName, thisCoach);
                            }
                        }

                        var session = new Session
                        {
                            SessionTitle = thisSession.Title,
                            StartTime = thisSession.StartsAt,
                            EndTime = thisSession.EndsAt,
                            Squads = addedSquads[room.Name],
                            SessionDescription = thisSession.Description
                        };

                        session.SessionCoaches = new List<SessionCoach>();
                        foreach (var sp in thisSession.Coach)
                        {
                            session.SessionCoaches.Add(new SessionCoach
                            {
                                Session = session,
                                Coaches = addedCoaches[sp.Name]
                            });
                        }

                        db.Sessions.Add(session);
                    }
                }
            }
        }

        private class RootObject
        {
            public DateTime Date { get; set; }
            public List<Room> Rooms { get; set; }
            public List<TimeSlot> TimeSlots { get; set; }
        }

        private class ImportSpeaker
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        private class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<object> CategoryItems { get; set; }
            public int Sort { get; set; }
        }

        private class ImportSession
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartsAt { get; set; }
            public DateTime EndsAt { get; set; }
            public bool IsServiceSession { get; set; }
            public bool IsPlenumSession { get; set; }
            public List<ImportSpeaker> Coach { get; set; }
            public List<Category> Categories { get; set; }
            public int RoomId { get; set; }
            public string Room { get; set; }
        }

        private class Room
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public List<ImportSession> Sessions { get; set; }
            public bool HasOnlyPlenumSessions { get; set; }
        }

        private class TimeSlot
        {
            public string SlotStart { get; set; }
            public List<Room> Rooms { get; set; }
        }
    }
}