using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Clients.Queries;

namespace KOG.ECommerce.Features.Clients.GetClientsWithDefualtShippingAddress
{
    public record GetClientsWithDefualtShippingAddressRequestViewModel();
    public class GetClientsWithDefualtShippingAddressRequestValidator : AbstractValidator<GetClientsWithDefualtShippingAddressRequestViewModel>
    {
        public GetClientsWithDefualtShippingAddressRequestValidator()
        {
        }
    }
    public class GetClientsWithDefualtShippingAddressRequestProfile : Profile
    {
        public GetClientsWithDefualtShippingAddressRequestProfile()
        {
            CreateMap<GetClientsWithDefualtShippingAddressRequestViewModel, GetClientsWithDefualtShippingAddressQuery>();
        }
    }
}
