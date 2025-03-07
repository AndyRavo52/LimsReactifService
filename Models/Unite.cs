using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LimsReactifService.Models
{
    [Table("Unite")]
    public class Unite
    {
        [Key]
        [Column("id_unite")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUnite { get; set; }

        [Required]
        [Column("designation")]
        [StringLength(50)]
        public string? Designation { get; set; }
    }
}
