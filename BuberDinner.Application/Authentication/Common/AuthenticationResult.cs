using BuberDinner.Domain.Users;

namespace BuberDinner.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);