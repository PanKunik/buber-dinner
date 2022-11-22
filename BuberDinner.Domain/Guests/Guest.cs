using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Guests.ValueObjects;

namespace BuberDinner.Domain.Guests;

public sealed class Guest : AggregateRoot<GuestId>
{
    public Guest(GuestId id) : base(id)
    {
    }
}