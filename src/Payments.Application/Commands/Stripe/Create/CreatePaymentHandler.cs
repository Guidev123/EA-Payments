using MediatR;
using Payments.Application.Mappers;
using Payments.Application.Responses;
using Payments.Application.Services;
using Payments.Domain.Repositories;

namespace Payments.Application.Commands.Stripe.Create;

public sealed class CreatePaymentHandler(IStripeService stripeService,
                                         IUserService userService,
                                         IPaymentRepository paymentRepository,
                                         IUnitOfWork unitOfWork)
                                       : IRequestHandler<CreatePaymentCommand, Response<CreatePaymentResponse>>
{
    private readonly IStripeService _stripeService = stripeService;
    private readonly IUserService _userService = userService;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Response<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var customerId = _userService.GetUserId();
        var customerEmail = _userService.GetUserEmail();
        if (customerId is null || customerEmail is null) return new(null, 404);
        request.SetCustomerCredentials(customerId.Value, customerEmail);

        var session = await _stripeService.CreateSessionAsync(request);
        if (string.IsNullOrEmpty(session)) return new(null, 400);

        await _paymentRepository.CreateAsync(request.MapToEntity());

        return await _unitOfWork.CompleteAsync() > 0
            ? new(new(session), 200)
            : new(null, 400);
    }
}
