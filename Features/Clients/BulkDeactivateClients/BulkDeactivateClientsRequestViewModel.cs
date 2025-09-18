using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.BulkDeactivateClients.Commands;

namespace KOG.ECommerce.Features.Clients.BulkDeactivateClients
{
    public record BulkDeactivateClientsRequestViewModel(List<string> Ids);
    public class BulkDeactivateClientsRequestValidator : AbstractValidator<BulkDeactivateClientsRequestViewModel>
    {
        public BulkDeactivateClientsRequestValidator() { }
    }
    public class BulkDeactivateClientsRequestProfile : Profile
    {
        public BulkDeactivateClientsRequestProfile() { 
            CreateMap<BulkDeactivateClientsRequestViewModel, BulkDeactivateClientsCommand>();
        }
    }
}
