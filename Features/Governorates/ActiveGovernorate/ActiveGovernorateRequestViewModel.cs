using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.ActiveGovernorate.Commands;

namespace KOG.ECommerce.Features.Governorates.ActiveGovernorate
{
    public record ActiveGovernorateRequestViewModel(string ID);
    public class ActiveGovernorateRequestValidator : AbstractValidator<ActiveGovernorateRequestViewModel>
    {
        public ActiveGovernorateRequestValidator()
        {
        }
    }
    public class ActiveGovernorateRequestProfile : Profile
    {
        public ActiveGovernorateRequestProfile()
        {
            CreateMap<ActiveGovernorateRequestViewModel, ActiveGovernorateCommand>();
        }
    }
}
