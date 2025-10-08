using BootcampDay23AdvancedReading.Repository;
using BootcampDay23AdvancedReading.Services;
using Microsoft.EntityFrameworkCore;
using BootcampDay23AdvancedReading.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Optional: disable this if you don't need HTTPS redirection locally
    // app.UseHttpsRedirection();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");
app.MapGet("/", context =>
{
    context.Response.Redirect("/Tasks");
    return Task.CompletedTask;
});

app.Run();
