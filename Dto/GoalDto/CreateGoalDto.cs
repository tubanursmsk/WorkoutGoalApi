using System.ComponentModel.DataAnnotations;

namespace WorkoutGoalApi.Dto.GoalDto
{
    public class CreateGoalDto
    {
        [Required(ErrorMessage = "Hedef tipi boş olamaz.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Hedef tipi 3 ile 150 karakter arasında olmalıdır.")]
        public string GoalType { get; set; }

        [Required(ErrorMessage = "Hedef değer boş olamaz.")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Hedef değer 0'dan büyük olmalıdır.")]
        public double TargetValue { get; set; }

        // Mevcut değer (CurrentValue) genellikle 0 olarak başlar, bu yüzden DTO'da olmasına gerek yok.
        // Servis katmanında varsayılan olarak 0 atanabilir.

        [Required(ErrorMessage = "Başlangıç tarihi boş olamaz.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi boş olamaz.")]
        public DateTime EndDate { get; set; }
    }
}