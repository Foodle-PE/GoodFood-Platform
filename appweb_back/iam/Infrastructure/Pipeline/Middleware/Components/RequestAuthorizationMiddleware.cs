using appweb_back.iam.Application.Internal.OutboundServices;
using appweb_back.iam.Domain.Model.Queries;
using appweb_back.iam.Domain.Services;
using appweb_back.iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace appweb_back.iam.Infrastructure.Pipeline.Middleware.Components;
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        Console.WriteLine("Entering InvokeAsync");
        var endpoint = context.Request?.HttpContext?.GetEndpoint();
        if (endpoint == null)
        {
            Console.WriteLine("Endpoint is null");
            await next(context);
            return;
        }
        
        var allowAnonymous = endpoint.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));
        if (allowAnonymous)
        {
            Console.WriteLine("Skipping authorization");
            await next(context);
            return;
        }
        Console.WriteLine("Entering Authorization");
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (token == null) throw new Exception("Null or invalid token");
        var userId = await tokenService.ValidateToken(token);
        if (userId == null) throw new Exception("Invalid token");
        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
        var result = await userQueryService.Handle(getUserByIdQuery);
        if (result == null) throw new Exception("User not found");
        Console.WriteLine("Successfully authorized user");
        context.Items["User"] = result;
        Console.WriteLine("Continuing to next middleware");
        await next(context);
    }
}