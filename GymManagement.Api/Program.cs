
using System.Net;
using DocumentFormat.OpenXml.Drawing;
using GymManagement.Api;
using GymManagement.Application;
using GymManagement.Infrastructure;
using GymManagement.Infrastructure.Authentication.TokenGenerator;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{

    builder.WebHost.UseKestrel(options => 
     options.Listen(IPAddress.Loopback, 5250));
   

    builder.Services.AddPeresentaion()
                    .AddApplication()
                    .AddInfrasturcture(builder.Configuration);

}



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddInfrastructureMiddleware();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
