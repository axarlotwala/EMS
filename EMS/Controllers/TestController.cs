using EMS.DbConnection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace EMS.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        //private readonly IConfiguration? _configuration;
        //string ConnetionStr = string.Empty;
        //bool isSuccess = false;
        //String message = string.Empty;

        //public TestController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    ConnetionStr = _configuration.GetConnectionString("ErpDbConnect");
        //}

       // private readonly DapperDbContext _dbContext;

        //public TestController(DapperDbContext dapperDbContext)
        //{
        //    _dbContext = dapperDbContext;
        //}

        [Route("TestSqlServerConnection")]
        [HttpGet]
        public IActionResult TestSqlServerConnection()
        {

          //  _ = _dbContext.GetConnection;
            return Ok("Something Wrong In Connection");

            //using (SqlConnection con = new SqlConnection(ConnetionStr))
            //{
            //    con.Open();
            //    return Ok("Success Connection");
            //}

            //return Ok("Something Wrong In Connection");
        }
    }
}
