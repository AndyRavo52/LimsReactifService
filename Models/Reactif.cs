using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsReactifService.Models
{
    [Table("Reactif")] // Spécifie le nom de la table dans la base de données
    public class Reactif
    {
        [Key] // Indique que cette propriété est la clé primaire
        [Column("id_reactif")] // Spécifie le nom de la colonne dans la base de données
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Valeur auto-incrémentée
        public int IdReactif { get; set; }

        [Required] // Propriété non nullable
        [Column("designation")]
        public required string Designation { get; set; }

        [Column("id_type_sortie")]
        public int IdTypeSortie { get; set; } // Clé étrangère vers la table TypeSortie (nullable)

        // Propriété de navigation pour la relation avec TypeSortie
        [ForeignKey("IdTypeSortie")]
        public TypeSortie? TypeSortie { get; set; }

        [Column("id_unite")]
        public int IdUnite { get; set; } // Clé étrangère vers la table Unite (nullable)

        // Propriété de navigation pour la relation avec Unite
        [ForeignKey("IdUnite")]
        public Unite? Unite { get; set; }
    }
}
