using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.AddRangeCities.Commands;
using KOG.ECommerce.Features.Common.Governorates.DTOs;
using KOG.ECommerce.Features.Governorates.AddRangeGovernrates.Commands;

namespace KOG.ECommerce.Features.Governorates.AddRangeGovernrates
{
    public record AddRangeGovernratesRequestViewModel(List<GovernrateDTO> Governrates);
    public class AddRangeGovernratesRequestValidator : AbstractValidator<AddRangeGovernratesRequestViewModel>
    {
        public AddRangeGovernratesRequestValidator()
        {

        }
    }
    public class AddRangeGovernratesProfile : Profile
    {
        public AddRangeGovernratesProfile()
        {
            CreateMap<AddRangeGovernratesRequestViewModel, AddRangeGovernratesCommand>();

        }
    }
}
