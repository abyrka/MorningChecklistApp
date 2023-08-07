using MediatR;
using MorningChecklist.Application.Commands;
using MorningChecklist.Application.Services.Interfaces;

namespace MorningChecklist.Application.Handlers
{
    public class CompleteMorningChecklistItemCommandHandler : IRequestHandler<CompleteMorningChecklistItemCommand>
    {
        private readonly IUserMorningChecklistService _userMorningChecklistService;

        public CompleteMorningChecklistItemCommandHandler(IUserMorningChecklistService userMorningChecklistService)
        {
            _userMorningChecklistService = userMorningChecklistService;
        }

        public Task Handle(CompleteMorningChecklistItemCommand request, CancellationToken cancellationToken)
        {
            _userMorningChecklistService.CompleteUserMorningChecklistItem(request.UserId, request.MorningChecklistItemId);
            return Task.CompletedTask;
        }
    }
}
