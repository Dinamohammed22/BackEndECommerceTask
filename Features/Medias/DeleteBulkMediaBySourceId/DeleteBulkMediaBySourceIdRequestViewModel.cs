using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Medias.DeleteBulkMediaBySourceId.Commands;

namespace KOG.ECommerce.Features.Medias.DeleteBulkMediaBySourceId
{
    public record DeleteBulkMediaBySourceIdRequestViewModel(string SourceId);
    public class DeleteBulkMediaBySourceIdRequestValidator : AbstractValidator<DeleteBulkMediaBySourceIdRequestViewModel>
    {
        public DeleteBulkMediaBySourceIdRequestValidator() { }
    }
    public class DeleteBulkMediaBySourceIdRequestProfile : Profile
    {
        public DeleteBulkMediaBySourceIdRequestProfile() 
        {
            CreateMap<DeleteBulkMediaBySourceIdRequestViewModel, DeleteBulkMediaBySourceIdCommand>();
        }
    }
}
