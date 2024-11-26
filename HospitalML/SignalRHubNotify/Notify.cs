using Microsoft.AspNetCore.SignalR;

namespace HospitalML.SignalRHubNotify
{
    public class PatientHub : Hub
    {
        public async Task SendCritical(int PatienId)
        {
            await Clients.All.SendAsync("CriticalPatient",PatienId);
        }
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }
    }
}
