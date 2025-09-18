using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Data;
using KOG.ECommerce.Features.Governorates.ActiveGovernorate.Commands;
using KOG.ECommerce.Models.Governorates;

namespace KOG.ECommerce.Features.Governorates.DeactiveGovernorate.Commands
{
    public record DeactiveGovernorateCommand(string ID) : IRequestBase<bool>;
    public class DeactiveGovernorateCommandHandler : RequestHandlerBase<Governorate, DeactiveGovernorateCommand, bool>
    {
        public DeactiveGovernorateCommandHandler(RequestHandlerBaseParameters<Governorate> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeactiveGovernorateCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);
            Governorate governorate = new Governorate { ID = request.ID };
            governorate.IsActive = false;
            _repository.SaveIncluded(governorate, nameof(governorate.IsActive));
            _repository.SaveChanges();
            var result = RequestResult<bool>.Success(true);
            return await Task.FromResult(result);
        }
    }
}
