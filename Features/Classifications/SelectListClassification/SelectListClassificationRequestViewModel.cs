using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Classifications.Queries;

namespace KOG.ECommerce.Features.Classifications.SelectListClassification
{
    public record SelectListClassificationRequestViewModel();
    public class SelectListClassificationRequestValidator : AbstractValidator<SelectListClassificationRequestViewModel>
    {
        public SelectListClassificationRequestValidator()
        {
        }
    }
    public class SelectListClassificationRequestProfile : Profile
    {
        public SelectListClassificationRequestProfile()
        {
            CreateMap<SelectListClassificationRequestViewModel, SelectListClassificationQuery>();
        }
    }
}
