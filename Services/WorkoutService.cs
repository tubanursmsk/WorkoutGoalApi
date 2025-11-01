using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic; // EKLENDİ
using System.Linq; // 
using System.Security.Claims; // EKLENDİ
using System.Threading.Tasks; // EKLENDİ
using AutoMapper;
using Microsoft.AspNetCore.Http; // EKLENDİ
using Microsoft.EntityFrameworkCore; // EKLENDİ
using WorkoutGoalApi.Utils;
using WorkoutGoalApi.Dto.WorkoutDto;
using WorkoutGoalApi.Models;

namespace WorkoutGoalApi.Services // Namespace'i "Workouts" değil, "Services" olarak düzelttim
{
    // 1. Interface (IWorkoutService) oluşturup implement etmeniz en iyi pratiktir.
    // Şimdilik servisin kendisini yazıyoruz:
    public class WorkoutService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor; // (Kullanıcıyı bulmak için)

        public WorkoutService(
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor) // GÜNCELLENDİ
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor; // YENİ EKLENDİ
        }

        // --- YENİ HELPER METOT ---
        // Token'ı okuyup o anki kullanıcının ID'sini (long) getiren metot
        private long GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !long.TryParse(userIdString, out var userId))
            {
                // GlobalExceptionHandler bu hatayı yakalayacaktır
                throw new Exception("Kullanıcı kimliği token içinde bulunamadı. Lütfen giriş yapın.");
            }
            return userId;
        }

        // --- GÜNCELLENMİŞ Create METODU ---
        // (Async yapıldı, Dto döndürüyor ve doğru mantıkta çalışıyor)
        public async Task<WorkoutDto> CreateWorkoutAsync(CreateWorkoutDto workoutCreateDto)
        {
            // 1. O anki kullanıcıyı bul
            var userId = GetCurrentUserId();

            // 2. DTO'yu Modele çevir
            var workout = _mapper.Map<Workout>(workoutCreateDto);

            // 3. Workout'u o kullanıcıya ata
            workout.UserId = userId;

            // 4. Veritabanına ekle
            await _dbContext.Workouts.AddAsync(workout);
            await _dbContext.SaveChangesAsync();

            // 5. Sonucu DTO olarak dön
            return _mapper.Map<WorkoutDto>(workout);
        }

        // --- GÜNCELLENMİŞ GetAll METODU ---
        // (Async yapıldı, Dto döndürüyor ve sadece o kullanıcıya ait veriyi getiriyor)
        public async Task<List<WorkoutDto>> GetMyWorkoutsAsync()
        {
            // 1. O anki kullanıcıyı bul
            var userId = GetCurrentUserId();

            // 2. Sadece o kullanıcıya ait egzersizleri bul
            var workouts = await _dbContext.Workouts
                                         .Where(w => w.UserId == userId)
                                         .ToListAsync();

            // 3. Modelleri DTO'ya çevirip dön
            return _mapper.Map<List<WorkoutDto>>(workouts);
        }

        // --- YENİ EKLENEN Update METODU ---
        // (İsteğiniz buydu)
        public async Task<WorkoutDto?> UpdateWorkoutAsync(int workoutId, UpdateWorkoutDto updateDto)
        {
            // 1. O anki kullanıcıyı bul
            var userId = GetCurrentUserId();

            // 2. Güncellenecek kaydı bul
            //    (Hem ID'si tutmalı hem de o kullanıcıya ait olmalı)
            var workoutFromDb = await _dbContext.Workouts
                .FirstOrDefaultAsync(w => w.WId == workoutId && w.UserId == userId);

            // 3. Kayıt bulunamazsa (ya yok ya da bu kullanıcıya ait değil)
            if (workoutFromDb == null)
            {
                // Controller'da "NotFound" (404) dönebilmesi için null dönüyoruz
                return null;
            }

            // 4. AutoMapper ile DTO'daki bilgileri DB'den gelen kaydın üzerine işle
            //    (workoutFromDb'nin içindeki alanlar güncellenir)
            _mapper.Map(updateDto, workoutFromDb);

            // 5. Değişiklikleri kaydet
            await _dbContext.SaveChangesAsync();

            // 6. Güncellenmiş nesneyi DTO olarak dön
            return _mapper.Map<WorkoutDto>(workoutFromDb);
        }
        
        public async Task<WorkoutDto?> GetWorkoutByIdAsync(int workoutId)
        {
            var userId = GetCurrentUserId();
            var workout = await _dbContext.Workouts
                .FirstOrDefaultAsync(w => w.WId == workoutId && w.UserId == userId);
            
            return _mapper.Map<WorkoutDto>(workout); // Bulamazsa 'null' döner
        }

        public async Task<bool> DeleteWorkoutAsync(int workoutId)
        {
            var userId = GetCurrentUserId();
            var workout = await _dbContext.Workouts
                .FirstOrDefaultAsync(w => w.WId == workoutId && w.UserId == userId);

            if (workout == null)
            {
                return false; // Silinecek bir şey bulunamadı
            }

            _dbContext.Workouts.Remove(workout);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}