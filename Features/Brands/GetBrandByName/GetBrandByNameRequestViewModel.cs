using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Brands.Queries;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Governorates.GetGovernorateByName;

namespace KOG.ECommerce.Features.Brands.GetBrandByName
{
    public record GetBrandByNameRequestViewModel(string Name, int pageIndex = 1,
        int pageSize = 100);

    public class GetBrandByNameRequestValidator : AbstractValidator<GetBrandByNameRequestViewModel>
    {
        public GetBrandByNameRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
        }
    }

    public class GetBrandByNameRequestProfile : Profile
    {
        public GetBrandByNameRequestProfile()
        {
            CreateMap<GetBrandByNameRequestViewModel, GetBrandByNameQuery>();
        }
    }
}
