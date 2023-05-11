using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NuGet.Configuration;
using StudentsApi.Dataccess.autoMapperProfile;
using StudentsApi.Dataccess.Data;
using StudentsApi.Dataccess.DIDbInilizer;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsApi.Dataccess.TokenService;
using StudentsApi.Model;
using StudentsApi.Model.ViewModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
/*{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme =JwtBearerDefaults.AuthenticationScheme.ToLowerInvariant(),
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
  
}*/
);
builder.Services.AddDbContext<ApplicatioDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicatioDbContext>();

builder.Services.AddScoped<IUnitOfWrok, UnitOfWrok>();
builder.Services.AddScoped<IDbInilizer, DbInilizer>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o =>
  {
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;//true
    o.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      ValidateIssuer = true,//false
      ValidateAudience = true,//false
      ValidateLifetime = true,
      ValidIssuer =builder.Configuration["JWT:Issuer"],
      ValidAudience = builder.Configuration["JWT:Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
  });

builder.Services.AddMvcCore();
      /*  .AddMvcOptions(options =>
        {
            // Mark all endpoints with the default policy
            options.Filters.Add(new AuthorizeFilter("Default"));
        });*/

builder.Services.AddAuthorization();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Resources")),
    RequestPath = "/Resources"
});
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
   // app.UseStaticFiles();

seedDatabase();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
void seedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var DbIntilizer = scope.ServiceProvider.GetRequiredService<IDbInilizer>();
        DbIntilizer.intials();
    }
}