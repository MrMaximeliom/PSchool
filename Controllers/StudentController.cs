using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PSchool.Backend.DataTransferObjects;
using PSchool.Backend.Interfaces;
using PSchool.Backend.Models;

namespace PSchool.Backend.Controllers
{
    [Route("api/students")]
    [Produces("application/json")]
    [ApiController]
    public class StudentController(IUnitOfWork unitOfWork,ApplicationDbContext context) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ApplicationDbContext _context = context;


        /// <summary>
        /// Gets all students.
        /// </summary>
        /// 
        /// <returns>A list of all students</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Students
        ///
        /// </remarks>
        /// <response code="200">Returns all students</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {

            var result = await _unitOfWork.Students.GetAllAsync();
            var resultDto = result.Adapt<IEnumerable<StudentDto>>();

            return Ok(resultDto);


        }

        /// <summary>
        /// Gets parents full names
        /// </summary>
        /// 
        /// <returns>A list of parents with thier full names</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /students/parents-names
        ///
        /// </remarks>
        /// <response code="200">Returns parents with their full names</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("parents-names")]
        public async Task<IActionResult> GetParentsNamesAsync()
        {

            var result = await _unitOfWork.Students.GetProperty(p => p.Parent != null,t => t.Parent!.User.FullName,true);
         

            return Ok(result);


        }
        /// <summary>
        /// Gets all students with parents
        /// </summary>
        /// 
        /// <returns>A list of all students with their parents</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Students
        ///
        /// </remarks>
        /// <response code="200">Returns all students with their parents details</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("with-parents")]
        public async Task<IActionResult> GetAllStudentsWithParentsAsync()
        {
            var resultParentWithNoInfo = await _context.Students.Include(p => p.Parent).ThenInclude(p => p.User).Select(
               s => new StudentDto
               {
                   Id = s.Id,
                   FirstName = s.FirstName,
                   LastName = s.LastName,
                   ParentName = s.Parent.User.FullName,
                   ParentId = s.Parent.Id,
               }


                ).ToListAsync();
            //var resultParentWithNoInfo = await _context.Students.Include(p => p.Parent).ThenInclude(p => p.User).ToListAsync();


            var resultDto = resultParentWithNoInfo.Adapt<IEnumerable<StudentDto>>();


            return Ok(resultDto);


        }
        /// <summary>
        /// Gets a student by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested student by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Students/{id:int}
        ///
        /// </remarks> 
        /// <response code="200">Returns student with submitted id</response>
        /// <response code="404">Returns not found if there is no student with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {

            var result = await _unitOfWork.Students.GetByIdAsync(id);
            var resultDto = result.Adapt<StudentDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Adds new student 
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="parentId"></param>
        /// <returns>a newly created student</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Students
        ///     {
        ///        "id": 1,
        ///        "firstName": "Ahmed",
        ///        "lastName": "Sami",
        ///        "parentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added student</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddStudentAsync(StudentDto StudentDto)
        {

            var result = await _unitOfWork.Students.AddAsync(StudentDto.Adapt<Student>());
            var resultDto = result.Adapt<StudentDto>();
            _unitOfWork.Complete();
            return Ok(resultDto);


        }

        /// <summary>
        /// Update a Student
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="parentId"></param>
        /// <returns>an updated Student </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Students/{id:int}
        ///     {
        ///        "id": 1,
        ///        "firstName": "Ahmed",
        ///        "lastName": "Sami",
        ///        "parentId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated Student</response>
        /// <response code="404">if there is no Student record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudentAsync(int id, StudentDto StudentDto)
        {

            var entity = _unitOfWork.Students.GetById(id);

            if (!ModelState.IsValid || StudentDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.FirstName = StudentDto.FirstName;
            entity.LastName = StudentDto.LastName;
            entity.ParentId = StudentDto.ParentId;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update partial data for Student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="operationType"></param>
        /// <param name="path"></param>
        /// <param name="op"></param>
        /// <param name="from"></param>
        /// <param name="value"></param>
        /// <returns> no content </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PATCH /api/Students/{id:int}
        ///     [ 
        ///       {
        ///       "operationType": 0,
        ///       "path": "/propertyName",
        ///       "op": "replace",
        ///       "from": "string",
        ///       "value": "string"
        ///       }
        ///     ]
        ///        
        ///     
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="400">If submitted data was wrong</response>
        /// <response code="404">If there is no record with submitted id</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPatch("{id:int}")]
        public IActionResult UpdateStudentPartial(int id, JsonPatchDocument<Student> Student)

        {
            var entity = _unitOfWork.Students.GetById(id);
            if (!ModelState.IsValid || Student is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            Student.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Students.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a Student
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that Student deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Students/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a Student</response>
        /// <response code="404">if there is no Student with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudentAsync(int id)
        {

            var entity = _unitOfWork.Students.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Students.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
