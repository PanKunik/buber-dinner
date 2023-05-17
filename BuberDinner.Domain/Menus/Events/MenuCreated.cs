using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Menus.Events;

public record MenuCreated(Menu Menu) : IDomainEvent;