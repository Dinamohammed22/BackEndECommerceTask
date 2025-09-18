using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Governorates.Queries;
using KOG.ECommerce.Features.Governorates.GetGovernorateByName;

namespace KOG.ECommerce.Features.Governorates.GetGovernorateByID
{
    public record GetGovernorateByIDRequestViewModel(string ID);

    public class GetGovernorateByIDRequestValidator : AbstractValidator<GetGovernorateByIDRequestViewModel>
    {
        public GetGovernorateByIDRequestValidator()
        {
            RuleFor(request => request.ID).NotEmpty();
        }
    }

    public class GetGovernorateByIDRequestProfile : Profile
    {
        public GetGovernorateByIDRequestProfile()
        {
            CreateMap<GetGovernorateByIDRequestViewModel, GetGovernorateByIDQuery>();
        }
    }

}
