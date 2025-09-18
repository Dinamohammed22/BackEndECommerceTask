using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ClientGroups.DeleteClientGroup.Commands;
using KOG.ECommerce.Features.Common.ClientGroups.Queries;

namespace KOG.ECommerce.Features.ClientGroups.DeleteClientGroup
{
    public record DeleteClientGroupRequestViewModel(string Id);
    public class DeleteClientGroupRequestValidator : AbstractValidator<DeleteClientGroupRequestViewModel>
    {
        public DeleteClientGroupRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required.");

        }
    }
    public class DeleteClientGroupEndPointRequestProfile : Profile
    {
        public DeleteClientGroupEndPointRequestProfile()
        {
            CreateMap<DeleteClientGroupRequestViewModel, DeleteClientGroupCommand>();
            CreateMap<DeleteClientGroupCommand,CheckClientGroupHasClientQuery>();
        }
    }
}
