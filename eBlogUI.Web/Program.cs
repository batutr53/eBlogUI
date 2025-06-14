using eBlogUI.Business.Configuration;
using eBlogUI.Business.Interfaces;
using eBlogUI.Business.Services;
using eBlogUI.Business.Services.Interfaces;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// ApiSettings (BaseUrl) config binding
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
    ?? throw new InvalidOperationException("ApiSettings:BaseUrl bulunamadý!");

// Named HttpClient (AuthorizedClient) - Cookie'den token alýr ve Authorization header'a ekler
builder.Services.AddHttpClient("AuthorizedClient", (provider, client) =>
{
    var accessor = provider.GetRequiredService<IHttpContextAccessor>();
    var token = accessor.HttpContext?.Request.Cookies["AuthToken"];

    if (!string.IsNullOrEmpty(token))
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    client.BaseAddress = new Uri(apiBaseUrl);
});

// Generic HttpClient injection (AuthorizedClient)
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("AuthorizedClient");
});

// API Servisleri - BaseAddress'e gerek yok çünkü AuthorizedClient üzerinden çaðrýlýyor
builder.Services.AddScoped<IPostApiService, PostApiManager>();
builder.Services.AddScoped<ICategoryApiService, CategoryApiManager>();
builder.Services.AddScoped<ITagApiService, TagApiManager>();
builder.Services.AddScoped<IAuthApiService, AuthApiManager>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Area destekli route
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapDefaultControllerRoute();
});
app.Run();
