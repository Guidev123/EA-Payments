using MediatR;
using Payments.Application.DTOs;
using Payments.Application.Responses;

namespace Payments.Application.Queries.Stripe.GetTransactionByOrderCode;

public record GetTransactionByOrderCodeQuery(string Code) : IRequest<Response<List<StripeTransactionDTO>?>>;
