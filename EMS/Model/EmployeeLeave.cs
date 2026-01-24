namespace EMS.Model
{
    public class RequestLeave
    {
        public string? EmployeeCode { get; set; }
        public DateTime? LeaveDate { get; set; }

        public string? reason { get; set; }
    }

    public class EmployeeLeave
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public DateTime LeaveDates { get; set; }
        public bool IsApprove { get; set; }
        public int Status { get; set; }
        public string LeaveStatus { get; set; }
        public string Reason { get; set; }
    }

    public class AdminUpdateStatus
    {
        public int? ID { get; set; }
        public int? Status { get; set; }
    }
}
