using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Governorates.Queries;


namespace KOG.ECommerce.Features.Governorates.GetDropdownListGovernorate
{
    public record GetDropdownListGovernorateRequestViewModel;

    public class GetDropdownListGovernoratRequestValidator : AbstractValidator<GetDropdownListGovernorateRequestViewModel>
    {
        public GetDropdownListGovernoratRequestValidator()
        {

        }
    }

    public class GetDropdownListListGovernorateRequestProfile : Profile
    {
        public GetDropdownListListGovernorateRequestProfile()
        {
            CreateMap<GetDropdownListGovernorateRequestViewModel, GetDropdownListGovernorateQuery>();
        }
    }
}
