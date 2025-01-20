using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace ToDoLIstAPi.Authentication;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Unauthorized");
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
            return AuthenticateResult.Fail("Unauthorized");
        }

        var username = credentials[0];
        var password = credentials[1];

        if (!UserValidate.Login(username, password))
        {
            return AuthenticateResult.Fail("Authentication failed");
        }

        var role = UserValidate.GetRole(username);
        // claim is key value pair : represent information about the authenticated user 
        // ( name , role , email , phone number , address , ... )
        var claims = new[]
        {
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
}
