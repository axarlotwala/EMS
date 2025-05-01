using EMS.Common;
using EMS.DataAccess.IRepository;
using EMS.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers
{
    [Route("api/EmployeeMaster")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {
       
        private readonly IEmployeeMaster _employeeMaster;
        private readonly IWebHostEnvironment _environment;

        public EmployeeMasterController(IEmployeeMaster employeeMaster, IWebHostEnvironment hosting)
        {
            _employeeMaster = employeeMaster;
            _environment = hosting;
        }


        [Route("AddEmployee")]
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeModel employeeModel)
        {
            var result = await _employeeMaster.AddEmployee(employeeModel);
            return Ok(result);

            //var formdata = Request.Form["formdata"];
            //var model = JsonConvert.DeserializeObject<EmployeeModel>(formdata);
            //var result = await _employeeMaster.AddEmployee(model);
            //return Ok(result);
        }


        [Route("UploadProfile")]
        [HttpPost]
        public async Task<IActionResult> UploadProfile(UploadFile fileUpload)
        {
            var files = Request.Form.Files[0];
            CommonUtils commonUtils = new CommonUtils();

            if (files != null)
            {
                var FileName = Guid.NewGuid() + Path.GetExtension(files.FileName);
  
                var upload = Path.Combine(_environment.ContentRootPath, "Resources", "Upload", "EmployeeProfile");

                var filepath = Path.Combine(upload, FileName);

                if (!Directory.Exists(upload))
                {
                    Directory.CreateDirectory(upload);
                }

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                    stream.Close();
                    return Ok(FileName);
                }
            }

            return Ok();
        }


        [HttpGet("DownloadBulkEmployeeRegister")]
        public IActionResult DownloadBulkEmployeeRegister()
        {
            string fileName = "EmployeeRegistrationFormat.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var filePath = Path.Combine(_environment.ContentRootPath,"Resources","Format",fileName);

            var fileStream = new FileStream(filePath,FileMode.Open,FileAccess.Read);
            return File(fileStream, contentType ,fileName);
        }


        [Route("BulkRegistrationEmployee")]
        [HttpPost]
        public async Task<IActionResult> BulkRegistrationEmployee([FromForm] int CreatedID,IFormFile formFile)
        {
            var files = Request.Form.Files[0];
            CommonUtils commonUtils = new CommonUtils();

            if (files != null)
            {
                var FileName = Guid.NewGuid() +"_" + files.FileName;

                var upload = Path.Combine(_environment.ContentRootPath, "Resources", "Upload");

                var filepath = Path.Combine(upload, FileName);

                if (!Directory.Exists(upload))
                {
                    Directory.CreateDirectory(upload);
                }

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                    stream.Close();
                   // return Ok(FileName);
                }

                var result = await _employeeMaster.BulkRegistrationEmployee(CreatedID,filepath);
                return Ok(result);
            }

            return Ok();
        }


        
        [HttpGet(Name = "GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            ApiResponse response = new ApiResponse();
            response.Message = "Data Not Found";
            response.Success = 0;

            var result = await _employeeMaster.GetAllEmployee();

            if (result.Count > 0)
            {
                return Ok(result);
            }

            return Ok(result);

        }


    }
}
