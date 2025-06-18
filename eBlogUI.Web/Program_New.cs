using eBlogUI.Business.Configuration;
using eBlogUI.Business.Interfaces;
using eBlogUI.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// API Configuration
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// HTTP Client with Authorization
builder.Services.AddHttpClient("AuthorizedClient", (provider, client) =>
{
    var apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>();
    client.BaseAddress = new Uri(apiSettings?.BaseUrl ?? "https://localhost:7290");
    
    // Token'ı HttpContext'ten al ve Authorization header'a ekle
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
    var context = httpContextAccessor.HttpContext;
    
    if (context != null)
    {
        var token = context.Session.GetString("AuthToken") ?? context.Request.Cookies["AuthToken"];
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
});

// HttpClient Factory için named client'ı scoped olarak register et
builder.Services.AddScoped(sp =>
{
    var factory = sp.GetRequiredService<IHttpClientFactory>();
    return factory.CreateClient("AuthorizedClient");
});

// Business Services
builder.Services.AddScoped<IPostApiService, PostApiManager>();
builder.Services.AddScoped<ICategoryApiService, CategoryApiManager>();
builder.Services.AddScoped<ITagApiService, TagApiManager>();
builder.Services.AddScoped<IAuthApiService, AuthApiManager>();
builder.Services.AddScoped<IAdminDashboardApiService, AdminDashboardApiManager>();

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

// Admin Area Route
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

// Default Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
