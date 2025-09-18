using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Products.DTOs;
using KOG.ECommerce.Features.Common.Products.Queries;
using KOG.ECommerce.Features.Products.GetProductById.Orchestrator;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.GetProductById
{
    public record GetProductByIDRequestViewModel(string ID);

    public class GetProductByIDRequestValidator : AbstractValidator<GetProductByIDRequestViewModel>
    {
        public GetProductByIDRequestValidator()
        {

        }
    }
    public class GetProductByIDRequestProfile : Profile
    {
        public GetProductByIDRequestProfile()
        {
            CreateMap<GetProductByIDRequestViewModel, GetProductByIDWithMediaOrchestrator>();
            CreateMap<GetProductByIDWithMediaOrchestrator, GetProductByIDWithMediaQuery>();
            CreateMap<GetProductByIDWithMediaOrchestrator, CheckProductHasMediaQuery>();

        }
    }


}
