using Microsoft.EntityFrameworkCore;
using WorkshopSolution.BusinessLogic;
using WorkshopSolution.BusinessLogic.Mappings;
using WorkshopSolution.Persistence;
using WorkshopSolution.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("WorkshopDatabase");
ArgumentNullException.ThrowIfNull(connectionString);

builder.Services.AddDbContext<WorkshopDbContext>(opt => opt.UseMongoDB(connectionString, "WorkshopDatabase"));

builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<RackRepository>();
builder.Services.AddScoped<RackManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddAuthentication("Basic").AddScheme<BasicAuthOptions, BasicAuthHandler>("Basic", null);

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
