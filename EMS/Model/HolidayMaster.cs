namespace EMS.Model
{
    public class HolidayMaster
    {
        public string Name { get; set; }
        public int? CreatedID { get; set; }
        public int? UpdatedID { get; set; }
    }

    public class GetHolidayMaster
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int CreatedID { get; set; }

        public string CreatedDate { get; set; }

        public int UpdatedID { get; set; }

        public string UpdatedDate { get; set; }
    }

    public class HolidayMappingMaster
    {
        public List<DateTime> dateList { get; set; }
        public int HolidayMasterID { get; set; }
        public int? CreatedID { get; set; }
        public int? UpdatedID { get; set; }
    }

}
