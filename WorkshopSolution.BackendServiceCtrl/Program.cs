using Microsoft.EntityFrameworkCore;
using WorkshopSolution.BusinessLogic;
using WorkshopSolution.BusinessLogic.Mappings;
using WorkshopSolution.Persistence;
using WorkshopSolution.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("WorkshopDatabase");
ArgumentNullException.ThrowIfNull(connectionString, "Connection string is null");
//builder.Services.AddDbContext<WorkshopDbContext>(options => options.UseMongoDB(connectionString, "WorkshopDatabase"));
builder.Services.AddDbContext<WorkshopDbContext>(options => options.UseCosmos(connectionString, "WorkshopDatabase"));

builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IRackRepository, RackRepository>();
builder.Services.AddScoped<IRackManager, RackManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddAuthentication("Basic").AddScheme<BasicAuthOptions, BasicAuthHandler>("Basic", null);

// alternative mit JWT
//builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
//{
//  options.TokenValidationParameters.ValidateAudience = false; 

//  options.Events.OnTokenValidated = async context =>
//  {
//    var userContext = context.HttpContext.RequestServices.GetRequiredService<IUserContext>();
//    var claims = context.Principal.Claims;
//    var rackClaim = claims.FirstOrDefault(c => c.Type == "rack");
//    //if (rackClaim != null)
//    //{
//    //  userContext.RackClaim = rackClaim.Value;
//    //}
//    //var roleClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
//    //if (roleClaim != null)
//    //{
//    //  userContext.RoleClaim = roleClaim.Value;
//    //}
//    await Task.CompletedTask;
//  };
//});

builder.Services.AddAuthorization(configure =>
{
  configure.AddPolicy("RackUser", policy =>
  {
    policy
      .RequireAuthenticatedUser()
      .RequireClaim("rack", ["read", "change"])
      .RequireRole("user")
      .Build();
  });
  configure.AddPolicy("DeleteRackUser", policy =>
  {
    policy
      .RequireAuthenticatedUser()
      .RequireClaim("rack", ["delete"])
      .RequireRole("user")
      .Build();
  });
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
