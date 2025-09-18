using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;

namespace KOG.ECommerce.Features.ClientGroups.SelectClientGroupList
{
    public record SelectClientGroupListRequestViewModel();
    public class SelectClientGroupListRequestValidator:AbstractValidator<SelectClientGroupListRequestViewModel>
    {
        public SelectClientGroupListRequestValidator()
        {

        }

    }
    public class SelectClientGroupListRequestProfile:Profile
    {
        public SelectClientGroupListRequestProfile()
        {
            CreateMap<SelectClientGroupListRequestViewModel, SelectClientGroupListQuery>();
        }
    }
}
