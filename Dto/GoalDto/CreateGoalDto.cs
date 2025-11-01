using System.ComponentModel.DataAnnotations;

namespace WorkoutGoalApi.Dto.GoalDto
{
    public class CreateGoalDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string GoalType { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]
        public double TargetValue { get; set; }

        // Mevcut değer (CurrentValue) genellikle 0 olarak başlar, bu yüzden DTO'da olmasına gerek yok.
        // Servis katmanında varsayılan olarak 0 atanabilir.

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}