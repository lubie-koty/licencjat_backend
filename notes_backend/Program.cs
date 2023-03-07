using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using notes_backend.Data;
using notes_backend.Entities.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("NotesAppDb"));
    }
);
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DataContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCors(options => options.AddPolicy(
    name: "NotesOrigins",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("NotesOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
