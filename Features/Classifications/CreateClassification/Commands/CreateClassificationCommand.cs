using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Classifications;

namespace KOG.ECommerce.Features.Classifications.CreateClassification.Commands;

public record CreateClassificationCommand(string Name) : IRequestBase<bool>;
public class CreateClassificationCommandHandler : RequestHandlerBase<Classification, CreateClassificationCommand, bool>
{
    public CreateClassificationCommandHandler(RequestHandlerBaseParameters<Classification> parameters) : base(parameters)
    { }

    public async override Task<RequestResult<bool>> Handle(CreateClassificationCommand request, CancellationToken cancellationToken)
    {
        Classification classification = new Classification { Name = request.Name };
        _repository.Add(classification);
        _repository.SaveChanges();

        var result = RequestResult<bool>.Success(true);

        return await Task.FromResult(result);
    }
}


