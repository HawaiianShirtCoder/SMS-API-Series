using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SMS.Shared.BLL;
using SMS.Shared.DAL;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var adminConnection = builder.Configuration.GetConnectionString("DefaultConnection");

// JWT Authentication setup
var key = Encoding.ASCII.GetBytes("YourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKeyYourSuperSecretKey");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "StanwayRonnies",
            ValidateAudience = true,
            ValidAudience = "StanwayRonnies",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // NOTE: serialize enums as strings in api responses (e.g. HOME OR AWAY)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IDataAccess, DapperDataAccess>();
builder.Services.AddScoped<IDataAccess, SqlLiteDapperDataAccess>();
builder.Services.AddScoped<ISMSLogic, SMSLogic>();

var app = builder.Build();

app.UseCors(options => options.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
