using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.BulkDeactivateProduct.Orchistrator;

namespace KOG.ECommerce.Features.Products.BulkDeactivateProduct
{
    public record BulkDeactivateProductRequestViewModel(List<string> IDs);
    public class BulkDeactivateProductRequestValidator : AbstractValidator<BulkDeactivateProductRequestViewModel>
    {
        public BulkDeactivateProductRequestValidator()
        {
        }
    }
    public class BulkDeactivateProductRequestProfile : Profile
    {
        public BulkDeactivateProductRequestProfile()
        {
            CreateMap<BulkDeactivateProductRequestViewModel, BulkDeactivateProductOrchistrator>();
        }
    }
}
