using EMS.DataAccess.Interface;
using EMS.DataAccess.IRepository;
using EMS.DataAccess.Repository;
using EMS.DbConnection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbFactory,DapperDbConnectionFactory>();

/* Services */
builder.Services.AddScoped<IEmployeeMaster,EmployeeMasterReposiotory>();
builder.Services.AddScoped<IHolidayMasterRepository,HolidayMasterRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();




app.Run();
