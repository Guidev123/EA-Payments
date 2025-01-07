﻿using Payments.Domain.DomainObjects;

namespace Payments.Domain.Entities;

public class ShoppingCart : Entity
{
    public ShoppingCart(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
}
