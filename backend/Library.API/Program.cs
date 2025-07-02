using Library.API.DI;
using Library.API.Middlewares;
using Library.Application.DI;
using Library.Persistence.DI;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();
    
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Configuration.AddEnvironmentVariables();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Services.AddControllers();


builder.Services.AddAuthorization(builder.Configuration);
builder.Services.AddAuthorizationPolicies();
builder.Services.AddAutoMapper();
builder.Services.AddServices();
builder.Services.AddValidators();
builder.Services.AddRepositories();
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddSwagger();

    
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseExceptionHandling(); 
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();