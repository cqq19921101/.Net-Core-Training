using System;
using System.Collections.Generic;
using System.Text;

namespace HangFire.Application.Contracts.ExceptionEntity
{
    public class GroupsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
    }

    public class PhotosItem
    {
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
        public string origin_url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double quality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int subject_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
    }

    public class DataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string come_from { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int company_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int create_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string department { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int end_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entry_date { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extra_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<GroupsItem> groups { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string interviewee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string interviewee_pinyin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string job_number { get; set; }
        /// <summary>
        /// 陈乾乾
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string password_reseted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PhotosItem> photos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pinyin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int purpose { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int start_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int subject_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string visit_notify { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wg_number { get; set; }
    }

    public class Page
    {
        /// <summary>
        /// 
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int current { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
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
        public List<DataItem> data { get; set; }
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
