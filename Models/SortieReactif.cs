using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsReactifService.Models
{
    [Table("sortie_reactif")]
    public class SortieReactif
    {
        [Key]
        [Column("id_sortie")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSortie { get; set; }

        [Required]
        [Column("quantite")]
        public double Quantite { get; set; }

        [Required]
        [Column("date_sortie")]
        public DateTime DateSortie { get; set; }

        [Column("id_departement")]
        public int? IdDepartement { get; set; }

        [ForeignKey("IdDepartement")]
        public Departement? Departement { get; set; }

        [Required]
        [Column("id_reactif")]
        public int IdReactif { get; set; }

        [ForeignKey("IdReactif")]
        public Reactif? Reactif { get; set; }

        [Required]
        [Column("id_objet_sortie_reactif")]
        public int IdObjetSortieReactif { get; set; }

        [ForeignKey("IdObjetSortieReactif")]
        public ObjetSortieReactif? ObjetSortieReactif { get; set; }
    }
}