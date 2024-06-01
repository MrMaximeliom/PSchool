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
    [Route("api/parents")]
    [Produces("application/json")]
    [ApiController]
    public class ParentController(IUnitOfWork unitOfWork, ApplicationDbContext context) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly ApplicationDbContext _context = context;
        /// <summary>
        /// Gets all parents.
        /// </summary>
        /// 
        /// <returns>A list of all parents</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /parents
        ///
        /// </remarks>
        /// <response code="200">Returns all parents</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllParentsAsync()
        {

            var result = await _unitOfWork.Parents.GetAllAsync();
            var resultDto = result.Adapt<IEnumerable<ParentDto>>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets parents with details
        /// </summary>
        /// 
        /// <returns>A list of all parents with more details</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /parents/update-details
        ///
        /// </remarks>
        /// <response code="200">Returns all parents with more details</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("with-details/{parentId:int}")]
        public async Task<IActionResult> GetParentsWithDetailsByIdAsync(int parentId )
        {



            var result = await _unitOfWork.Parents.GetParentWithDetailsById(parentId);
            //var resultDto = result.Adapt<IEnumerable<ParentDto>>();

            return Ok(result);


        }
        /// <summary>
        /// Gets parents with details
        /// </summary>
        /// 
        /// <returns>A list of all parents with more details</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /parents/with-details
        ///
        /// </remarks>
        /// <response code="200">Returns all parents with more details</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("with-details")]
        public async Task<IActionResult> GetParentsWithDetailsAsync()
        {



            var result = await _unitOfWork.Parents.GetParentsWithDetails();
            var resultDto = result.Adapt<IEnumerable<ParentDto>>();

            return Ok(result);


        }
        /// <summary>
        /// Gets only parents with students
        /// </summary>
        /// 
        /// <returns>A list of only parents whith students </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /parents
        ///
        /// </remarks>
        /// <response code="200">Returns all parents that have at least one student</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("have-students")]
        public async Task<IActionResult> GetOnlyParentsWithStudentsAsync()
        {

            var result = await _unitOfWork.Parents.GetAllAsync();
            var resultDto = result.Adapt<IEnumerable<ParentDto>>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets only parents with students with Parent Full Name and Id only
        /// </summary>
        /// 
        /// <returns>A list of only parents (Full name and Id only) whith students </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /parents
        ///
        /// </remarks>
        /// <response code="200">Returns all parents (Full name and Id only) that have at least one student</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("with-short-details")]
        public async Task<IActionResult> GetOnlyParentsWithStudentsShortDetailsAsync()
        {

            var result = await _unitOfWork.Parents.GetAllAsync();
            var resultDto = result.Adapt<IEnumerable<ParentDto>>();

            var response = await _context.Parents.Include(p => p.User).Select(
            p => new
            {
                p.Id,
                p.FullName,
            }).ToListAsync();

            return Ok(response);


        }
        /// <summary>
        /// Gets parents with their students
        /// </summary>
        /// 
        /// <returns>A list of parents and their students </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /parents
        ///
        /// </remarks>
        /// <response code="200">Returns all parents that have at least one student</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("students")]
        public async Task<IActionResult> GetParentsAndStudentsAsync()
        {
            //var result =  await _dbContext.Students.Include(p => p.Parent).ThenInclude(p => p.User).ToListAsync();

            // var result = await _context.Parents.Include(p => p.User).Include(p => p.Students).ToListAsync();
            var result = await _context.Parents.Include(p => p.Students).Include(p => p.User).ToListAsync();
            var resultDto = result.Adapt<IEnumerable<ParentDto>>();

            return Ok(resultDto);


        }
        /// <summary>
        /// Gets parent  by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested parent by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Parents/{id:int}
        ///
        /// </remarks> 
        /// <response code="200">Returns parent with submitted id</response>
        /// <response code="404">Returns not found if there is no parent with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetParentByIdAsync(int id)
        {

            var result = await _unitOfWork.Parents.GetByIdAsync(id);
            var resultDto = result.Adapt<ParentDto>();

            return Ok(resultDto);


        }
        /// <summary>
        /// update parent  by id with details
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested parent by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Parents/{id:int}/with-details
        ///
        /// </remarks> 
        /// <response code="200">Returns parent with submitted id</response>
        /// <response code="404">Returns not found if there is no parent with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("update-details/{id:int}")]
        public async Task<IActionResult> UpdateParentWithDetailsById(int id, ParentDto ParentDto)
        {

            var entity = await _unitOfWork.Parents.GetParentWithDetailsById(id);
           
            if (!ModelState.IsValid || ParentDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.WorkPhone = ParentDto.WorkPhone;
            entity.HomeAddress = ParentDto.HomeAddress;
            entity.Siblings = ParentDto.Siblings;
            entity.UserId = ParentDto.UserId;
            entity.UpdatedAt = DateTime.UtcNow;
            // update parent basic details
            entity.UserId = ParentDto.UserId;
            entity.User.FirstName = ParentDto.User.FirstName ?? entity.User.FirstName;
            entity.User.LastName = ParentDto.User.LastName ?? entity.User.LastName;
            entity.User.Email = ParentDto.User.Email ?? entity.User.Email;
            entity.User.UpdatedAt = DateTime.UtcNow;

            var resultDto = entity.Adapt<ParentDto>();
            _unitOfWork.Complete();


            return Ok(resultDto);


        }
     
        /// <summary>
        /// Adds new parent 
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="workPhone"></param>
        /// <param name="homeAddress"></param>
        /// <param name="siblings"></param>
        /// <param name="userId"></param>
        /// <returns>a newly created parent</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Parents
        ///     {
        ///        "id": 1,
        ///        "workPhone": "0564534231",
        ///        "homeAddress": "address",
        ///        "siblings": 2,
        ///        "userId": "user-id"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns newly added parent</response>
        /// <response code="400">Returns bad request if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddParentAsync(ParentDto ParentDto)
        {

            var result = await _unitOfWork.Parents.AddAsync(ParentDto.Adapt<Parent>());
            var resultDto = result.Adapt<ParentDto>();

            _unitOfWork.Complete();
            return Ok(resultDto);


        }

        /// <summary>
        /// Update a parent
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="workPhone"></param>
        /// <param name="homeAddress"></param>
        /// <param name="siblings"></param>
        /// <param name="userId"></param>
        /// <returns>an updated parent </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Parents/{id:int}
        ///     {
        ///        "id": 1,
        ///        "workPhone": "0564534231",
        ///        "homeAddress": "address",
        ///        "siblings": 2,
        ///        "userId": "user-id"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated parent</response>
        /// <response code="404">if there is no parent record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}")]
        public IActionResult UpdateParentAsync(int id, ParentDto ParentDto)
        {

            var entity = _unitOfWork.Parents.GetById(id);

            if (!ModelState.IsValid || ParentDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.WorkPhone = ParentDto.WorkPhone;
            entity.HomeAddress = ParentDto.HomeAddress;
            entity.Siblings = ParentDto.Siblings;
            entity.UserId = ParentDto.UserId;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update a parent with user details
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="workPhone"></param>
        /// <param name="homeAddress"></param>
        /// <param name="siblings"></param>
        /// <param name="userId"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="phoneNumber"></param>
        /// <returns>an updated parent </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Parents/{id:int}
        ///     {
        ///        "id": 1,
        ///        "workPhone": "0564534231",
        ///        "homeAddress": "address",
        ///        "siblings": 2,
        ///        "user":{
        ///        "firstName": "Ahmed",
        ///        "lastName": "Ali",
        ///        "email": "user@example.com",
        ///        "phoneNumber": "0505432345"
        ///        }
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated parent</response>
        /// <response code="404">if there is no parent record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}/with-details")]
        public async Task<IActionResult> UpdateParentWithDetailsAsync(int id, ParentDto ParentDto)
        {

            var entity = await _unitOfWork.Parents.GetParentWithDetailsById(id);

            if (!ModelState.IsValid || ParentDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            if (entity.User != null && ParentDto.User != null)
            {
                entity.User.FirstName = ParentDto.User.FirstName;
                entity.User.LastName = ParentDto.User.LastName ?? entity.User.LastName;
                entity.User.Email = ParentDto.User.Email ?? entity.User.Email;
                entity.User.PhoneNumber = ParentDto.User.PhoneNumber ?? entity.User.PhoneNumber;
                entity.User.UpdatedAt = DateTime.UtcNow;

            }
            entity.WorkPhone = ParentDto.WorkPhone;
            entity.HomeAddress = ParentDto.HomeAddress;
            entity.Siblings = ParentDto.Siblings;
            entity.UpdatedAt = DateTime.UtcNow;
         

            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update partial data for parent
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
        ///     PATCH /api/Parents/{id:int}
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
        public IActionResult UpdateParentPartial(int id, JsonPatchDocument<Parent> parent)

        {
            var entity = _unitOfWork.Parents.GetById(id);
            if (!ModelState.IsValid || parent is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            parent.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Parents.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a parent
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>message shows that parent deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Parents/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a parent</response>
        /// <response code="404">if there is no parent with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteParentAsync(int id)
        {

            var entity = _unitOfWork.Parents.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Parents.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
