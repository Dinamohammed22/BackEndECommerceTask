using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;

namespace KOG.ECommerce.Features.ClientGroups.GetClientGroupByID
{
    public record GetClientGroupByIDRequestViewModel(string Id);
    public class GetClientGroupByIDRequestValidator : AbstractValidator<GetClientGroupByIDRequestViewModel>
    {
        public GetClientGroupByIDRequestValidator()
        {
        }
    }
    public class GetClientGroupByIDRequestProfile : Profile
    {
        public GetClientGroupByIDRequestProfile()
        {
            CreateMap<GetClientGroupByIDRequestViewModel, GetClientGroupByIDQuery>();
        }
    }
}
