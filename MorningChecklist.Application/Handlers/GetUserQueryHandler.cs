using AutoMapper;
using MediatR;
using MorningChecklist.Application.Queries;
using MorningChecklist.Application.Services.Interfaces;
using MorningChecklist.Domain.Models;

namespace MorningChecklist.Application.Handlers
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IMapper mapper,
            IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<UserModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = _userService.GetUserInfo(request.UserId);
            if (user == null)
            {
                throw new Exception($"User not found with ID {request.UserId}");
            }

            var model = _mapper.Map<UserModel>(user);
            return model;
        }
    }
}
