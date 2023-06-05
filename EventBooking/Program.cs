using BusinessEvents.DataAccess.Repository.IRepository;
using BusinessEvents.DataAccess.Repository;
using EventBooking.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BusinessEventsAPI.MappingConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB connection

builder.Services.AddDbContext<ApplicationDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));

//===============================================================================
//================== Auto mapper config =========================================

builder.Services.AddAutoMapper(typeof(MappingConfig));

//==============================================================================
//================== Dependancy Injection ======================================

builder.Services.AddScoped<IBEventRepository, BEventRepository>();
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();






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
