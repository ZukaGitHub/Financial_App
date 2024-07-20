using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Persistance;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Infrastructure.JWT;
using Microsoft.OpenApi.Models;
using Presentation.AutoMapper;
using Domain;
using Persistance.UnitOfWork;
using Infrastructure.ValidationService;
using Infrastructure.FileManager;


Assembly presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
Assembly applicationAssembly = typeof(Application.AssemblyReference).Assembly;
Assembly domainAssembly = typeof(Domain.AssemblyReference).Assembly;
Assembly infrastructureAssembly = typeof(Infrastructure.AssemblyReference).Assembly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddDbContext<FinancialAppDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpClient<ValidationService>();
builder.Services.AddScoped<IFileManager>(provider =>
    new FileManager(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")));
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

}).AddEntityFrameworkStores<FinancialAppDBContext>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultAuthenticateScheme =
    options.DefaultForbidScheme =
    options.DefaultSignInScheme =
    options.DefaultScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtOptions =>
{
    JwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience= true,
        ValidAudience= builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey=true,
        IssuerSigningKey=new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SignInKey"])

            )
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminClaim", policy =>
        policy.RequireClaim("AdminClaim", "true"));

    options.AddPolicy("RequireUserClaim", policy =>
        policy.RequireClaim("UserClaim", "true"));
});

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationAssembly));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiler));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
