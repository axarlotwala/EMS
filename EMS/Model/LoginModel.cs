namespace EMS.Model
{
    public class RequestLogin
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }

    public class LoginUser
    {
        public string? EmployeeCode { get; set; }
        public string? RoleType { get; set; }
        public string? EmployeeName { get; set; }
        public int? Success { get; set; }
    }
}
