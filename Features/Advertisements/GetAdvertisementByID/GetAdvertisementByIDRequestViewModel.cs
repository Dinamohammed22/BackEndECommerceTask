using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Advertisements.Queries;

namespace KOG.ECommerce.Features.Advertisements.GetAdvertisementByID
{
    public record GetAdvertisementByIDRequestViewModel(string ID);
    public class GetAdvertisementByIDRequestValidator : AbstractValidator<GetAdvertisementByIDRequestViewModel>
    {
        public GetAdvertisementByIDRequestValidator()
        {
        }
    }

    public class GetAdvertisementByIDRequestProfile : Profile
    {
        public GetAdvertisementByIDRequestProfile()
        {
            CreateMap<GetAdvertisementByIDRequestViewModel, GetAdvertisementByIdQuery>();
        }
    }
}
