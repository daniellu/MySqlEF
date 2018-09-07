
namespace Hatfield.AnalyteManagement.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("analyteguideline")]
    public partial class AnalyteGuideline
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string AnalyteName { get; set; }

        public virtual string Unit { get; set; }
        public virtual string GuidelineValue { get; set; }

        public virtual string ValueType { get; set; }
        public virtual string ReferenceSource { get; set; }
        public virtual string ComparisonOperation { get; set; }


        public int GuidelineId { get; set; }

        public virtual Guideline Guideline { get; set; }
    }
}
