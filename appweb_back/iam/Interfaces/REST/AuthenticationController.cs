using System.Net.Mime;
using System.Text.Json;
using appweb_back.iam.Domain.Services;
using appweb_back.iam.Infrastructure.Pipeline.Middleware.Attributes;
using appweb_back.iam.Interfaces.ACL;
using appweb_back.iam.Interfaces.REST.Resources;
using appweb_back.iam.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace appweb_back.iam.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthenticationController(IIamContextFacade iamContextFacade,  IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-up")]
    [AllowAnonymous]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        Console.WriteLine($"Received: {JsonSerializer.Serialize(resource)}");
        if (resource == null) return BadRequest("Sign-up resource cannot be null.");

        var userId = await iamContextFacade.CreateUser(
            resource.Username,
            resource.Password,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Phone,
            resource.Role
        );

        if (userId == 0)
            return StatusCode(500, "An error occurred while creating the user and profile.");

        return Ok(new { message = "User and profile created successfully", userId });
    }
    [HttpPost("sign-in")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        if (resource == null) return BadRequest("Sign-in resource cannot be null.");
        
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        
        if (authenticatedUser.user == null || string.IsNullOrEmpty(authenticatedUser.token))
        {
            return Unauthorized("Invalid username or password.");
        }
        var authenticatedUserResource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        return Ok(authenticatedUserResource);
    }
    
    
}