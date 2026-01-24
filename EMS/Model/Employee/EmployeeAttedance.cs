namespace EMS.Model.Employee
{

    public class RequestGenerateEmployeeAttedance
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string? EmployeeCode { get; set; }
        public IFormFile? FileData { get; set; }
        public int?  CreatedID { get; set; }

    }

    public class BulkUploadAttedanceResponse
    {
        public int? Success { get; set; }
        public string? Message { get; set; }

        public List<AttedanceValidation> requireField { get; set; } = new List<AttedanceValidation>();
    }

    public class AttedanceValidation
    {
        public int? Success { get; set; }
        public DateTime? Date { get; set; }  
        public string? ColumnName { get; set; }
        public string? Message { get; set; }
    }

    public class EmployeeAttedanceGet
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public DateTime AttedanceDate { get; set; }
        public DateTime PunchInTime { get; set; }
        public DateTime PunchOutTime { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsHoliday { get; set; }
        public string HolidayName { get; set; }
        public string Status { get; set; }
    }
}
