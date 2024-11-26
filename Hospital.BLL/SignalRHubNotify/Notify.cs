using Hospital.BLL.PatientServices.Dto;
using Microsoft.AspNetCore.SignalR;

namespace Hospital.BLL.SignalRHubNotify
{
    public class PatientHub : Hub
    {
        public async Task SendCritical(PatientDtoName Patient)
        {
            await Clients.All.SendAsync("CriticalPatient",Patient);
        }
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            await base.OnConnectedAsync();
        }
    }
}
