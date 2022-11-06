using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Guest.ValueObjects;

namespace BuberDinner.Domain.Guest;

public sealed class Guest : AggregateRoot<GuestId>
{
    public Guest(GuestId id) : base(id)
    {
    }
}