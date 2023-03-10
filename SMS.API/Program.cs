using SMS.Shared.BLL;
using SMS.Shared.DAL;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // NOTE: serialize enums as strings in api responses (e.g. HOME OR AWAY)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataAccess, DapperDataAccess>();
builder.Services.AddScoped<ISMSLogic, SMSLogic>();

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
