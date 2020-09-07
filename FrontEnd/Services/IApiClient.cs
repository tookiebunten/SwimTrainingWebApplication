using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsDTO;

namespace FrontEnd.Services
{
    public interface IApiClient
    {
        Task<List<SessionResponse>> GetSessionsAsync();
        Task<SessionResponse> GetSessionAsync(int id);
        Task<List<CoachResponse>> GetSpeakersAsync();
        Task<CoachResponse> GetSpeakerAsync(int id);
        Task PutSessionAsync(Session session);
        Task<bool> AddAttendeeAsync(Swimmer swimmer);
        Task<SwimmerResponse> GetAttendeeAsync(string name);
        Task DeleteSessionAsync(int id);
    }
}
