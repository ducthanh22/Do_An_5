using BUS.Interface;
using BUS;
using Microsoft.EntityFrameworkCore;
using Model;
using DAL;
using DAL.Interface;
using Microsoft.AspNetCore.Hosting;
using DTO.AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using DTO;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddDbContext<Achino_DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<Achino_DbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        // Set your token parameters
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));



builder.Services.AddScoped<ICategoriesBus, CategoriesBus>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddScoped<IProductsBus, ProductsBus>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();


builder.Services.AddScoped<IDetail_exportbillBus, Detail_exportbillBus>();
builder.Services.AddScoped<IDetail_exportbillRepository, Detail_exportbillRepository>();

builder.Services.AddScoped<IDetail_importbillBus, Detail_importbillBus>();
builder.Services.AddScoped<IDetail_importbillRepository, Detail_importbillRepository>();

builder.Services.AddScoped<IDetail_warehouseBus, Detail_warehouseBus>();
builder.Services.AddScoped<IDetail_warehouseRepository, Detail_warehouseRepository>();

builder.Services.AddScoped<IExportbillBus, EportbillBus>();
builder.Services.AddScoped<IExportbillRepository, EportbillRepository>();

builder.Services.AddScoped<IImportbillBus, ImportbillBus>();
builder.Services.AddScoped<IImportbillRepository, ImportbillRepository>();

builder.Services.AddScoped<IPriceBus, PriceBus>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();

builder.Services.AddScoped<IProducesBus, ProducesBus>();
builder.Services.AddScoped<IProducesRepository, ProducesRepository>();

builder.Services.AddScoped<IWarehouseBus, WarehouseBus>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddScoped<IOder_detailBus, Order_detailBus>();

builder.Services.AddScoped<IOrder_detailRepository, Order_detailRepository>();

builder.Services.AddScoped<IOrderBus, OrderBus>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IAccountBus, AccountBus>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IColorBus, ColorBus>();
builder.Services.AddScoped<IColorRepository, ColorRepository>();

builder.Services.AddScoped<ISendEmailBus, SendEmailBus>();
builder.Services.AddScoped<ISendEmailRepository, SendEmailRepository>();

builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerRole", policy => policy.RequireRole("Customer"));
    options.AddPolicy("AdminRole", policy => policy.RequireRole("admin"));
    // Thêm các policy khác n?u c?n thi?t
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    // Thêm c?u hình Swagger ?? yêu c?u xác th?c
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme="oauth2",
                Name="Bearer",
                In = ParameterLocation.Header,
            },
            new string[] { }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add this line to configure CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
