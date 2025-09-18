using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.Classifications.Queries;

namespace KOG.ECommerce.Features.Classifications.GetClassificationByID
{
    public record GetClassificationByIDRequestViewModel(string ID);
    public class GetClassificationByIDRequestValidator : AbstractValidator<GetClassificationByIDRequestViewModel>
    {
        public GetClassificationByIDRequestValidator()
        {
        }
    }
    public class GetClassificationByIDRequestProfile : Profile
    {
        public GetClassificationByIDRequestProfile()
        {
            CreateMap<GetClassificationByIDRequestViewModel, GetClassificationByIDQuery>();
        }
    }
}
