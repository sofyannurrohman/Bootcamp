using BootcampDay3Generic.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Impl DI for repository
builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

/* Personal Note: 
> No code duplication → one DTO works for all types (User, Product, etc.).
> Type safety → ResponseDto<User> ensures Data is always a User.
> Easier maintenance → only modify one DTO if structure changes.
👉 using generic DTOs is a best practice in ASP.NET Core, especially for standardized API responses or paginated results
*/