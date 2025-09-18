using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.DeleteClient.Orchestrator;

namespace KOG.ECommerce.Features.Clients.DeleteClient
{
    public record DeleteClientRequestViewModel(string ID);
    public class DleteClientRequestValidator : AbstractValidator<DeleteClientRequestViewModel>
    {
        public DleteClientRequestValidator()
        {
        }
    }
    public class DleteClientRequestProfile : Profile
    {
        public DleteClientRequestProfile()
        {
            CreateMap<DeleteClientRequestViewModel, DeleteClientOrchestrator>();
        }
    }
}
