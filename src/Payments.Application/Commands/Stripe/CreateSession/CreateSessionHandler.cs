using MediatR;
using Payments.Application.Responses;
using Payments.Application.Services;
using Payments.Domain.Repositories;

namespace Payments.Application.Commands.Stripe.CreateSession;

public sealed class CreateSessionHandler(IStripeService stripeService,
                                         IUserService userService,
                                         IPaymentRepository paymentRepository)
                                       : IRequestHandler<CreateSessionCommand, Response<CreateSessionResponse>>
{
    private readonly IStripeService _stripeService = stripeService;
    private readonly IUserService _userService = userService;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
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
