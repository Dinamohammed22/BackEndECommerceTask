using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Governorates.Queries;

namespace KOG.ECommerce.Features.Governorates.GetGovernorateByName
{
    public record GetGovernorateByNameRequestViewModel(string Name);

    public class GetGovernorateByNameRequestValidator : AbstractValidator<GetGovernorateByNameRequestViewModel>
    {
        public GetGovernorateByNameRequestValidator()
        {
            RuleFor(request => request.Name).NotEmpty();
        }
    }

    public class GetGovernorateByNameRequestProfile : Profile
    {
        public GetGovernorateByNameRequestProfile()
        {
            CreateMap<GetGovernorateByNameRequestViewModel, GetGovernorateByNameQuery>();
        }
    }
}
