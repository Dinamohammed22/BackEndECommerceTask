using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Clients.DeactivateClients.Orchestrators;
using KOG.ECommerce.Features.Users.DeactivateUser.Commands;
using KOG.ECommerce.Features.Wishlists.DeactivateWishlist.Commands;

namespace KOG.ECommerce.Features.Clients.DeactivateClients
{
    public record DeactivateClientsRequestViewModel(string ID);

    public class DeactivateClientsRequestValidator : AbstractValidator<DeactivateClientsRequestViewModel>
    {
        public DeactivateClientsRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty().Length(1, 100);
        }
    }
    public class DeactivateClientsEndPointRequestProfile : Profile
    {
        public DeactivateClientsEndPointRequestProfile()
        {
            CreateMap<DeactivateClientsRequestViewModel, DeactiveClientOrchestrator>();
            CreateMap<DeactiveClientOrchestrator, DeactivateWishlistCommand>();
            CreateMap<DeactiveClientOrchestrator, DeactivateUserCommand>();

        }
    }

}
