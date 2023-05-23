using CustomerApi.Data.Database;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Messaging.Send.Options.v1;
using CustomerApi.Messaging.Send.Sender.v1;
using CustomerApi.Service.v1.Command;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHealthChecks();
builder.Services.AddOptions();

var rabbitMqServiceConfig = builder.Configuration.GetSection("RabbitMq");
builder.Services.Configure<RabbitMqConfiguration>(rabbitMqServiceConfig);

builder.Services.AddControllers();
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

builder.Services.AddSingleton<ICustomerUpdateSender,CustomerUpdateSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
