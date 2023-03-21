using C_4_Buoi1_MVC.ViewModel;

namespace C_4_Buoi1_MVC.Repositories.Service
{
    public class CheckLogOut
    {
        private readonly RequestDelegate _next;

        public CheckLogOut(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path;
            // Kiểm tra xem trang hiện tại đã là trang đăng nhập hay chưa
            if (path.StartsWith("/Account", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }
            if (context.Session.IsAvailable)
            {
                var user = context.Session.GetObject<GoogleUserInfoVM>("UserLogin");
                // Kiểm tra nếu email null lại redirect về trang đăng nhập tiếp
                if (user == null)
                {
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }

            await _next(context);
        }
    }
}
