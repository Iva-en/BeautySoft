using BeautySoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// BD (misma cadena que usas en WebUI)
builder.Services.AddDbContext<BeautySoftDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BeautySoftConnection")));

// CORS básico para permitir llamadas desde WebUI
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowWebUI", policy =>
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowWebUI");
app.MapControllers();

app.Run();
