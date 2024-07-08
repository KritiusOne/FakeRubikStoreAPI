using Aplication.Interfaces;
using Aplication.Services;
using Infraestructure.Data;
using Infraestructure.Filters;
using Infraestructure.Mappings;
using Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var _myCors = "cors";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<GlobalExceptionFilter>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: _myCors,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();

        });
});

builder.Services.AddAutoMapper(typeof(ProfileMapper));

builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient(typeof(IUnitOfWork<>),typeof(UnitOfWork<>));
builder.Services.AddTransient(typeof(IBasicEndpointService<>), typeof(BasicEndpointService<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient(typeof(IUserService<>), typeof(UserService<>));
builder.Services.AddTransient<IUserDirectionRepoitory, UserDirectionRepository>();
builder.Services.AddScoped<IDirectionService, DirectionService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IReviewServices, ReviewServices>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();

builder.Services.AddSingleton<IUriService>(provider =>
{
    var accesor = provider.GetRequiredService<IHttpContextAccessor>();
    var request = accesor.HttpContext.Request;
    var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    Console.WriteLine(absoluteUri);
    return new UriService(absoluteUri);
});

builder.Services.AddDbContext<FakeRubikStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Prod")).LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseCors(_myCors);
app.UseAuthorization();

app.MapControllers();

app.Run();
