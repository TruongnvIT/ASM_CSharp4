using C_4_Buoi1_MVC.Data.CShap4DbContext;
using C_4_Buoi1_MVC.Data.Identity;
using C_4_Buoi1_MVC.Models;
using C_4_Buoi1_MVC.Repositories.IService;
using C_4_Buoi1_MVC.Repositories.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession(options =>
{
    // Configure session options here
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddTransient<IBillDetailService, BillDetailService>();
builder.Services.AddTransient<IBillService, BillService>();
builder.Services.AddTransient<ICartDetailService, CartDetailService>();
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IUserService, UserService>();
// Add services to the container.
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}
)
    //.AddOpenIdConnect("oidc", options =>
    //{
    //    // Thiết lập các thông tin đăng xuất
    //    options.SignedOutCallbackPath = "/signout-callback-oidc";
    //    options.SignOutScheme = "Cookies";
    //    options.RemoteSignOutPath = "/signout-google";
    //    options.SignedOutRedirectUri = "/";
    //})
    
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddGoogle(options =>
       {
           IConfigurationSection googleAuthNSection =
           builder.Configuration.GetSection("Authentication:Google");
           options.ClientId = googleAuthNSection["ClientId"];
           options.ClientSecret = googleAuthNSection["ClientSecret"];
           options.AuthorizationEndpoint += "?prompt=consent"; // thêm prompt consent để yêu cầu xác thực người dùng
           options.AccessType = "offline"; // yêu cầu truy cập offline để có thể lấy refresh token
           options.SaveTokens = true;
       });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}");//Trang bắt đầu sau Layout
    //pattern: "{controller=Account}/{action=Login}");//Trang bắt đầu sau Layout

app.Run();
