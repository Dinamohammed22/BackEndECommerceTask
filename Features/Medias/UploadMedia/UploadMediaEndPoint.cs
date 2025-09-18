using KOG.ECommerce.Common.Endpoints;
using Microsoft.AspNetCore.Mvc;
using Roboost.Common.Views;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Middlewares;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Features.Medias.UploadMedia.Commands;

namespace KOG.ECommerce.Features.Medias.UploadMedia
{
    public class UploadMediaEndPoint : EndpointBase<UploadMediaRequestViewModel, UploadMediaResponseViewModel>
    {
        public UploadMediaEndPoint(EndpointBaseParameters<UploadMediaRequestViewModel> dependencyCollection) : base(dependencyCollection)
        {
        }

        [HttpPost]
        //[TypeFilter(typeof(CustomizedAuthorizeAttribute), Arguments = new object[] { Feature.UploadMedia })]
        public async Task<EndPointResponse<UploadMediaResponseViewModel>> UploadMedia([FromForm] UploadMediaRequestViewModel viewModel)

        {
            var validationResult = await ValidateRequestAsync(viewModel);

            if (!validationResult.IsSuccess)
                return validationResult;

            // Send the command to upload the files
            var result = await _mediator.Send(viewModel.MapOne<UploadMediaCommand>());

            // Convert the list of file paths to response view model
            var response = new UploadMediaResponseViewModel(result.Data);

            if (result.IsSuccess)
                return EndPointResponse<UploadMediaResponseViewModel>.Success(response, "Files uploaded successfully");
            else
                return EndPointResponse<UploadMediaResponseViewModel>.Failure(result.ErrorCode);
        }
    }
}
