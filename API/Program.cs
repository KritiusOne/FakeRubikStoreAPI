using Aplication.Interfaces;
using Aplication.Options;
using Aplication.Services;
using Infraestructure.Data;
using Infraestructure.Filters;
using Infraestructure.Mappings;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ProfileMapper));

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IUnitOfWork<>),typeof(UnitOfWork<>));
builder.Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddTransient(typeof(IBasicEndpointService<>), typeof(BasicEndpointService<>));
builder.Services.AddTransient(typeof(IUserService<>), typeof(UserService<>));
builder.Services.AddDbContext<FakeRubikStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("dev"));
});

var app = builder.Build();

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
