using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.BulkActivateClients.Commands;

namespace KOG.ECommerce.Features.Clients.BulkActivateClients
{
    public record BulkActivateClientsRequestViewModel(List<string>Ids);
    public class BulkActivateClientsRequestValidator : AbstractValidator<BulkActivateClientsRequestViewModel>
    {
        public BulkActivateClientsRequestValidator() { }
    }
    public class BulkActivateClientsRequestProfile : Profile
    {
        public BulkActivateClientsRequestProfile() {
            CreateMap<BulkActivateClientsRequestViewModel, BulkActivateClientsCommand>();
        }
    }
}
