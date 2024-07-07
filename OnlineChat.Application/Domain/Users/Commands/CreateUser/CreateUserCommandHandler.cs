using MediatR;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Core.Domain.Users.Data;
using OnlineChat.Core.Domain.Users.Models;

namespace OnlineChat.Application.Domain.Users.Commands.CreateUser;

internal class CreateUserCommandHandler(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateUserData(request.Nickname);

        var newUser = User.Create(data);

        userRepository.Add(newUser);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newUser.Id;
    }
}
