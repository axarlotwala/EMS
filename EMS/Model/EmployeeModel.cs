namespace EMS.Model
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string? BloodGroup { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal? Experiance { get; set; }
        public decimal? FixSalary { get; set; }
        //public string EmployeeCode { get; set; }
        public string Occupation { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
     //  public string WhatsappNo { get; set; }
        public string? City { get; set; }

        public IFormFile? ProfilePhoto { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }

        public string? IssuedAuthority { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? IssuedDate { get; set; }

        //public string Nationality { get; set; }
    }

    public class UploadFile
    {
        public IFormFile File { get; set; }
    }

    public class BulkRegisterEmployeeResponse
    {
        public string EmployeeName { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
    }

    public class RequestGetEmployee
    {
        public string? Mode { get; set; }
        public int? ID { get; set; }
    }

    public class EmployeeGetModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Experiance { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
       // public string WhatsappNo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
       
    }

    public class EmployeeModelGetByID
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string? BloodGroup { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Experiance { get; set; }
        public decimal FixSalary { get; set; }
        //public string EmployeeCode { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //  public string WhatsappNo { get; set; }
        public string City { get; set; }

        public string ProfilePhoto { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public string IssuedAuthority { get; set; }
        public string IdNumber { get; set; }
        public DateTime IssuedDate { get; set; }

        //public string Nationality { get; set; }
    }

    public class AdminDashboard
    {
        public int TotalActiveEmployee { get; set; }
        public int PendingLeaveRequest { get; set; }
    }
}
