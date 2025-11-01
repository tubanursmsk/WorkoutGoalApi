
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutGoalApi.Models
{
    [Index(nameof(WorkoutType), IsUnique = true)]
    public class Workout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WId { get; set; }

        public string  WorkoutType{ get; set; } = string.Empty;

        public string Detail { get; set; } = string.Empty;

        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }  // Navigation Property

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}