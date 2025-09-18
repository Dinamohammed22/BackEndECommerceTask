using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Governorates.AddRangeGovernrates.Commands;
using KOG.ECommerce.Features.Governorates.AddRangeGovernrates;
using KOG.ECommerce.Features.TempController.InitiateGovernrateCitiesData.Orchistrator;

namespace KOG.ECommerce.Features.TempController.InitiateGovernrateCitiesData
{
    public class InitiateGovernrateCitiesDataRequestViewModel
    {
    }
    public class InitiateGovernrateCitiesDataRequestViewModelValidator : AbstractValidator<InitiateGovernrateCitiesDataRequestViewModel>
    {
        public InitiateGovernrateCitiesDataRequestViewModelValidator()
        {

        }
    }
    public class InitiateGovernrateCitiesDataRequestViewModelProfile : Profile
    {
        public InitiateGovernrateCitiesDataRequestViewModelProfile()
        {
            CreateMap<InitiateGovernrateCitiesDataRequestViewModel, InitiateGovernrateCitiesDataOrchistrator>();

        }
    }
}
