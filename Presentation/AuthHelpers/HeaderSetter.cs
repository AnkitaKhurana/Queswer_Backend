using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Presentation.AuthHelpers
{
    public class HeaderSetter : OwinMiddleware
    {
        public HeaderSetter(OwinMiddleware next)
       : base(next)
        {
        }
        public override async Task Invoke(IOwinContext context)
        {
            context.Response.Headers["Access-Control-Allow-Origin"] = "*";
            context.Response.Headers["Access-Control-Allow-Headers"] = "*";
            context.Response.Headers["Access-Control-Allow-Methods"] = "*";
            await Next.Invoke(context);
        }
    }
}