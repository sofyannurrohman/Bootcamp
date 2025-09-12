using MyApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// DI implementation for IUserRepository with InMemoryUserRepository
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>(); //Whenever something asks for IUserRepository, give them a UserRepository

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapControllers();

app.Run();

/* Personal Note :
Flow to create of this Api:
Define a contract / interface in IUserRepository
            |
            v
Define a class for implementation of contract in UserRepository (ex: comunicate with db creating crud)
            |
            v
Controller will Dependency Inject the Contract and use what it need instead inherit of contract
            |
            v
Program.cs Will handle the registration of IUserRepository and UserRepository

*/
