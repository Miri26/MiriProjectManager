using Microsoft.EntityFrameworkCore;
using MiriProjectManager.Server.Data;
using MiriProjectManager.Server.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LocalDBContext>(options =>
    options.UseInMemoryDatabase("ProjectManager"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost50032",
        policy => policy.WithOrigins("https://localhost:50032")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowLocalhost50032");
app.UseMiddleware<JwtMiddleware>();
app.UseDefaultFiles();
app.UseStaticFiles();

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

app.MapFallbackToFile("/index.html");

app.Run();
