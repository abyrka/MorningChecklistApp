using MediatR;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Application.Queries
{
    public class GetUserQuery : IRequest<UserModel>
    {
        public int UserId { get; set; }
    }
}
