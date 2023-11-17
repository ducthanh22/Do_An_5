using BUS.Interface;
using BUS;
using Microsoft.EntityFrameworkCore;
using Model;
using DAL;
using DAL.Interface;
using Microsoft.AspNetCore.Hosting;
using DTO.AutoMapper;
using AutoMapper;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddDbContext<Achino_DbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<ICategoriesBus, CategoriesBus>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddScoped<IProductsBus, ProductsBus>();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

builder.Services.AddScoped<ICustommerBus, CustommerBus>();
builder.Services.AddScoped<ICustommerRepository, CustommerRepository>();

builder.Services.AddScoped<IDetail_exportbillBus, Detail_exportbillBus>();
builder.Services.AddScoped<IDetail_exportbillRepository, Detail_exportbillRepository>();

builder.Services.AddScoped<IDetail_importbillBus, Detail_importbillBus>();
builder.Services.AddScoped<IDetail_importbillRepository, Detail_importbillRepository>();

//builder.Services.AddScoped<IProductsBus, ProductsBus>();
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

builder.Services.AddScoped<IStaffBus, StaffBus>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();

builder.Services.AddScoped<IWarehouseBus, WarehouseBus>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddScoped<IOder_detailBus, Order_detailBus>();

builder.Services.AddScoped<IOrder_detailRepository, Order_detailRepository>();

builder.Services.AddScoped<IOrderBus, OrderBus>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();




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
