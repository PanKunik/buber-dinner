using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);