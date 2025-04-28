using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsReactifService.Models
{
    [Table("report_reactif")]
    public class ReportReactif
    {
        [Key]
        [Column("id_report_reactif")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdReportReactif { get; set; }

        [Required]
        [Column("date_report")]
        public DateTime DateReport { get; set; }

        [Required]
        [Column("quantite")]
        public double Quantite { get; set; }

        [Required]
        [Column("id_reactif")]
        public int IdReactif { get; set; }

        [ForeignKey("IdReactif")]
        public Reactif? Reactif { get; set; }
    }
}