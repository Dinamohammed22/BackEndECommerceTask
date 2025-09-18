using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.ActiveGovernorate.Commands
{
    public record ActiveGovernorateCommand(string ID):IRequestBase<bool>;
    public class ActiveGovernorateCommandHandler : RequestHandlerBase<Governorate, ActiveGovernorateCommand, bool>
    {
        public ActiveGovernorateCommandHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(ActiveGovernorateCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Governorate governorate = new Governorate { ID = request.ID };
            governorate.IsActive = true;
            _repository.SaveIncluded(governorate, nameof(governorate.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
