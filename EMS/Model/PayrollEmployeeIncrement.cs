namespace EMS.Model
{
    
    public class RequestEmployeeFixSalary
    {
        public string? EmployeeCode { get; set; }
    }
    
    public class GetEmployeeWiseFixSalary
    {
        public int EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public decimal FixSalary { get; set; }
        public DateTime LastChangeDate { get; set; }
        public DateTime EffectiveFromDate { get; set; }
    }

    public class RequestEmployeeIncrement
    {
        public int? EmployeeID { get; set; }
        public string? EmployeeCode { get; set; }
        public decimal? FixSalary { get; set; }
        public decimal? IncrementPercentage { get; set; }
        public decimal? IncrementAmount { get; set; }
        public decimal? FinalFixSalary { get; set; }
        public DateTime? EffectiveFromDate { get; set; }
    }

}
