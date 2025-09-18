using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.ActivatePoints.Commands;

namespace KOG.ECommerce.Features.Products.ActivatePoints
{
    public record ActivatePointsRequestViewModel(string ID);
    public class ActivatePointsRequestValidator : AbstractValidator<ActivatePointsRequestViewModel>
    {
        public ActivatePointsRequestValidator()
        {
        }
    }
    public class ActivatePointsRequestProfile : Profile
    {
        public ActivatePointsRequestProfile()
        {
            CreateMap<ActivatePointsRequestViewModel, ActivatePointsCommand>();
        }
    }
}
