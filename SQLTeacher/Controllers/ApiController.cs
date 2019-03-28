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
            score.Success = currentQuery.checkStatement(response.query);
            _context.Update(score);
            _context.SaveChanges();
            return score.Success;
        }

        [HttpPost("checkPinCode")]
        public Boolean checkPinCode(QueryResponse response)
        {
            People currentStudent = _context.People.Where(f => f.Role.Name == "student").FirstOrDefault(p => p.PinCode == response.pinCode);
            return currentStudent != null;
        }

        [HttpGet("scores")]
        public IEnumerable<Scores> getScores()
        {
            return _context.Scores;
        }
    }
}