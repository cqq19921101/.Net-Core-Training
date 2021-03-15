using System;
using System.Collections.Generic;

#nullable disable

namespace SmartPark.EntityFrameworkCore.DbMigrator.Models
{
    public partial class SpFaceCapture
    {
        public Guid Tkey { get; set; }
        public string ExtraId { get; set; }
        public decimal? Age { get; set; }
        public string CameraPosition { get; set; }
        public decimal? CompanyId { get; set; }
        public double? Confidence { get; set; }
        public decimal? EventType { get; set; }
        public decimal? Fmp { get; set; }
        public string FmpError { get; set; }
        public decimal? Gender { get; set; }
        public decimal? Id { get; set; }
        public string Name { get; set; }
        public decimal? PassType { get; set; }
        public string Photo { get; set; }
        public double? Quality { get; set; }
        public decimal? ScreenId { get; set; }
        public decimal? SubjectId { get; set; }
        public string SubjectPhoto { get; set; }
        public decimal? SubjectType { get; set; }
        public decimal? Temperature { get; set; }
        public DateTime? Timestamp { get; set; }
        public string Uuid { get; set; }
        public decimal? VerificationMode { get; set; }
        public string CreateDate { get; set; }
    }
}
