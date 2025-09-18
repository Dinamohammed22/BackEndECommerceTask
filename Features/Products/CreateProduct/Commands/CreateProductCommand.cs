using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.CreateProduct.Commands;

public record CreateProductCommand(
    string Name, string Description, string CategoryId, List<string> Tags,
    string Model, double Price, double Tax, string BrandId,
    int MinimumQuantity, int MaximumQuantity, double Length,
    double Width, double Height, double Liter, DateTime AvailableDate, string SpecificationMetrix, 
    string Data, bool FeaturedProduct, int Quantity, int NumberOfPoints, bool IsActivePoint, bool IsActive, Grade Grade) :IRequestBase<string>;

public class CreateProductCommandHandler : RequestHandlerBase<Product, CreateProductCommand, string>
{
    public CreateProductCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId,
            Tags = request.Tags,
            Model = request.Model,
            Price = request.Price,
            Tax = request.Tax,
            BrandId = request.BrandId,
            MinimumQuantity = request.MinimumQuantity,
            MaximumQuantity = request.MaximumQuantity,
            Length = request.Length,
            Width = request.Width,
            Height = request.Height,
            Liter = request.Liter,
            AvailableDate = request.AvailableDate,
            SpecificationMetrix = request.SpecificationMetrix,
            Data = request.Data,
            FeaturedProduct = request.FeaturedProduct,
            Quantity = request.Quantity,
            IsActivePoint = request.IsActivePoint,
            NumberOfPoints = request.NumberOfPoints,
             IsActive=request.IsActive,
             CompanyId=_userState.UserID,
             Grade = request.Grade
        };
        _repository.Add(product);
        _repository.SaveChanges();
        var result = product.ID;
        return RequestResult<string>.Success(result);
    }
}