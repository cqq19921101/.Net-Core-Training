using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Meowv.Blog.Domain.AccessControl.Repositories
{
    public class FaceCapture : Entity<Guid>
    {
        public Guid Tkey { get; set; }

        public string ExtraId { get; set; }

        public decimal? Age { get; set; }

        public string CameraPosition { get; set; }

        public decimal? CompanyId { get; set; }

        public float? Confidence { get; set; }

        public decimal? EventType { get; set; }

        public decimal? Fmp { get; set; }

        public string FmpError { get; set; }

        public decimal? Gender { get; set; }

        public decimal? id { get; set; }

        public string Name { get; set; }

        public decimal? PassType { get; set; }

        public string Photo { get; set; }

        public float? Quality { get; set; }

        public decimal? ScreenId { get; set; }

        public decimal? SubjectId { get; set; }

        public string SubjectPhoto { get; set; }

        public decimal? SubjectType { get; set; }

        public decimal? temperature { get; set; }

        public DateTime? Temperature { get; set; }

        public string Uuid { get; set; }

        public decimal? VerificationMode { get; set; }

        public DateTime? Timestamp { get; set; }

        public string CreateDate { get; set; }

    }

}
