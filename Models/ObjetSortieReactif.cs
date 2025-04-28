using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LimsReactifService.Models
{
    [Table("objet_sortie_reactif")]
    public class ObjetSortieReactif
    {
        [Key]
        [Column("id_objet_sortie_reactif")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdObjetSortieReactif { get; set; }

        [Required]
        [Column("designation")]
        [StringLength(50)]
        public string? Designation { get; set; }
    }
}