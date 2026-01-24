namespace EMS.Model
{
    public class RequestPayrollSalary
    {
        public string? EmployeeCode { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        //public int CreatedID { get; set; }
    }

    public class PayrollGeneratedEmployeeSalary
    {
        public string EmployeeCode { get; set;}
        public string EmployeeName { get; set; }
        public int SalaryYear { get; set; }
        public int SalaryMonth { get; set; }
        public int TotalDaysInMonth { get; set; }
        public decimal TotalPresentDay { get; set; }
        public decimal AbsentDay { get; set; }
        public decimal FixSalary { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal InHandSalary { get; set; }
    }

    public class PayrollAllEmployeeSalary
    {
        public string EmployeeCode { get; set; }
       
        public int TotalDaysInMonth { get; set; }
        public decimal TotalPresentDay { get; set; }
        public decimal AbsentDay { get; set; }
        public decimal FixSalary { get; set; }
        public decimal DeductionAmount { get; set; }
        public decimal InHandSalary { get; set; }
    }

}
