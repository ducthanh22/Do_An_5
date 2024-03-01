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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddDbContext<Achino_DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<Achino_DbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddHttpContextAccessor(); // ??ng k� IHttpContextAccessor

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); // ??ng k� IActionContextAccessor
builder.Services.AddSingleton<IPaymentRepository, PaymentRepository>();

//builder.Services.AddScoped<IUrlHelper>(x => {
//    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
//    return new UrlHelper(actionContext);
//});


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
builder.Services.Configure<VnPay>(builder.Configuration.GetSection("VnPay"));



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
builder.Services.AddScoped<IExportbillRepository, ExportbillRepository>();

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

builder.Services.AddScoped<IPaymentBus, PaymentBus>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CustomerRole", policy => policy.RequireRole("Customer"));
    options.AddPolicy("AdminRole", policy => policy.RequireRole("admin"));
    // Th�m c�c policy kh�c n?u c?n thi?t
});

//builder.Services.AddCors(cors => cors.AddPolicy("MyPolicy", builder =>
//{
//    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
//}));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ACHINO API", Version = "v1" });
    // Th�m c?u h�nh Swagger ?? y�u c?u x�c th?c
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
app.UseStaticFiles();

// Add this line to configure CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseCors("MyPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
