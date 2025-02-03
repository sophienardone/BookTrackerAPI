using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTrackerAPI.Models;

namespace BookTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgressesController : ControllerBase
    {
        private readonly BooksContext _context;

        public UserProgressesController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/UserProgresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProgress>>> GetUserProgresses()
        {

            var userProgresses = await _context.UserProgresses
        .Include(up => up.User)  
        .Include(up => up.book)  
        .ToListAsync();

            var userProgressDtos = userProgresses.Select(up => MapToDTO(up)).ToList();
            return Ok(userProgressDtos);
            
        }

        // GET: api/UserProgresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProgress>> GetUserProgress(int id)
        {
            var userProgress = await _context.UserProgresses
            .Include(up => up.User) 
            .Include(up => up.book) 
            .Where(up => up.Id == id)
            .FirstOrDefaultAsync();

            if (userProgress == null)
            {
                return NotFound();
            }

            var userProgressDTO = new UserProgressDTO
            {
                Id = userProgress.Id,
                UserId = userProgress.UserId,
                BookId = userProgress.BookId,
                PagesRead = userProgress.PagesRead,
                LastUpdated = userProgress.LastUpdated,
                UserName = userProgress.User?.Username,  
                Title = userProgress.book?.Title        
            };

            return Ok(userProgressDTO);
        }

        // PUT: api/UserProgresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProgress(int id, UserProgress userProgress)
        {
            if (id != userProgress.Id)
            {
                return BadRequest();
            }

            _context.Entry(userProgress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProgressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserProgresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserProgress>> PostUserProgress(UserProgress userProgress)
        {
            _context.UserProgresses.Add(userProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserProgress), new { id = userProgress.Id }, userProgress);
        }

        // DELETE: api/UserProgresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProgress(int id)
        {
            var userProgress = await _context.UserProgresses.FindAsync(id);
            if (userProgress == null)
            {
                return NotFound();
            }

            _context.UserProgresses.Remove(userProgress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserProgressExists(int id)
        {
            return _context.UserProgresses.Any(e => e.Id == id);
        }

        //helper method for ouputting data 
        private static UserProgressDTO MapToDTO(UserProgress userProgress)
        {
            return new UserProgressDTO
            {
                Id = userProgress.Id,
                UserId = userProgress.UserId,
                BookId = userProgress.BookId,
                PagesRead = userProgress.PagesRead,
                LastUpdated = userProgress.LastUpdated,
                UserName = userProgress.User?.Username, 
                Title = userProgress.book?.Title       
            };
        }



    }
}
