using Application.ConfigureServices;
using Application.Middleware;
using Presentation.API;
using Serilog;
using Services.Concretes.AutoMapper;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Host.UseSerilog((context, configuration) => {
    configuration.ReadFrom.Configuration(context.Configuration);
});

services.ConfigureSqlContext(builder.Configuration);
services.ConfigureMailSettings(builder.Configuration);
services.AddDataProtection();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddAutoMapper(typeof(DtoMappingProfile));
services.AddAutoMapper(typeof(ModelMappingProfile));
services.RegisterServices();
services.RegisterRepositories();
services.RegisterOtherDependencies();
services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
services.ConfigureCors();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();