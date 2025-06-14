using eBlogUI.Business.Configuration;
using eBlogUI.Business.Interfaces;
using eBlogUI.Business.Services;
using eBlogUI.Business.Services.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPostApiService, PostApiManager>();
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("AuthorizedClient");
});

builder.Services.AddHttpClient("AuthorizedClient")
    .ConfigureHttpClient((provider, client) =>
    {
        var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
        var token = httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]!);
    });

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
var apiBaseUrl = builder.Configuration.GetSection("ApiSettings")["BaseUrl"];
builder.Services.AddHttpClient<IPostApiService, PostApiManager>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl!);
});
builder.Services.AddHttpClient<IPostApiService, PostApiManager>(client =>
    client.BaseAddress = new Uri(apiBaseUrl!));
builder.Services.AddHttpClient<ICategoryApiService, CategoryApiManager>(client =>
    client.BaseAddress = new Uri(apiBaseUrl!));
builder.Services.AddHttpClient<ITagApiService, TagApiManager>(client =>
    client.BaseAddress = new Uri(apiBaseUrl!));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapDefaultControllerRoute();
});
app.Run();
