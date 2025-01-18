using MediatR;
using Payments.Application.Responses;
using Payments.Application.Services;

namespace Payments.Application.Commands.Stripe.CreateSession;

public sealed class CreateSessionHandler(IStripeService stripeService,
                                         IUserService userService)
                                       : IRequestHandler<CreateSessionCommand, Response<CreateSessionResponse>>
{
    private readonly IStripeService _stripeService = stripeService;
    private readonly IUserService _userService = userService;
    public async Task<Response<CreateSessionResponse>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        var customerId = _userService.GetUserIdAsync();
        if (customerId is null) return new(null, 404);

        request.SetCustomerId(customerId.Value);

        var session = await _stripeService.CreateSessionAsync(request);

        return string.IsNullOrEmpty(session) 
            ? new(null, 400)
            : new(new(session), 201);
    }
}
