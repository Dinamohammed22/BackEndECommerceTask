using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Governorates.GetListGovernorate;

namespace KOG.ECommerce.Features.Brands.GetLisBrand
{
    public record GetListBrandRequestViewModel(int pageIndex = 1, int pageSize = 100);

    public class GetGroupListEndPointRequestValidator : AbstractValidator<GetListBrandRequestViewModel>
    {
        public GetGroupListEndPointRequestValidator()
        {
            //RuleFor(request => request.Name).NotEmpty();
        }
    }

    public class GetListBrandRequestProfile : Profile
    {
        public GetListBrandRequestProfile()
        {
            CreateMap<GetListBrandRequestViewModel, GetListBrandQuery>();
        }
    }

}
