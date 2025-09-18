using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ClientGroups.BulkDeleteClientGroups.Commands;

namespace KOG.ECommerce.Features.ClientGroups.BulkDeleteClientGroups
{
    public record BulkDeleteClientGroupsRequestViewModel(List<string> Ids);
    public class BulkDeleteClientGroupsRequestValidator : AbstractValidator<BulkDeleteClientGroupsRequestViewModel>
    {
        public BulkDeleteClientGroupsRequestValidator() { }
    }
    public class BulkDeleteClientGroupsRequestProfile : Profile
    {
        public BulkDeleteClientGroupsRequestProfile() {
            CreateMap<BulkDeleteClientGroupsRequestViewModel, BulkDeleteClientGroupsCommand>();
        }
    }
}
