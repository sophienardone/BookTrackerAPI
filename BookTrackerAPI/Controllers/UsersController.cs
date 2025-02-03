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
    public class UsersController : ControllerBase
    {
        private readonly BooksContext _context;

        public UsersController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()

        {
            var users = await _context.Users
       .Include(u => u.UserProgress)
       .Include(u => u.Reviews)
       .Select(u => MapToDTO(u))
        .ToListAsync();

            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users

                .Include(u => u.UserProgress)
                .Include(u => u.Reviews)
                .Where(u => u.Id == id)
                .Select(u => MapToDTO(u))
            .FirstOrDefaultAsync();


            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users
                .Include(u => u.UserProgress)
                .Include(u => u.Reviews)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Username = userDto.Username;
            user.Email = userDto.Email;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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





        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserDTO userDto)
        {
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users
        .Include(u => u.UserProgress)
        .Include(u => u.Reviews)
        .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.UserProgresses.RemoveRange(user.UserProgress);
            _context.Reviews.RemoveRange(user.Reviews);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        //helper method to map to UserDTO
        private static UserDTO MapToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                UserProgress = user.UserProgress.Select(up => new UserProgressDTO
                {
                    BookId = up.BookId,
                    PagesRead = up.PagesRead,
                    LastUpdated = up.LastUpdated
                }).ToList(),
                Reviews = user.Reviews.Select(r => new ReviewDTO
                {
                    BookId = r.BookId,
                    Rating = r.Rating,
                    Comment = r.Comment
                }).ToList()
            };
        }
    }

}
