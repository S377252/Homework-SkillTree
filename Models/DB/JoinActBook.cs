using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models.DB
{
    [Table("AccountBook")]
    public class JoinActBook : IValidatableObject
    {
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Categoryyy")]
        public int Categoryyy { get; set; } // 資料庫中的數字欄位

        [Display(Name = "收支類別")]// 這是用來在前端顯示的文字欄位
        [NotMapped]
        [Required(ErrorMessage = "請選擇類別")]
        public string sType
        {
            get
            {
                // 將數字轉換為對應的文字
                return Categoryyy switch
                {
                    0 => "支出",
                    1 => "收入",
                    _ => null // 如果無效，返回 null，觸發 Required 驗證
                };
            }
            set
            {
                // 如果需要反向設定，將文字轉換為數字
                Categoryyy = value switch
                {
                    "支出" => 0,
                    "收入" => 1,
                    _ => -1 // 預設值，表示無效
                };
            }
        }

        [Display(Name = "日期")]// 這是用來在前端顯示的文字欄位
        [Column("Dateee")]
        [Required(ErrorMessage = "請選擇日期")]
        //日期不能大於今日
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime sDate { get; set; }

        [Display(Name = "金額")]// 這是用來在前端顯示的文字欄位
        [Column("Amounttt")]
        [Required(ErrorMessage = "請輸入金額")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於0")]
        public int Amount { get; set; }

        [Display(Name = "備註")]// 這是用來在前端顯示的文字欄位
        //長度限制100
        [Column("Remarkkk")]
        [StringLength(100, ErrorMessage = "備註長度不能超過100個字元")]
        public string Description { get; set; } = "";

        // ✅ 自訂驗證邏輯：日期介於 2010-01-01 ~ 今天
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var minDate = new DateTime(2010, 1, 1);
            var maxDate = DateTime.Today;

            if (sDate < minDate || sDate > maxDate)
            {
                yield return new ValidationResult("日期必須在 2010/01/01 到今天之間", new[] { nameof(sDate) });
            }
        }
    }

    public enum CategoryType
    {
        收入 = 1,
        支出 = 0
    }



}
