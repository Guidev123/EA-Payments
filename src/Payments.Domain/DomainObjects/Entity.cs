﻿using Payments.Domain.Events;

namespace Payments.Domain.DomainObjects;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
        UpdatedAt = DateTime.Now;
        IsDeleted = false;
    }

    public Guid Id { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; }

    protected void SetAsDeleted() => IsDeleted = true;  

    private List<IDomainEvent> _events = [];

    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    public void AddEvent(IDomainEvent @event)
    {
        if (@event == null) throw new ArgumentNullException(nameof(@event));

        _events.Add(@event);
    }

    public void RemoveEvent(IDomainEvent @event)
    {
        ArgumentNullException.ThrowIfNull(@event);

        _events.Remove(@event);
    }

    public void ClearAllEvents() => _events.Clear();

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (compareTo is null) return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
            return true;
        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

    public override string ToString() => $"{GetType().Name} [Id={Id}]";
}
