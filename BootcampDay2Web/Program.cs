using Interfaces.IUserAuth;
using Services.UserService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IUserAuth, UserService>();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles(); // replace MapStaticAssets if not custom
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Register}/{id?}");

app.Run();
