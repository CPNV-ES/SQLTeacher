using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLTeacher.Models;

namespace SQLTeacher.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        // Create struct for json binding
        public struct QueryResponse
        {
            public int pinCode;
            public string query;
        }

        private readonly SQLTeacherContext _context;

        public ApiController(SQLTeacherContext context)
        {
            _context = context;
        }

        [HttpPost("checkAnswer/{id}")]
        public Boolean checkAnswer(int id, QueryResponse response)
        {
            People currentStudent = _context.People.FirstOrDefault(p => p.PinCode == response.pinCode);
            Queries currentQuery = _context.Queries.FirstOrDefault(q => q.Id == id);
            if (currentStudent == null || currentQuery == null)
            {
                return false;
            }
            Scores score = _context.Scores.FirstOrDefault(s => s.PeopleId == currentStudent.Id && s.QuerieId == currentQuery.Id);
            // Check if response is good
            if (score == null)
            {
                score = new Scores();
                score.People = currentStudent;
                score.Querie = currentQuery;
            }

            score.Attempts += 1;
            score.Success = Convert.ToInt32(currentQuery.checkStatement(response.query));
            _context.Update(score);
            _context.SaveChanges();
            return true;
        }

       /* // GET: api/Api
        [HttpGet("queries")]
        public IEnumerable<Queries> GetQueries()
        {
            return _context.Queries;
        }

        // GET: api/Api/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQueries([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queries = await _context.Queries.FindAsync(id);

            if (queries == null)
            {
                return NotFound();
            }

            return Ok(queries);
        }

        // PUT: api/Api/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQueries([FromRoute] int id, [FromBody] Queries queries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != queries.Id)
            {
                return BadRequest();
            }

            _context.Entry(queries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QueriesExists(id))
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

        // POST: api/Api
        [HttpPost]
        public async Task<IActionResult> PostQueries([FromBody] Queries queries)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Queries.Add(queries);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQueries", new { id = queries.Id }, queries);
        }

        // DELETE: api/Api/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQueries([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var queries = await _context.Queries.FindAsync(id);
            if (queries == null)
            {
                return NotFound();
            }

            _context.Queries.Remove(queries);
            await _context.SaveChangesAsync();

            return Ok(queries);
        }

        private bool QueriesExists(int id)
        {
            return _context.Queries.Any(e => e.Id == id);
        }*/
    }
}