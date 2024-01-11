using Squadmakers.Application.Interfaces;
using Squadmakers.Application.Repositories;
using Squadmakers.Application.Services;
using Squadmakers.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressInferBindingSourcesForParameters = true;
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddXmlSerializerFormatters();

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddDbContextSquadmakers(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();

    DirectoryInfo dir = new(AppDomain.CurrentDomain.BaseDirectory);
    foreach (FileInfo fi in dir.EnumerateFiles("*.xml"))
    {
        x.IncludeXmlComments(fi.FullName, true);
        x.EnableAnnotations();
    }
});

builder.Services.AddScoped(typeof(IRepositoryJoke<>), typeof(RepositoryJoke<>));
builder.Services.AddScoped<IJokeService, ChuckNorrisService>();
builder.Services.AddScoped<IJokeService, DadService>();
builder.Services.AddTransient<IJokeServiceFactory, JokeServiceFactory>();

var app = builder.Build();

app.ApplyDatabaseMigrationsSiguda(builder.Configuration);

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
