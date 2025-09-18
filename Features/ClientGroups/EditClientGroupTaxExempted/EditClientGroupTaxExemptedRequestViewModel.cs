using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.ClientGroups.EditClientGroupTaxExempted.Commands;

namespace KOG.ECommerce.Features.ClientGroups.EditClientGroupTaxExempted
{
    public record EditClientGroupTaxExemptedRequestViewModel(string ID,bool TaxExempted);
    public class EditClientGroupTaxExemptedRequestValidator : AbstractValidator<EditClientGroupTaxExemptedRequestViewModel>
    {
        public EditClientGroupTaxExemptedRequestValidator()
        {
        }
    }
    public class EditClientGroupTaxExemptedRequestProfile : Profile
    {
        public EditClientGroupTaxExemptedRequestProfile()
        {
            CreateMap<EditClientGroupTaxExemptedRequestViewModel, EditClientGroupTaxExemptedCommand>();
        }
    }
}
