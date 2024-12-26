using Microsoft.EntityFrameworkCore;
using DotaDashboard.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>
    (opt =>  opt.UseInMemoryDatabase("DotaDashboardDb"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Add allowed origins
                  .AllowAnyMethod() // Allow GET, POST, etc.
                  .AllowAnyHeader(); // Allow all headers
        });
});


builder.Services.AddOpenApi();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
};

app.MapControllers();
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();


app.Run();
