using AutoMapper;
using FluentValidation;
using KOG.ECommerce.Features.Products.CreateProduct.Commands;
using KOG.ECommerce.Features.Products.CreateProduct.Orchestrators;
using KOG.ECommerce.Models.Enums;

namespace KOG.ECommerce.Features.Products.CreateProduct;

public record CreateProductRequestViewModel(string Name, string Description, string CategoryId, List<string> Tags,
    string Model, double Price, double Tax, string BrandId,
    int MinimumQuantity, int MaximumQuantity, double Length,
    double Width, double Height, double Liter, DateTime AvailableDate, List<string> Paths,
    string SpecificationMetrix, string Data, bool FeaturedProduct, int Quantity, int NumberOfPoints,
    bool IsActivePoint, bool IsActive, Grade Grade);

public class CreateProductRequestValidator:AbstractValidator<CreateProductRequestViewModel>
{
    public CreateProductRequestValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MinimumLength(2).WithMessage("Product name must be at least 2 characters long.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.");

        RuleForEach(x => x.Tags)
            .MaximumLength(50).WithMessage("Each tag must not exceed 50 characters.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.");
        
        RuleFor(x => x.Data)
         .NotEmpty().WithMessage("Data is required.");

        RuleFor(x => x.SpecificationMetrix)
                 .NotEmpty().WithMessage("Specification Metrix is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0).WithMessage("Tax must be zero or a positive value.");

        RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("Brand ID is required.");

        RuleFor(x => x.MinimumQuantity)
            .GreaterThan(0).WithMessage("Minimum quantity must be greater than zero.");
        RuleFor(x => x.MaximumQuantity)
       .GreaterThan(0).WithMessage("Maximum quantity must be greater than zero.")
       .GreaterThanOrEqualTo(x => x.MinimumQuantity)
       .WithMessage("Maximum quantity must be greater than or equal to minimum quantity.")
       .LessThanOrEqualTo(9999).WithMessage("Maximum quantity must be less than or equal to 9999.");

        RuleFor(x => x.Length)
            .GreaterThan(0).WithMessage("Length must be greater than zero.");
        RuleFor(x => x.Width)
            .GreaterThan(0).WithMessage("Width must be greater than zero.");
        RuleFor(x => x.Height)
            .GreaterThan(0).WithMessage("Height must be greater than zero.");
        RuleFor(x => x.Liter)
            .GreaterThan(0).WithMessage("Liter must be greater than zero.");

        RuleFor(x => x.AvailableDate)
            .GreaterThanOrEqualTo(DateTime.Now.Date).WithMessage("Available date cannot be in the past.");

        RuleForEach(x => x.Paths)
            .NotEmpty().WithMessage("Each path must not be empty.");
    }
}
public class CreateProductEndPointRequestProfile : Profile
{
    public CreateProductEndPointRequestProfile()
    {
        CreateMap<CreateProductOrchestrator, CreateProductCommand>();
        CreateMap<CreateProductRequestViewModel, CreateProductOrchestrator>();


    }
}
