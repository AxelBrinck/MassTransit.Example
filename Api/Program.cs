using Components.Consumers;
using MassTransit;
using MessageContracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(configuration =>
{
    configuration.AddConsumer<SubmitOrderConsumer>();
    configuration.AddRequestClient<ISubmitOrder>();
    configuration.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
    });
});
builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
