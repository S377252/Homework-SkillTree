using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models.DB;

public partial class AccountBook
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public int Categoryyy { get; set; }

    [Required]
    public int Amounttt { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime Dateee { get; set; }

    [Required]
    [StringLength(500)]
    public string Remarkkk { get; set; } = null!;
}