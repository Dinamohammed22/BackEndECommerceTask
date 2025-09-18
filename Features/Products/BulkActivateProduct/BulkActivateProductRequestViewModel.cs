using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.BulkActivateProduct.Orchistrator;

namespace KOG.ECommerce.Features.Products.BulkActivateProduct
{
    public record BulkActivateProductRequestViewModel(List<string> IDs);
    public class BulkActivateProductRequestValidator : AbstractValidator<BulkActivateProductRequestViewModel>
    {
        public BulkActivateProductRequestValidator() { }
    }
    public class BulkActivateProductRequestProfile : Profile
    {
        public BulkActivateProductRequestProfile() {
            CreateMap<BulkActivateProductRequestViewModel, BulkActivateProductOrchistrator>();
        }
    }
}
