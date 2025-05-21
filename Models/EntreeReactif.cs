using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LimsReactifService.Models
{
    [Table("Entree_reactif")]
    public class EntreeReactif
    {
        [Key]
        [Column("id_entree_reactif")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEntreeReactif { get; set; }

        [Required]
        [Column("quantite")]
        public double Quantite { get; set; }

        [Required]
        [Column("prix_achat")]
        public decimal PrixAchat { get; set; }

        [Required]
        [Column("bon_reception")]
        [StringLength(50)]
        public string? BonReception { get; set; }

        // Nouveau champ ajouté
        [Required]
        [Column("bon_de_commande")]
        [StringLength(50)]
        public string? BonDeCommande { get; set; }

        // Nouveau champ ajouté
        [Required]
        [Column("numero_facture")]
        [StringLength(50)]
        public string? NumeroFacture { get; set; }

        [Required]
        [Column("date_entree")]
        public DateTime DateEntree { get; set; }

        [Column("date_peremption")]
        public DateTime? DatePeremption { get; set; }

        [Column("id_reactif")]
        public int? IdReactif { get; set; }

        [ForeignKey("IdReactif")]
        public Reactif? Reactif { get; set; }

        [Required]
        [Column("id_fournisseur")]
        public int IdFournisseur { get; set; }

        [ForeignKey("IdFournisseur")]
        public Fournisseur? Fournisseur { get; set; }
    }
}