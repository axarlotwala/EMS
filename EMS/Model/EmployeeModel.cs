namespace EMS.Model
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Experiance { get; set; }
        public decimal Salary { get; set; }
        public string EmployeeCode { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int WhatsappNo { get; set; }
        public string City { get; set; }

        public IFormFile ProfilePhoto { get; set; }
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

    public class EmployeeGetModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Experiance { get; set; }
        public decimal Salary { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string WhatsappNo { get; set; }
        public string City { get; set; }
       
    }
}
