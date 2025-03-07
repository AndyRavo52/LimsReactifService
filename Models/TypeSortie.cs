using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LimsReactifService.Models
{
    [Table("Type_Sortie")]
    public class TypeSortie
    {
        [Key]
        [Column("id_type_sortie")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTypeSortie { get; set; }

        [Required]
        [Column("designation")]
        [StringLength(50)]
        public string? Designation { get; set; }
    }
}
