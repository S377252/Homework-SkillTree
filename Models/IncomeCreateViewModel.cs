using System.ComponentModel.DataAnnotations;

namespace Homework_SkillTree.Models
{
    public class IncomeCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "請選擇類別")]
        public string sType { get; set; }
        [Required(ErrorMessage = "請選擇日期")]
        public DateTime sDate { get; set; }
        [Required(ErrorMessage = "請輸入金額")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於0")]
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
