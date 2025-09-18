using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Classifications.UpdateClassification.Commands;

public record UpdateClassificationCommand(string ID, string Name) : IRequestBase<bool>;

public class UpdateClassificationCommandHandler : RequestHandlerBase<Classification, UpdateClassificationCommand, bool>
{
    public UpdateClassificationCommandHandler(RequestHandlerBaseParameters<Classification> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(UpdateClassificationCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        Classification classification=new Classification { ID = request.ID };
        classification.Name = request.Name;
        _repository.SaveIncluded(classification, nameof(classification.Name));
        _repository.SaveChanges();
        var result = RequestResult<bool>.Success(true);

        return await Task.FromResult(result);
    }
}
