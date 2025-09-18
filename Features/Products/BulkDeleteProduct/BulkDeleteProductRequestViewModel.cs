using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Products.DeleteProduct.Commands;
using KOG.ECommerce.Features.Products.DeleteProduct;
using KOG.ECommerce.Features.Products.BulkDeleteProduct.Orchistrator;

namespace KOG.ECommerce.Features.Products.BulkDeleteProduct
{
    public record BulkDeleteProductRequestViewModel(List<string> Ids);
    public class BulkDeleteProductRequestValidator : AbstractValidator<BulkDeleteProductRequestViewModel>
    {
        public BulkDeleteProductRequestValidator()
        {
        }
    }
    public class BulkDeleteProductRequestProfile : Profile
    {
        public BulkDeleteProductRequestProfile()
        {
            CreateMap<BulkDeleteProductRequestViewModel, BulkDeleteProductOrchistrator>();
        }
    }
}
