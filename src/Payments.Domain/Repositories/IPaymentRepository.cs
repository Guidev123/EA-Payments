﻿using Payments.Domain.Entities;

namespace Payments.Domain.Repositories;

public interface IPaymentRepository
{
    Task<List<Payment>> GetAllAsync(int pageNumber, int pageSize);
    Task<Payment?> GetByIdAsync();
    Task CreateAsync(Payment payment);
    void Update(Payment payment);
}