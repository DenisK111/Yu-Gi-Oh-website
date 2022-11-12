namespace Yu_Gi_Oh_website.Web.Middleware
{
	
        public class VisitorCounterMiddleware
        {
            private readonly RequestDelegate _next;

            public VisitorCounterMiddleware(RequestDelegate requestDelegate)
            {
                _next = requestDelegate;
            }

            public async Task Invoke(HttpContext context)
            {
                
                string visitorId = context.Request.Cookies["VisitorId"];
                if (visitorId == null)
                {
                File.WriteAllText("file",$"{context.Request.Path}");

                    context.Response.Cookies.Append("VisitorId", Guid.NewGuid().ToString(), new CookieOptions()
                    {
                        Path = "/",
                        HttpOnly = true,
                        Secure = false,
                    });
                }

                await _next(context);

                
            }
        }
    
}
