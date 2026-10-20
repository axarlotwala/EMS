using EMS.DataAccess.Interface;
using EMS.DataAccess.IRepository;
using EMS.DataAccess.Repository;
using EMS.DbConnection;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // your Angular app URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});





builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.PropertyNamingPolicy = null;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbFactory,DapperDbConnectionFactory>();

/* Services */
builder.Services.AddScoped<IEmployeeMaster,EmployeeMasterReposiotory>();
builder.Services.AddScoped<IHolidayMasterRepository,HolidayMasterRepository>();
builder.Services.AddScoped<IEmployeeAttedanceRepository, EmployeeAttedanceRepository>();
builder.Services.AddScoped<IPayrollGenerateEmployeeSalaryRepository, PayrollGenerateEmployeeSalaryRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
builder.Services.AddScoped<IPayrollEmployeeIncrementRepository,PayrollEmployeeIncrementRepository>();



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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","Resources")),
    RequestPath = "/Resources"
});

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();




app.Run();

// https://www.techiediaries.com/angular-by-example-httpclient-get/
