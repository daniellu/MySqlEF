namespace Hatfield.AnalyteManagement.Domain
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("labanalyte")]
    public partial class LabAnalyte
    {
        public LabAnalyte()
        {
            
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string CAS_Code { get; set; }
    }
}
