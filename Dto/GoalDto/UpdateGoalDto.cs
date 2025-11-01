using System.ComponentModel.DataAnnotations;

namespace WorkoutGoalApi.Dto.GoalDto
{
    // Güncelleme için genellikle tüm alanlar gerekmez.
    // Özellikle hedefin mevcut durumunu güncellemek için ayrı bir DTO da yapılabilir
    // ama şimdilik tam güncelleme yapalım:
    public class UpdateGoalDto
    {
        [Required]
        [StringLength(150, MinimumLength = 3)]
        public string? GoalType { get; set; }

        [Required]
        [Range(0.1, double.MaxValue)]
        public double TargetValue { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double CurrentValue { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}