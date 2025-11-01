
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkoutGoalApi.Models
{
   
    public class Goal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GId { get; set; }

        public string GoalType { get; set; } = string.Empty;

        public double TargetValue { get; set; } = 0.0;

        public double CurrentValue { get; set; } = 0.0;

        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsCompleted { get; set; } 
        public int UserId { get; set; } // Foreign Key
        public User User { get; set; }  // Navigation Property
    }
}