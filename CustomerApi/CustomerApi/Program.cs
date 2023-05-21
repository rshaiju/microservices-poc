using CustomerApi.Data.Database;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Models.v1;
using CustomerApi.Service.v1.Command;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks();
builder.Services.AddOptions();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Cutsomer Api",
        Description = "A sample api to create/update customers",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Shaiju Rajan"
        }

    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(c => { c.RegisterServicesFromAssemblies(typeof(CreateCustomerCommand).Assembly); });
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddDbContext<CustomerContext>(options=> { options.UseInMemoryDatabase("CustomerDB"); });

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
