using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Classifications.Queries;

namespace KOG.ECommerce.Features.Classifications.GetClassificationsNames
{
    public record GetClassificationsNamesRequestViewModel();
    public class GetClassificationsNamesRequestValidator : AbstractValidator<GetClassificationsNamesRequestViewModel>
    {
        public GetClassificationsNamesRequestValidator()
        {
        }
    }
    public class GetClassificationsNamesRequestProfile : Profile
    {
        public GetClassificationsNamesRequestProfile()
        {
            CreateMap<GetClassificationsNamesRequestViewModel, GetClassificationsNamesQuery>();
        }
    }
}
