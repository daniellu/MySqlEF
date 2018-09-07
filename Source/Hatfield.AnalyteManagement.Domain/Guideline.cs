namespace Hatfield.AnalyteManagement.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("guideline")]
    public partial class Guideline
    {
        public Guideline()
        {
            AnalyteGuidelines = new HashSet<AnalyteGuideline>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string ShortCode { get; set; }

        public virtual ICollection<AnalyteGuideline> AnalyteGuidelines { get; set; }
    }
}
