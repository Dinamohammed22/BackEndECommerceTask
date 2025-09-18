using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Advertisements.Queries;

namespace KOG.ECommerce.Features.Advertisements.GetAllAdvertisement
{
    public record GetAllAdvertisementRequestViewModel(int pageIndex = 1, int pageSize = 100);
    public class GetAllAdvertisementRequestValidator : AbstractValidator<GetAllAdvertisementRequestViewModel>
    {
        public GetAllAdvertisementRequestValidator()
        {
        }
    }
    public class GetAllAdvertisementRequestProfile : Profile
    {
        public GetAllAdvertisementRequestProfile()
        {
            CreateMap<GetAllAdvertisementRequestViewModel, GetAllAdvertisementQuery>();
        }
     }
}
