using Authentication.Provider;
using Authentication.Provider.KeyProvider;
using Authentication.Provider.SignUpProvider;
using Authentication.Services;
using Authentication.Services.SignUpService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISignUpProvider, GeneralSignUpProvider>();
builder.Services.AddScoped<IAuthProvider, RawAuthProvider>();
//builder.Services.AddScoped<ISignUpService, GeneralSignUpService>();
builder.Services.AddScoped<ISignUpService, SecureSignUpService>();
//builder.Services.AddScoped<IAuthService, RawAuthValidatorService>();
builder.Services.AddScoped<IAuthService, SecureAuthValidatorService>();
builder.Services.AddScoped<IKeyProvider, KeyProvider>();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
