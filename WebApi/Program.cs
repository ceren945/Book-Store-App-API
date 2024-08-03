using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Presentation.ActionFilters;
using Repositories.EFCore;
using Services.Contracts;
using WebApi.Extensions;
using WebApi.Extensions.WebApi.Extensions;


var builder = WebApplication.CreateBuilder(args);
LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));


    builder.Services.AddControllers(config =>{
    config.RespectBrowserAcceptHeader = true; //Api i�erik pazarl���na a��k hale getirdik (defaultu false)
    config.ReturnHttpNotAcceptable = true;//. Bu ifade, API'nin, ge�erli olmayan bir "Accept" ba�l��� g�nderildi�inde
                                          //HTTP 406 "Not Acceptable" yan�t�n� d�nd�rmesini sa�lar.
                                          
})   .AddCustomCsvFormatter()
    .AddXmlDataContractSerializerFormatters()//XML d��kt�s�n� desteklemesini sa�lad�k.
    .AddApplicationPart(typeof(Presentation.AssemblyReferance).Assembly)
    .AddNewtonsoftJson();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureActionFilters();



var app = builder.Build();
var logger=app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
