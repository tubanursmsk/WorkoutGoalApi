using System.ComponentModel.DataAnnotations;

namespace WorkoutGoalApi.Dto.WorkoutDto
{
    public class CreateWorkoutDto
    {
        [Required(ErrorMessage = "Egzersiz tipi boş olamaz.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Egzersiz tipi 2 ile 100 karakter arasında olmalıdır.")]
        public string WorkoutType { get; set; }

        [Required(ErrorMessage = "Süre boş olamaz.")]
        [Range(1, 1440, ErrorMessage = "Süre 1 ile 1440 dakika arasında olmalıdır.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Tarih boş olamaz.")]
        public DateTime Date { get; set; }

        [Range(0, 10000, ErrorMessage = "Yakılan kalori 0 ile 10000 arasında olmalıdır.")]
        public int CaloriesBurned { get; set; }

        // UserId buradan alınmaz, serviste token'dan alınır.
    }
}