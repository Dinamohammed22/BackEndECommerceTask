using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Cities.Queries;

namespace KOG.ECommerce.Features.Cities.Queries.GetLisCity
{
    public record GetListCityRequestViewModel(int pageIndex = 1, int pageSize = 100);

    public class GetGroupListEndPointRequestValidator : AbstractValidator<GetListCityRequestViewModel>
    {
        public GetGroupListEndPointRequestValidator()
        {
            //RuleFor(request => request.Name).NotEmpty();
        }
    }

    public class GetListCityRequestProfile : Profile
    {
        public GetListCityRequestProfile()
        {
            CreateMap<GetListCityRequestViewModel, GetListCityQuery>();
        }
    }

}
