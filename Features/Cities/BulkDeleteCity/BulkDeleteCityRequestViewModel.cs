using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Cities.BulkDeleteCity.Orchestrator;

namespace KOG.ECommerce.Features.Cities.BulkDeleteCity
{
    public record BulkDeleteCityRequestViewModel(List<string> Ids);
    public class BulkDeleteCityRequestValidator:AbstractValidator<BulkDeleteCityRequestViewModel>
    {
        public BulkDeleteCityRequestValidator() { }
    }
    public class BulkDeleteCityRequestProfile : Profile
    {
        public BulkDeleteCityRequestProfile()
        {
            CreateMap<BulkDeleteCityRequestViewModel, BulkDeleteCityOrchestrator>();
        }
    }
}
