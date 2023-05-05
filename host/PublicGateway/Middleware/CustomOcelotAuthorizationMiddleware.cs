using Ocelot.Authorization;
using Ocelot.Middleware;
using System.Security.Claims;

namespace PublicGateway.Middleware
{
    public class CustomOcelotAuthorizationMiddleware
    {
        public static async Task Authorize(HttpContext httpContext, Func<Task> next)
        {
            if (ValidateScope(httpContext))
                await next.Invoke();
            else
            {
                //if (httpContext.Request.Method.Equals("OPTIONS"))
                //{
                //    await next.Invoke();
                //    //httpContext.Response.StatusCode = 200;
                //}
                //else
                //{
                httpContext.Response.StatusCode = 403;
                httpContext.Items.SetError(new UnauthorizedError($"Fail to authorize"));
                //}
            }
        }

        /// <summary>
        /// 验证Token中是否有允许的Scope
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static bool ValidateScope(HttpContext httpContext)
        {
            var downstreamRoute = httpContext.Items.DownstreamRoute();
            var scopes = downstreamRoute.AuthenticationOptions.AllowedScopes;
            if (scopes == null || scopes.Count == 0) return true;

            var result = false;
            var userClaimsPrincipals = httpContext.User.Claims.ToArray();
            foreach (var scope in scopes)
            {
                if (userClaimsPrincipals.Any(x => x.Type == "aud" && x.Value.Equals(scope)))
                    result = true;
            }
            return result;
        }
    }
}
