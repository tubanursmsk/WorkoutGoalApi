namespace WorkoutGoalApi.Dto.GoalDto
{
    public class GoalDto
    {
        public int GId { get; set; }
        public string GoalType { get; set; }
        public double TargetValue { get; set; }
        public double CurrentValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; } // Model'e bu alanı da eklemek iyi olabilir.
    }
}