using MediatR;

namespace MorningChecklist.Application.Commands
{
    public class CompleteMorningChecklistItemCommand : IRequest
    {
        public int UserId { get; set; }

        public int MorningChecklistItemId { get; set; }
    }
}
