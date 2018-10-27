using BusinessLogic.Logic;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Presentation.AuthHelpers
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string email = context.Parameters.Where(f => f.Key == "email").Select(f => f.Value).SingleOrDefault()[0];
            context.OwinContext.Set<string>("email", email);
            context.Validated(); 
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {

            // Change authentication ticket for refresh token requests  
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });


            string email = context.OwinContext.Get<string>("email");

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            UserLogic userLogic = new UserLogic();

            //Authenticate the user credentials
            if (userLogic.Find(email, context.Password)!=null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Email,email));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided email and password is incorrect");
                return;
            }
        }
    }
}