using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleQuizApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Quizzes")));
builder.Services.AddDbContext<UserQuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserQuizzes")));
builder.Services.AddScoped<SimpleQuizApp.Services.QuizService>();

// Configure Identity services
builder.Services.AddIdentity<SimpleQuizApp.Models.User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false; 
    options.SignIn.RequireConfirmedEmail = false; 
    options.SignIn.RequireConfirmedPhoneNumber = false; 
}).AddEntityFrameworkStores<UserQuizDbContext>()
  .AddDefaultTokenProviders();

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

app.MapControllerRoute
(   
    name: "quiz",
    pattern: "{controller=Quiz}/{action=Index}/{id?}"
);

app.Run();
