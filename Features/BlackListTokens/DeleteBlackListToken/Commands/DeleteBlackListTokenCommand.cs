using KOG.ECommerce.Common.Enums;
using KOG.ECommerce.Common.Requests;
using KOG.ECommerce.Models.BlackListTokens;

namespace KOG.ECommerce.Features.BlackListTokens.DeleteBlackListToken.Commands
{
    public record DeleteBlackListTokenCommand(string ID):IRequestBase<bool>;
    public class DeleteBlackListTokenCommandHandler : RequestHandlerBase<BlackListToken, DeleteBlackListTokenCommand, bool>
    {
        public DeleteBlackListTokenCommandHandler(RequestHandlerBaseParameters<BlackListToken> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<RequestResult<bool>> Handle(DeleteBlackListTokenCommand request, CancellationToken cancellationToken)
        {
            var check = await _repository.AnyAsync(b => b.ID == request.ID);
            if (!check)
                return RequestResult<bool>.Failure(ErrorCode.NotFound);

            _repository.Delete(request.ID);
            _repository.SaveChanges();
            return await Task.FromResult(RequestResult<bool>.Success(true));
        }
    }
}
