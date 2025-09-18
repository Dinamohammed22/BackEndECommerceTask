using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Features.Common.Companies.Queries;
using KOG.ECommerce.Helpers;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Classifications.DeleteClassification.Commands;

public record DeleteClassificationCommand(string ID) : IRequestBase<bool>;

public class DeleteClassificationCommandHandler : RequestHandlerBase<Classification, DeleteClassificationCommand, bool>
{
    public DeleteClassificationCommandHandler(RequestHandlerBaseParameters<Classification> parameters) : base(parameters) { }

    public async override Task<RequestResult<bool>> Handle(DeleteClassificationCommand request, CancellationToken cancellationToken)
    {
        var check = await _repository.AnyAsync(b => b.ID == request.ID);
        if (!check)
            return RequestResult<bool>.Failure(ErrorCode.NotFound);
        var checkResult = await _mediator.Send(request.MapOne<CheckCompanyHasClassificationQuery>());
        if (!checkResult.Data)
        {
            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
        var result = RequestResult<bool>.Failure(ErrorCode.CannotDelete);

        return await Task.FromResult(result);
    }
}
