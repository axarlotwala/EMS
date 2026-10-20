using EMS.Common;
using EMS.DataAccess.Interface;
using EMS.DataAccess.IRepository;
using EMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace EMS.Controllers
{
    [Route("api/HolidayMaster")]
    [ApiController]
    public class HolidayMasterController : ControllerBase
    {
        public readonly IWebHostEnvironment _environment;
        public readonly IHolidayMasterRepository _holidayMaster;

        public HolidayMasterController(IWebHostEnvironment webHostEnvironment,IHolidayMasterRepository holidayMaster) 
        {
            _environment = webHostEnvironment;
            _holidayMaster = holidayMaster;
        }


        [HttpPost]
        [Route("AddHolidaysMaster")]
        public async Task<IActionResult> AddHolidaysMaster([FromBody]HolidayMaster holidayMaster)
        {
            var result = await _holidayMaster.AddHolidaysMaster(holidayMaster);
            return Ok(result);
        }

        [HttpGet("DownloadBulkHolidayAddFormat")]
        public IActionResult DownloadBulkHolidayAddFormat()
        {
            string fileName = "AddAllHolidayFormat.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var filePath = Path.Combine(_environment.WebRootPath, "Resources", "Format", fileName);

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, contentType, fileName);
        }

       
        [HttpPost]
        [Route("AddBulkHoliday")]
        public async Task<IActionResult> AddBulkHoliday([FromForm] RequestHolidayData request)
        {
           // var files = Request.Form.Files[0];
           
            if (request.file != null)
            {
                var FileName = Guid.NewGuid() + "_" + request.file.FileName;

                var upload = Path.Combine(_environment.WebRootPath, "Resources", "Upload");

                var filepath = Path.Combine(upload, FileName);

                if (!Directory.Exists(upload))
                {
                    Directory.CreateDirectory(upload);
                }

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    await request.file.CopyToAsync(stream);
                    stream.Close();
                    // return Ok(FileName);
                }

                var result = await _holidayMaster.AddBulkHoliday(filepath);
                return Ok(result);
            }

            return Ok();
        }


        [HttpPost]
        [Route("UpdateHolidaysMaster/{ID}")]
        public async Task<IActionResult> UpdateHolidaysMaster(int ID,[FromBody] HolidayMaster holidayMaster)
        {
            var result = await _holidayMaster.UpdateHolidaysMaster(ID,holidayMaster);
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteHolidaysMaster/{ID}")]
        public async Task<IActionResult> DeleteHolidaysMaster(int ID)
        {
            var result = await _holidayMaster.DeleteHolidaysMaster(ID);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAllHolidaysMaster")]
        public async Task<IActionResult> GetAllHolidaysMaster()
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = 0;
            apiResponse.Message = "No Data Found";
            
            var result = await _holidayMaster.GetAllHolidayMasters();

            if (result.Count > 0)
            {
                return Ok(result);
            }

            return Ok(apiResponse);

        }

        [HttpGet]
        [Route("GetHolidaysMasterByID/{ID}")]
        public async Task<IActionResult> GetHolidaysMasterByID(int ID)
        {
            ApiResponse apiResponse = new ApiResponse();
            apiResponse.Success = 0;
            apiResponse.Message = "No Data Found";

            var result = await _holidayMaster.GetHolidayMastersByID(ID);

            if (result != null)
            {
                return Ok(result);
            }

            return Ok(apiResponse);
        }

        #region Holiday Date Wise Mapping
        [HttpPost]
        [Route("AddHolidayMappingMaster")]
        public async Task<IActionResult> AddHolidayMappingMaster([FromBody] HolidayMappingMaster mappingMaster)
        {
            var result = await _holidayMaster.AddHolidayMappingMaster(mappingMaster);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetAllHolidays")]
        public async Task<IActionResult> GetAllHolidays()
        {
            var result = await _holidayMaster.GetAllHolidays();
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteHolidayMappingByID/{ID}")]
        public async Task<IActionResult> DeleteHolidayMappingByID(int ID)
        {
            var result = await _holidayMaster.DeleteHolidayMappingByID(ID);
            return Ok(result);
        }
        #endregion
    }
}
