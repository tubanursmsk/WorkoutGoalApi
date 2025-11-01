using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutGoalApi.Dto.GoalDto; 
using WorkoutGoalApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkoutGoalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // PROJE GEREĞİ: Tüm Goal işlemleri yetkilendirme gerektirir
    public class GoalController : ControllerBase
    {
        private readonly GoalService _goalService;

        public GoalController(GoalService goalService)
        {
            _goalService = goalService;
        }

        /// <summary>
        /// Sadece o an giriş yapmış kullanıcıya ait hedefleri getirir.
        /// </summary>
        [HttpGet("allList")]
        public async Task<ActionResult<List<GoalDto>>> GetMyGoals()
        {
            var goals = await _goalService.GetMyGoalsAsync();
            return Ok(goals);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip ve o anki kullanıcıya ait hedefi getirir.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GoalDto>> GetGoalById(int id)
        {
            var goal = await _goalService.GetGoalByIdAsync(id);

            if (goal == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(goal);
        }

        /// <summary>
        /// O anki kullanıcı için yeni bir hedef kaydı oluşturur.
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> CreateGoal(CreateGoalDto createDto)
        {
            var newGoal = await _goalService.CreateGoalAsync(createDto);

            // Yeni nesnenin yerini (GetGoalById) ve kendisini dön (201 Created)
            return CreatedAtAction(nameof(GetGoalById), new { id = newGoal.GId }, newGoal);
            // NOT: GoalDto'da ID 'GId' ise 'newGoal.GId' yapın
            // Eğer 'Id' ise 'newGoal.Id' yapın.
        }

        /// <summary>
        /// O anki kullanıcıya ait bir hedef kaydını günceller.
        /// </summary>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateGoal(int id, UpdateGoalDto updateDto)
        {
            var updatedGoal = await _goalService.UpdateGoalAsync(id, updateDto);

            if (updatedGoal == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(updatedGoal); // 200 OK
        }

        /// <summary>
        /// O anki kullanıcıya ait bir hedef kaydını siler.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteGoal(int id)
        {
            var result = await _goalService.DeleteGoalAsync(id);

            if (result == false)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content (Başarılı silme)
        }
    }
}