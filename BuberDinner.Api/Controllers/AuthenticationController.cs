using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authenticationResult = _authenticationService.Register(request.FirstName,
                                                                   request.LastName,
                                                                   request.Email,
                                                                   request.Password);

        var response = new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);

        return Ok(response);
    }

    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authenticationResult = _authenticationService.Login(request.Email,
                                                                request.Password);

        var response = new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token);
            
        return Ok(response);
    }
}
