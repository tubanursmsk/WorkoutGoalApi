using System.ComponentModel.DataAnnotations;

namespace WorkoutGoalApi.Dto.WorkoutDto
{
    public class CreateWorkoutDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string WorkoutType { get; set; }

        [Required]
        [Range(1, 1440)]
        public int Duration { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Range(0, 10000)]
        public int CaloriesBurned { get; set; }

        
    }
}