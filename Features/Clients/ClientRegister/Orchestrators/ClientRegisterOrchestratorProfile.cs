using AutoMapper;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;

namespace KOG.ECommerce.Features.Clients.ClientRegister.Orchestrators
{
    public class ClientRegisterOrchestratorProfile : Profile
    {
        public ClientRegisterOrchestratorProfile() {
       
            CreateMap<ClientRegisterOrchestrator, ClientRegisterCommand>();
           

        }
    }
}
