using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Database;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using OrderApi.Messaging.Receive.Options.v1;
using OrderApi.Messaging.Receive.Receiver.v1;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Query;
using OrderApi.Service.v1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddOptions();

var rabbitMqServiceConfig = builder.Configuration.GetSection("RabbitMq");
builder.Services.Configure<RabbitMqConfiguration>(rabbitMqServiceConfig);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo {
        Version = "v1",
        Title="Order Api",
        Description="A sample api to create/update orders",
        Contact=new Microsoft.OpenApi.Models.OpenApiContact { 
          Name="Shaiju Rajan"
        }

    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(c => { c.RegisterServicesFromAssemblies(typeof(CreateOrderCommand).Assembly); });
builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDbContext<OrderContext>(options => { options.UseInMemoryDatabase("OrdersDB"); });



builder.Services.AddHostedService<CustomerUpdateReceiver>();
builder.Services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

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
