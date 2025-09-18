using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.ActivateClients.Orchestrators;
using KOG.ECommerce.Features.Users.ActivateUser.Commands;
using KOG.ECommerce.Features.Wishlists.ActivateWishlist.Commands;

namespace KOG.ECommerce.Features.Clients.ActivateClients
{
    public record ActivateClientsRequestViewModel(string ID);


    public class ActiveClientsRequestValidator : AbstractValidator<ActivateClientsRequestViewModel>
    {
        public ActiveClientsRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class ActivateClientsEndPointRequestProfile : Profile
    {
        public ActivateClientsEndPointRequestProfile()
        {
            CreateMap<ActivateClientsRequestViewModel, ActivateClientOrchestrator>();
            CreateMap<ActivateClientOrchestrator, ActivateWishlistCommand>();
            CreateMap<ActivateClientOrchestrator, ActivateUserCommand>();

        }
    }
}
