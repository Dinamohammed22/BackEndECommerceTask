using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Common.OrderItems.Queries;
using KOG.ECommerce.Features.Products.DeleteProduct.Commands;

namespace KOG.ECommerce.Features.Products.DeleteProduct;

public record DeleteProductRequestViewModel(string ID);
public class DeleteProductRequestValidator : AbstractValidator<DeleteProductRequestViewModel>
{
    public DeleteProductRequestValidator()
    {
        RuleFor(request => request.ID).NotEmpty().Length(1, 100);
    }
}
public class DeleteProductEndPointRequestProfile : Profile
{
    public DeleteProductEndPointRequestProfile()
    {
        CreateMap<DeleteProductRequestViewModel, DeleteProductCommand>();
        CreateMap< DeleteProductCommand, CheckIfProductInOrderQuery>();
    }
}


