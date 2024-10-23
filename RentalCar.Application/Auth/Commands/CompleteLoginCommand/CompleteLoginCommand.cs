using MediatR;
using RentalCar.Application.Common.Dto.Auth;
using RentalCar.Application.common.Interfaces;

namespace RentalCar.Application.Auth.Commands.CompleteLoginCommand;

public record CompleteLoginCommand(string ValidationToken) : IRequest<CompleteLoginResponseDto>;

public class CompleteLoginHandler : IRequestHandler<CompleteLoginCommand, CompleteLoginResponseDto>
{

    private readonly IAuthService _authService;

    public CompleteLoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<CompleteLoginResponseDto> Handle(CompleteLoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.CompleteLoginAsync(request.ValidationToken);
    }
}