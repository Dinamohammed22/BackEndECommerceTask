using KOG.ECommerce.Helpers;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace KOG.ECommerce.Common.Enums;

public enum ErrorCode
{
    [DescriptionAnnotation("none", "none")]
    None = 0,
    [DescriptionAnnotation("Validation Errors", "Validation Errors")]
    ValidationErrors = 1,
    [DescriptionAnnotation("خطأ غير معرف", "Unknown Error")]
    UnKnown = 2,
    [DescriptionAnnotation("Not Found ", "Not Found ")]
    NotFound = 3,
    [DescriptionAnnotation("Can not Delete ", "Can not Delete ")]
    CannotDelete = 4,
    [DescriptionAnnotation("Invalid OTP", "Invalid OTP")]
    InvalidOTP = 5,
    [DescriptionAnnotation("Exist Mobile Number", "Exist Mobile Number")]
    ExistMobile = 6,
    [DescriptionAnnotation("Unauthorize Access ", "Unauthorize Access ")]
    Unauthorize = 7,
    [DescriptionAnnotation("can not send Message", "can not send Message")]
    CannotSend = 8,
    [DescriptionAnnotation(" Not Approved ", " Not Approved ")]
    NotApproved = 9,
    [DescriptionAnnotation("Not Active ", "Not Active ")]
    NotActive = 10,
    [DescriptionAnnotation("Can not Add Discount ", "Can not Add Discount ")]
    CannotAddDiscount = 11,
    [DescriptionAnnotation("Can not Edit ", "Can not Edit ")]
    CannotEdit = 12,
    [DescriptionAnnotation("Unauthorize Access Token is blacklisted", "Unauthorize Access  Token is blacklisted")]
    UnauthorizeTokenIsBlackListed,
    [DescriptionAnnotation("Product Not Approved ", "Product Not Approved ")]
    ProductNotApproved,
    [DescriptionAnnotation("Brand Not Found ", "Brand Not Found ")]
    BrandNotFound,
    [DescriptionAnnotation("Media Not Found ", "Media Not Found ")]
    MediaNotFound,
    [DescriptionAnnotation("Category Not Found ", "Category Not Found ")]
    CategoryNotFound,
    [DescriptionAnnotation("Product Not Found ", "Product Not Found ")]
    ProductNotFound,
    [DescriptionAnnotation("Order Not Found ", "Order Not Found ")]
    OrderNotFound,
    [DescriptionAnnotation("The Product Quantity in stock is not enough ", "The Product Quantity in stock is not enough ")]
    QuantityNotEnough,
    [DescriptionAnnotation("ShippingAddress Not Found ", "ShippingAddress Not Found ")]
    ShippingAddressNotFound,
    [DescriptionAnnotation("Product Not Found in this wishlist", "Product Not Found in this wishlist")]
    ProductWishlistNotFound,
    [DescriptionAnnotation("Exist National Number", "Exist National Number")]
    ExistNationalNumber ,
    [DescriptionAnnotation("Mobile Or Password Not Correct", "Mobile Or Password Not Correct")]
    MobileOrPasswordNotCorrect,
    [DescriptionAnnotation("No Account For This Mobile", "No Account For This Mobile")]
    NoAccountForMobile,
    [DescriptionAnnotation("Already Has 3 Shipping Addresses. Cannot add more", "Already Has 3 Shipping Addresses.Cannot add more")]
    AlreadyHasThreeShippingAddresses,
    [DescriptionAnnotation("No Default Shipping Address for this Client", "No Default Shipping Address for this Client")]
    NoDefaultShippingAddress ,
    [DescriptionAnnotation("You already have Address with the same location ", "You already have Address with the same location")]
    DoublicateShippingAddress,
    [DescriptionAnnotation("Client Not Verified ", "Client Not Verified ")]
    NotVerified,
    [DescriptionAnnotation("Client Not Verified ", "Client Not Verified ")]
    NotEnoughProducts,
    [DescriptionAnnotation("Product Restocked But can not send Message", "Product Restocked But can not send Message")]
    ProductRestockdAndCannotSend ,
    [DescriptionAnnotation("Can not Deactive", "Can not Deactive")]
    CannotDeactive,
    [DescriptionAnnotation("Cart is Empty", "Cart is Empty")]
    CartIsEmpty,
    [DescriptionAnnotation("Client Not Logged In Before", "The client has never logged in before.")]
    ClientNotLoggedInBefore,

}
