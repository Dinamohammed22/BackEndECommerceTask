using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Companies.EditPointToCash.Commands;

namespace KOG.ECommerce.Features.Companies.EditPointToCash
{
    public record EditPointToCashRequestViewModel(string Id, int NumberOfPoints, double AmountOfMoney);
    public class EditPointToCashRequestValidator:AbstractValidator<EditPointToCashRequestViewModel>
    {
        public EditPointToCashRequestValidator() { }
    }
    public class EditPointToCashRequestProfile : Profile
    {
        public EditPointToCashRequestProfile()
        {
            CreateMap<EditPointToCashRequestViewModel, EditPointToCashCommand>();
        }
    }
}
