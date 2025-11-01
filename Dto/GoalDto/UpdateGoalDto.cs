using System.ComponentModel.DataAnnotations;

namespace WorkoutGoalApi.Dto.GoalDto
{
    // Güncelleme için genellikle tüm alanlar gerekmez.
    // Özellikle hedefin mevcut durumunu güncellemek için ayrı bir DTO da yapılabilir
    // ama şimdilik tam güncelleme yapalım:
    public class UpdateGoalDto
    {
        [Required(ErrorMessage = "Hedef tipi boş olamaz.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Hedef tipi 3 ile 150 karakter arasında olmalıdır.")]
        public string GoalType { get; set; }

        [Required(ErrorMessage = "Hedef değer boş olamaz.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Hedef değer 0'dan büyük olmalıdır.")]
        public double TargetValue { get; set; }

        [Required(ErrorMessage = "Mevcut değer boş olamaz.")]
        [Range(0, double.MaxValue, ErrorMessage = "Mevcut değer 0 veya daha büyük olmalıdır.")]
        public double CurrentValue { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi boş olamaz.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi boş olamaz.")]
        public DateTime EndDate { get; set; }
    }
}