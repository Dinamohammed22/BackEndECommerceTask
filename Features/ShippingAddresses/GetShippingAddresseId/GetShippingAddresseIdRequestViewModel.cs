using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ShippingAddresses.Queries;

namespace KOG.ECommerce.Features.ShippingAddresses.GetShippingAddresseId
{
    public record GetShippingAddresseIdRequestViewModel(string ID);
    public class GetShippingAddresseIdRequestValidator : AbstractValidator<GetShippingAddresseIdRequestViewModel>
    {
        public GetShippingAddresseIdRequestValidator()
        {
        }
    }
    public class GetShippingAddresseIdRequestProfile : Profile
    {
        public GetShippingAddresseIdRequestProfile()
        {
            CreateMap<GetShippingAddresseIdRequestViewModel, GetShippingAddresseIdQuery>();
        }
    }
}
