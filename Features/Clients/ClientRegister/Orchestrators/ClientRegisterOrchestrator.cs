using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Clients.ClientRegister.Commands;
using KOG.ECommerce.Features.Common.Users.CreateUser.Commands;
using KOG.ECommerce.Features.Common.Users.GenerateOTP.Commands;
using KOG.ECommerce.Features.Common.Users.SendMessage;
using KOG.ECommerce.Features.Medias.SaveMedia.Commands;
using KOG.ECommerce.Features.ShippingAddresses.CreateShippingAddress.Commands;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Clients;
using KOG.ECommerce.Models.Enums;
using Microsoft.IdentityModel.Tokens;

namespace KOG.ECommerce.Features.Clients.ClientRegister.Orchestrators
{
    public record ClientRegisterOrchestrator(
        string? NationalNumber,
        string Name, 
        string Password,
        string Mobile,
        string GovernorateId,
        string CityId,
        string Street,
        string Landmark, 
        double Latitude, 
        double Longitude,
        string? Email, 
        string ConfirmPassword,
        string? Phone,
        List<string>? Paths,
        ClientActivity? ClientActivity,
        string BuildingData,
        Religion Religion
    ) : IRequestBase<string>;

    public class ClientRegisterOrchestratorHandler : RequestHandlerBase<Client, ClientRegisterOrchestrator, string>
    {
        public ClientRegisterOrchestratorHandler(RequestHandlerBaseParameters<Client> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<string>> Handle(ClientRegisterOrchestrator request, CancellationToken cancellationToken)
        {
            var phoneValid = await _repository.AnyAsync(c => c.Mobile == request.Mobile);
            if (!phoneValid)
            {
                var NationalNumberValid = await _repository.AnyAsync(c => c.NationalNumber == request.NationalNumber );
                if (!NationalNumberValid || request.NationalNumber.IsNullOrEmpty())
                {
                    var userIdResult = await _mediator.Send(new CreateUserCommand(
                        Name: request.Name,
                        Password: request.Password,
                        ConfirmPassword:request.ConfirmPassword,
                        Mobile: request.Mobile,
                        RoleId: Role.Client,
                        VerifyStatus: VerifyStatus.Pending,
                        JobTitle:null
                    ));

                    if (!userIdResult.IsSuccess)
                    {
                        return RequestResult<string>.Failure(userIdResult.ErrorCode);
                    }

                    var result = await _mediator.Send(new ClientRegisterCommand(
                          Name: request.Name,
                          Mobile: request.Mobile,
                          Password: request.Password,
                          ConfirmPassword: request.ConfirmPassword,
                          ID: userIdResult.Data,
                          Email: request.Email,
                          NationalNumber: request.NationalNumber,
                          Phone: request.Phone,
                          ClientActivity:request.ClientActivity,
                          Religion:request.Religion
                    ));

                    string Message = "";
                    if (!result.IsSuccess)
                    {
                        return RequestResult<string>.Failure(result.ErrorCode);
                    }
                    if (request.Paths != null && request.Paths.Any())
                    {
                        var saveMediaResult = await _mediator.Send(new SaveMediaCommand(
                            SourceId: result.Data,
                            SourceType: SourceType.Client,
                            Paths: request.Paths
                        ), cancellationToken);


                        if (!saveMediaResult.IsSuccess)
                        {

                            return RequestResult<string>.Failure(saveMediaResult.ErrorCode);
                        }
                    }
                    //Message = "Your OTP for registration is";

                    var OTPresult = await _mediator.Send(new GenerateOTPCommand(userIdResult.Data, request.Mobile));
                    //var SMSResult = await _mediator.Send(new SendMessageCommand(OTPresult.Data.OTP, request.Mobile, Message));
                    var shipping = await _mediator.Send(new CreateShippingAddressCommand(
                        GovernorateId: request.GovernorateId,
                        CityId: request.CityId,
                        Street: request.Street,
                        Landmark: request.Landmark,
                        Latitude: request.Latitude,
                        Longitude: request.Longitude,
                        ClientId: userIdResult.Data,
                        IsDefualt:true,
                        BuildingData:request.BuildingData,
                        Status:ShippingAddressStatus.Pending
                        ));

                    return RequestResult<string>.Success(OTPresult.Data.OTPtoken, "Client Registered successfully.");
                }
                else
                {
                    return RequestResult<string>.Failure(ErrorCode.ExistNationalNumber);
                }

            }
            else
            {

                return RequestResult<string>.Failure(ErrorCode.ExistMobile);
            }
        }

    }


}
