using AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using NLog; //NLog es una plataforma de registro para .NET que nos ayudará a crear y registrar nuestros mensajes
using Seguimiento.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config")); // logger para cargar el archivo

// Add services to the container.

builder.Services.ConfigureCors();

builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureLoggerService(); //se agrega el servicio de registro

var configuration = builder.Configuration;

builder.Services.ConfigureSqlContext(configuration); //Agregando el metodo que contiene la conexion con la DB 

builder.Services.ConfigureRepositoryManager(); //Se agrega el admin repository como servicio

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

var app = builder.Build();



// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();



   



