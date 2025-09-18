using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Classifications.Queries;

namespace KOG.ECommerce.Features.Classifications.GetAllClassifications
{
    public record GetAllClassificationsRequestViewModel(string? Name = null, int pageIndex = 1, int pageSize = 100);
    public class GetAllClassificationsRequestValidator : AbstractValidator<GetAllClassificationsRequestViewModel>
    {
        public GetAllClassificationsRequestValidator() { }
    }
    public class GetAllClassificationsRequestProfile:Profile
    {
        public GetAllClassificationsRequestProfile()
        {
            CreateMap<GetAllClassificationsRequestViewModel, GetAllClassificationsQuery>();
        }
    }
}
