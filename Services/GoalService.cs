using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkoutGoalApi.Dto.GoalDto; // DTO namespace'iniz
using WorkoutGoalApi.Models;
using WorkoutGoalApi.Utils;
using System.Security.Claims;

namespace WorkoutGoalApi.Services
{
    public class GoalService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoalService(
            ApplicationDbContext dbContext, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // --- Helper Metot (Token'dan UserId'yi alır) ---
        private long GetCurrentUserId()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !long.TryParse(userIdString, out var userId))
            {
                throw new Exception("Kullanıcı kimliği token içinde bulunamadı. Lütfen giriş yapın.");
            }
            return userId;
        }

        // --- Create (Oluşturma) ---
        public async Task<GoalDto> CreateGoalAsync(CreateGoalDto createDto)
        {
            var userId = GetCurrentUserId(); // 1. Kullanıcıyı bul
            var goal = _mapper.Map<Goal>(createDto); // 2. DTO'yu Modele çevir

            goal.UserId = userId; // 3. Hedefi kullanıcıya ata
            
            // 4. DTO'da olmayan varsayılan değerleri ata
            goal.CurrentValue = 0; 
            goal.IsCompleted = false; 

            await _dbContext.Goals.AddAsync(goal);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<GoalDto>(goal); // 5. Sonucu DTO olarak dön
        }

        // --- Get All (Kullanıcının Tüm Hedefleri) ---
        public async Task<List<GoalDto>> GetMyGoalsAsync()
        {
            var userId = GetCurrentUserId();

            var goals = await _dbContext.Goals
                                      .Where(g => g.UserId == userId)
                                      .ToListAsync();

            return _mapper.Map<List<GoalDto>>(goals);
        }

        // --- Get By Id (Kullanıcının Tek Bir Hedefi) ---
        public async Task<GoalDto?> GetGoalByIdAsync(int goalId)
        {
            var userId = GetCurrentUserId();

            // Modelinizdeki GId isimlendirmesine göre sorgu:
            var goal = await _dbContext.Goals
                .FirstOrDefaultAsync(g => g.GId == goalId && g.UserId == userId); 
                // ÖNEMLİ NOT: Modelinizde ID 'GId' ise 'g.GId == goalId' yapın
                // Eğer modelde 'Id' ise 'g.Id == goalId' olarak bırakın.

            return _mapper.Map<GoalDto>(goal); // Bulamazsa null döner
        }

        // --- Update (Güncelleme) ---
        public async Task<GoalDto?> UpdateGoalAsync(int goalId, UpdateGoalDto updateDto)
        {
            var userId = GetCurrentUserId();

            var goalFromDb = await _dbContext.Goals
                .FirstOrDefaultAsync(g => g.GId == goalId && g.UserId == userId);
                // NOT: Modelinizde ID 'GId' ise 'g.GId == goalId' yapın

            if (goalFromDb == null)
            {
                return null; // Bulunamadı veya kullanıcıya ait değil
            }

            // 1. DTO'daki bilgileri veritabanındaki nesneye işle
            _mapper.Map(updateDto, goalFromDb);

            // 2. Bonus Mantık: Değerleri kontrol et ve 'IsCompleted' durumunu güncelle
            goalFromDb.IsCompleted = (goalFromDb.CurrentValue >= goalFromDb.TargetValue);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<GoalDto>(goalFromDb);
        }

        // --- Delete (Silme) ---
        public async Task<bool> DeleteGoalAsync(int goalId)
        {
            var userId = GetCurrentUserId();
            var goal = await _dbContext.Goals
                .FirstOrDefaultAsync(g => g.GId == goalId && g.UserId == userId);
                // NOT: Modelinizde ID 'GId' ise 'g.GId == goalId' yapın

            if (goal == null)
            {
                return false; // Silinecek bir şey bulunamadı
            }

            _dbContext.Goals.Remove(goal);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}