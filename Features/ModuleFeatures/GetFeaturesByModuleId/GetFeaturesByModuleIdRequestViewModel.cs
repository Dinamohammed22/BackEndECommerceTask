using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ModuleFeatures.Queries;

namespace KOG.ECommerce.Features.ModuleFeatures.GetFeaturesByModuleId
{
    public record GetFeaturesByModuleIdRequestViewModel(string ModuleId);
    public class GetFeaturesByModuleIdValidator : AbstractValidator<GetFeaturesByModuleIdRequestViewModel>
    {
        public GetFeaturesByModuleIdValidator() { }
    }
    public class GetFeaturesByModuleIdRequestEndPoint : Profile
    {
        public GetFeaturesByModuleIdRequestEndPoint() {
            CreateMap<GetFeaturesByModuleIdRequestViewModel, GetFeaturesByModuleIdQuery>();
        }
    }
}
