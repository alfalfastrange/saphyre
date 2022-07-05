using MediatR;
using Microsoft.EntityFrameworkCore;
using Saphyre.Api.Infrastructure.Contexts;
using Saphyre.Api.Infrastructure.Extensions;
using Saphyre.Api.Infrastructure.IoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "DefaultPolicy",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build();
        });
});

builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddBindings(new List<IBinder>
{
    new ProviderDependencies(),
    new ValidationDependencies()
});

builder.Services.AddDbContext<SaphyreContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("SaphyreContext")));

var app = builder.Build();

app.UseCors("DefaultPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
