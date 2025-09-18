using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Enums;
using KOG.ECommerce.Models.Medias;
using MediatR.Wrappers;

namespace KOG.ECommerce.Features.Medias.UploadMedia.Commands;

public record UploadMediaCommand(List<IFormFile> Files) : IRequestBase<List<string>>;


public class UploadMediaCommandHandler : RequestHandlerBase<Media, UploadMediaCommand, List<string>>
{
    public UploadMediaCommandHandler(RequestHandlerBaseParameters<Media> requestParameters) : base(requestParameters)
    {
    }

    public async override Task<RequestResult<List<string>>> Handle(UploadMediaCommand request, CancellationToken cancellationToken)
    {
        if (request.Files == null || !request.Files.Any())
        {
            return RequestResult<List<string>>.Failure(ErrorCode.NotFound);
        }

        // Define the path to save the media files
        string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Media");

        // Ensure the directory exists
        if (!Directory.Exists(fullPath))
        {
            Directory.CreateDirectory(fullPath);
        }

        var resultPaths = new List<string>();

        foreach (var file in request.Files)
        {
            if (file.Length == 0) continue;

            // Generate a unique file name
            string name = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(fullPath, name);

            // Save the file to disk
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                // Add the relative path to the result list
                resultPaths.Add("Media/" + name);
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during file saving
                return RequestResult<List<string>>.Failure(ErrorCode.None);
            }
        }

        // Properly return the success result
        return RequestResult<List<string>>.Success(resultPaths);
    }
}