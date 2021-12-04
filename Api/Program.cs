using MassTransit;
using MassTransit.Example.Components.Consumers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<SubmitOrderConsumer>();
});
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

app.UseAuthorization();

app.MapControllers();

app.Run();
