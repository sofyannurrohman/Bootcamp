using BootcampDay3Generic.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Impl DI for repository
builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();

