using Hudl.Weather.Config;
using Hudl.Weather.Services;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
if (bool.TryParse(builder.Configuration["UseStub"], out var useStub) && useStub)
{
    builder.Services.AddSingleton<IWeatherGatewayService, StubWeatherGateway>();
}
else
{
    builder.Services.AddOptions<WeatherGatewayOption>().BindConfiguration(configSectionPath:"WeatherGateway");
    builder.Services.AddSingleton<IWeatherGatewayService, WeatherGatewayService>();
}

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath | HttpLoggingFields.RequestMethod;
});

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();