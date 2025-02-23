using Microsoft.EntityFrameworkCore;
using GymManagement;
using GymManagement.Repositories;
using GymManagementx;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "GymManagementAPI", Version = "v1" });
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddScoped<MembersRepository>();
builder.Services.AddScoped<Member_SubscriptionsRepository>();
builder.Services.AddScoped<SubscriptionsRepository>();
builder.Services.AddScoped<DiscountsRepository>();
builder.Services.AddScoped<Discounted_Member_SubscriptionsRepository>();


var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymManagementAPI V1");
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();