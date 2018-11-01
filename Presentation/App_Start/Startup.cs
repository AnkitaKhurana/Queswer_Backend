using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Presentation.AuthHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Cors;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(Presentation.App_Start.Startup))]

namespace Presentation.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Use(typeof(HeaderSetter));
            app.UseCors(CorsOptions.AllowAll);
            
            var myProvider = new AuthorizationServerProvider();
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                Provider = myProvider,
                RefreshTokenProvider = new RefreshTokenProvider()

            };

            app.UseOAuthAuthorizationServer(options);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}