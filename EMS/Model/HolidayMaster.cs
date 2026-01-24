using Microsoft.AspNetCore.Mvc;

namespace EMS.Model
{

    public class RequestHolidayData
    {
       public IFormFile file { get; set; }
    }
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
        public IList<MapWithDateList>? MapData { get; set; }
        public int? CreatedID { get; set; }
        public int? UpdatedID { get; set; }
    }

    public class MapWithDateList
    {
        public DateTime HolidayDate { get; set; }
        public int ID { get; set; }
    }
         
    public class GetHolidaysMapping
    {
        public int ID { get; set; }
        public DateTime HolidayDate { get; set; }
        public string Name { get; set; }
    }
}
