using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using APIdep;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDependencies();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Тестовое задание Teledoc (Спектор Александр Евгеньевич)",
        Description = "API для управления клиентами и учредителями. Реализована связь 'один ко многим'. " +
                      "При индивидуальном предпринимательстве возможно только одно учредительство. " +
                      "Соблюдены принципы SOLID, REST API, KISS, DI, CQRS и DRY. " +
                      "Каждый файл содержит в среднем 90 строк кода. СУБД - MSSQL. " +
                      "Специальная валидация свойств в DTO, а так же по умолчанию для строковых: trim и проверка на null." +
                      "В приложении остутсвуют предупреждения компилятора." + 
                      "Присутсвутют модульные тесты",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<TSteledocDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<TSteledocDbContext>();
        context.Database.Migrate();
        logger.LogInformation("Successful migration");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error during migration");
    }
}

app.MapControllers();
app.Run();
