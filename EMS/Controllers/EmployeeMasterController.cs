using EMS.Common;
using EMS.DataAccess.IRepository;
using EMS.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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


        
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee()
        {
            //var files = Request.Form.Files[0];
            //CommonUtils commonUtils = new CommonUtils();

            var formdata = Request.Form["formdata"];
            var model = JsonConvert.DeserializeObject<EmployeeModel>(formdata);

            if (Request.Form.Files.Count > 0)
            {
                var files = Request.Form.Files[0];
                var FileName = Guid.NewGuid() + "_" + files.FileName;

                var upload = Path.Combine(_environment.WebRootPath, "Resources", "Upload");

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

                var result = await _employeeMaster.AddEmployee(FileName, model);
                return Ok(result);
            }

            return Ok();

            
        }


        
        [HttpPost]
        [Route("UploadProfile")]
        public async Task<IActionResult> UploadProfile(UploadFile fileUpload)
        {
            var formdata = Request.Form["formdata"];
            var model = JsonConvert.DeserializeObject<EmployeeModel>(formdata);

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


        
        [HttpPost]
        [Route("BulkRegistrationEmployee")]
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


        
        [HttpPost]
        [Route("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee([FromBody] RequestGetEmployee request)
        {
            ApiResponse response = new ApiResponse();
            response.Message = "Data Not Found";
            response.Success = 0;


            if (request.Mode == "GetByID")
            {
                var empDetail = await _employeeMaster.GetEmployeeByID(request);
                return Ok(empDetail);
            }

            if (request.Mode == "ALL")
            {
                var result = await _employeeMaster.GetAllEmployee(request);

                if (result.Count > 0)
                {
                    return Ok(result);
                }
            }

            return Ok(response);

        }

        [HttpPost]
        [Route("UpdateEmployee/{ID}")]
        public async Task<IActionResult> UpdateEmployee(int ID)
        {
            //var result = await _employeeMaster.UpdateEmployee(ID, request);
            //return Ok(result);

            var formdata = Request.Form["formdata"];
            var model = JsonConvert.DeserializeObject<EmployeeModel>(formdata);

            if (Request.Form.Files.Count > 0)
            {
                var files = Request.Form.Files[0];
                var FileName = Guid.NewGuid() + "_" + files.FileName;

                var upload = Path.Combine(_environment.WebRootPath,"Resources" ,"Upload");

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

                var result = await _employeeMaster.UpdateEmployee(ID,FileName, model);
                return Ok(result);
            }
            else
            {
                var result = await _employeeMaster.UpdateEmployee(ID, null, model);
                return Ok(result);
            }

            return Ok();
        }

        [HttpPost]
        [Route("DeleteEmployee/{ID}")]
        public async Task<IActionResult> DeleteEmployee(int ID)
        {
            var result = await _employeeMaster.DeleteEmployee(ID);
            return Ok(result);
        }

        [HttpGet]
        [Route("AdminDashboard")]
        public async Task<IActionResult> AdminDashboard()
        {
            var result = await _employeeMaster.AdminDashboard();
            return Ok(result);
        }
    }
}
