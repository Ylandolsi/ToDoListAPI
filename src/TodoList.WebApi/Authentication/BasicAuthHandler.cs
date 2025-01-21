using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using ToDoLIstAPi.Contracts;

namespace ToDoLIstAPi.Authentication;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IAuthService _authenticationService;
    public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock , IAuthService authenticationService) 
        : base(options, logger, encoder, clock)
    {
        _authenticationService = authenticationService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Unauthorized , Authorization header is missing");
        }

        string authorizationHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var token = authorizationHeader.Substring(6);
        // convert to Byte Array :
        // then convert to string
        var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));

        var credentials = credentialAsString.Split(":");
        if (credentials?.Length != 2)
        {
            return AuthenticateResult.Fail("Unauthorized , Review Your Credentials");
        }

        var username = credentials[0];
        var password = credentials[1];

        var userEntity = await _authenticationService.Authenticate(username, password); 

        if ( userEntity is null ) 
        {
            return AuthenticateResult.Fail("Authentication failed ( username or password is incorrect )");
        }

        var role = userEntity.Role; 
        // claim is key value pair : represent information about the authenticated user 
        // ( name , role , email , phone number , address , ... )
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userEntity.Id.ToString()), // Include the user's ID
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role) // Add the user's role as a claim ( role-based authorization )
        };
        // specify the authentication scheme ( Basic )
        // The authentication type is useful for distinguishing between different authentication methods
        var identity = new ClaimsIdentity(claims, "Basic");

        // rpresent the security context of the user ( available throughout the request pipeline ) 
        var claimsPrincipal = new ClaimsPrincipal(identity);

        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
    }
    // HandleChallengeAsync is called when auth is failed 
    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        Response.ContentType = "application/json";

        // Get the failure message from the authentication result
        var authenticateResult = await HandleAuthenticateOnceAsync();
        var failureMessage = authenticateResult?.Failure?.Message ?? "Unauthorized";

        var response = new
        {
            status = StatusCodes.Status401Unauthorized,
            message = failureMessage
        };

        await Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}
