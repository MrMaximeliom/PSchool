using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSchool.Backend.Interfaces;
using PSchool.Backend.Models;
using PSchool.Backend.Repositories;
using System.Net.Http;

namespace PSchool.Backend.Controllers
{
    [Route("api/test")]
    [Produces("application/json")]
    [ApiController]
    public class TestController(ApplicationDbContext dbContext,IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext = dbContext;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [ProducesResponseType(StatusCodes.Status200OK)]

        [HttpGet("new-parents")]
        public  async Task<IActionResult> GetStudents()
        {
            //var result =  await _dbContext.Students.Include(p => p.Parent).ThenInclude(p => p.User).ToListAsync();
            var result = _unitOfWork.Parents.FindAllAndDive("Students", "User");
            return Ok(result);  
        }
    }
        
           
    
    
    
    
}

