global using Demo.Data;
global using Demo.Service;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using System.Text;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PersonDbContext>(x =>
{
    x.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 17)));
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IPersonData, PersonData>();
builder.Services.AddScoped<IAuth, Auth>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authentication header using bearer scheme (\"{token}\")",
        In = ParameterLocation.Header,
        Name = "Auth",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"

    });
    var reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"};
    var openApiScheme = new OpenApiSecurityScheme { Reference = reference };



    // Adds the auth header globally on all requests
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { openApiScheme, new string[] {} }
                });
});
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true

        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
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
