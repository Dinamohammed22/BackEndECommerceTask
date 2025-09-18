using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Brands.GetBrandByID.Orchestrator;
using KOG.ECommerce.Features.Common.Brands.Queries;

namespace KOG.ECommerce.Features.Brands.GetBrandByID
{
    public record GetBrandByIDRequestViewModel(string ID);
    public class GetBrandByIDRequestValidator:AbstractValidator<GetBrandByIDRequestViewModel>
    {
        public GetBrandByIDRequestValidator()
        { }
    }
    public class GetBrandByIDRequestProfile:Profile
    {
        public GetBrandByIDRequestProfile()
        {
            CreateMap<GetBrandByIDRequestViewModel, GetBrandByIdOrchestrator>();
            CreateMap<GetBrandByIdOrchestrator, GetBrandByIDQuery>();
            CreateMap<GetBrandByIdOrchestrator, CheckBrandHasMediaQuery>();
            CreateMap< GetBrandByIDQuery, GetBrandByIdOrchestrator>();
            CreateMap<GetBrandByIDRequestViewModel, GetBrandByIDQuery>();
        }
    }

}
