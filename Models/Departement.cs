using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsReactifService.Models
{
    [Table("departement")]
    public class Departement
    {
        [Key]
        [Column("id_departement")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDepartement { get; set; }

        [Column("code")]
        [StringLength(4)]
        public string? Code { get; set; }

        [Required]
        [Column("designation")]
        [StringLength(50)]
        public string? Designation { get; set; }
    }
}