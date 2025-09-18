using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Classifications;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Products;

namespace KOG.ECommerce.Features.Products.UpdateProduct.Commands;

public record UpdateProductCommand(
    string ID, string Name, string Description, string CategoryId, List<string> Tags,
    string Model, double Price, double Tax, string BrandId,
    int MinimumQuantity, int MaximumQuantity, double Length,
    double Width, double Height, double Liter, DateTime AvailableDate, string SpecificationMetrix, 
    string Data, bool FeaturedProduct, int Quantity, int NumberOfPoints, bool IsActive, bool IsActivePoint, Grade Grade) : IRequestBase<bool>;
public class UpdateProductCommandHandler : RequestHandlerBase<Product, UpdateProductCommand, bool>
{
    public UpdateProductCommandHandler(RequestHandlerBaseParameters<Product> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(p => p.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        Product product = new Product();
        product.ID = request.ID;
        product.Name = request.Name;
        product.Description = request.Description;
        product.CategoryId = request.CategoryId;
        product.Tags = request.Tags;
        product.Model = request.Model;
        product.Price = request.Price;
        product.Tax = request.Tax;
        product.BrandId = request.BrandId;
        product.MinimumQuantity = request.MinimumQuantity;
        product.MaximumQuantity = request.MaximumQuantity;
        product.Length = request.Length;
        product.Width = request.Width;
        product.Height = request.Height;
        product.Liter = request.Liter;
        product.AvailableDate = request.AvailableDate;
        product.SpecificationMetrix = request.SpecificationMetrix;
        product.Data = request.Data;
        product.FeaturedProduct = request.FeaturedProduct;
        product.Quantity = request.Quantity;
        product.NumberOfPoints = request.NumberOfPoints;
        product.IsActive = request.IsActive;
        product.IsActivePoint = request.IsActivePoint;
        product.Grade = request.Grade;
        _repository.SaveIncluded(product, nameof(product.Name),nameof(product.Description), nameof(product.CategoryId), nameof(product.Tags)
            , nameof(product.Model), nameof(product.Price), nameof(product.Tax), nameof(product.BrandId), nameof(product.MinimumQuantity)
            , nameof(product.MaximumQuantity), nameof(product.Length), nameof(product.Width), nameof(product.Height)
            , nameof(product.Liter), nameof(product.AvailableDate), nameof(product.SpecificationMetrix), nameof(product.Data)
            , nameof(product.FeaturedProduct), nameof(product.Quantity), nameof(product.IsActive), nameof(product.IsActivePoint)
            , nameof(product.NumberOfPoints),nameof(product.Grade));
        _repository.SaveChanges();
        return RequestResult<bool>.Success(true);
    }
}

