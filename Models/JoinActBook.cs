using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models
{
    [Table("AccountBook")]
    public class JoinActBook
    {
        [Column("Id")]
        public int Id { get; set; }
        
        [Column("Categoryyy")]
        [Required(ErrorMessage = "請選擇類別")]
        public string sType { get; set; }
        
        [Column("Dateee")]
        [Required(ErrorMessage = "請選擇日期")]
        public DateTime sDate { get; set; }
        
        [Column("Amounttt")]
        [Required(ErrorMessage = "請輸入金額")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於0")]
        public int Amount { get; set; }

        [Column("Remarkkk")]
        public string Description { get; set; }
    }
}
