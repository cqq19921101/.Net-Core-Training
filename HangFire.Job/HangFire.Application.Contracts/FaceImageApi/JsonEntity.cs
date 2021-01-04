using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.ExceptionEntity
{
    public class JsonEntity
    {
        public class Company
        {
            /// <summary>
            /// 
            /// </summary>
            public string attendance_on { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<int> attendance_weekdays { get; set; }
            /// <summary>
            /// 胡正好
            /// </summary>
            public string consigner { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int create_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int data_version { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int deployment { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<List<int>> door_range { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<int> door_weekdays { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int feature_version { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fmp_on { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string full_day { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 中文简体
            /// </summary>
            public string lang { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string lang_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string logo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double max_temperature { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double min_temperature { get; set; }
            /// <summary>
            /// 丘钛
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string notdetermined_on { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string remark { get; set; }
            /// <summary>
            /// 正常使用
            /// </summary>
            public string scenario { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string temperature_warn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int temperature_warn_last { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int temperature_warn_open_door_limit { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string upload { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yellowlist_warn { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string auth_token { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string avatar { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Company company { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int company_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string lang { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string lang_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string organization_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string password_reseted { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<int> permission { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int role_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string username { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string verify { get; set; }
        }

        public class Page
        {
        }

        public class Photo
        {
            public string subjectid { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Photo photo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Page page { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int timecost { get; set; }
        }
    }
}
