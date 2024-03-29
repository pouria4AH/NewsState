using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.EntityFrameworkCore;
using NewsState.Application.Services.Implementations;
using NewsState.Application.Services.interfaces;
using NewsState.DataLayer;
using NewsState.DataLayer.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBlogServices, BlogServices>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

#region config database
builder.Services.AddDbContext<NewsStateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Context")));
#endregion

#region html encoder

builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[]
{
    UnicodeRanges.BasicLatin , UnicodeRanges.Arabic
}));

#endregion

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

app.Run();
