using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.DeactivatePoints.Commands;

namespace KOG.ECommerce.Features.Products.DeactivatePoints
{
    public record DeactivatePointsRequestViewModel(string ID);
    public class DeactivatePointsRequestValidator : AbstractValidator<DeactivatePointsRequestViewModel>
    {
        public DeactivatePointsRequestValidator()
        {
        }
    }
    public class DeactivatePointsRequestProfile : Profile
    {
        public DeactivatePointsRequestProfile()
        {
            CreateMap<DeactivatePointsRequestViewModel, DeactivatePointsCommand>();
        }
    } 
}
