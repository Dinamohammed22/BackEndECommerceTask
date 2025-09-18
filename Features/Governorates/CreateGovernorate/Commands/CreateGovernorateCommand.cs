using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.CreateGovernorate.Commands;

public record CreateGovernorateCommand(string Name, string GovernorateCode, bool IsActive) : IRequestBase<string>;
public class CreateGovernorateCommandHandler : RequestHandlerBase<Governorate, CreateGovernorateCommand, string>
{
    public CreateGovernorateCommandHandler(RequestHandlerBaseParameters<Governorate> parameters) : base(parameters)
    { }

    public async override Task<RequestResult<string>> Handle(CreateGovernorateCommand request, CancellationToken cancellationToken)
    {
        Governorate governorate = new Governorate { Name = request.Name,GovernorateCode=request.GovernorateCode ,IsActive = request.IsActive};
        _repository.Add(governorate);
        _repository.SaveChanges();

        var result = RequestResult<string>.Success(governorate.ID);

        return await Task.FromResult(result);
    }
}
