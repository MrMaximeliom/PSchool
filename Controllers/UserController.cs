﻿using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PSchool.Backend.DataTransferObjects;
using PSchool.Backend.Interfaces;
using PSchool.Backend.Models;

namespace PSchool.Backend.Controllers
{
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UserController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;


        /// <summary>
        /// Gets all users.
        /// </summary>
        /// 
        /// <returns>A list of all users</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /users
        ///
        /// </remarks>
        /// <response code="200">Returns all users</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {

            var result = await _unitOfWork.Users.GetAllAsync();
            var resultDto = result.Adapt<IEnumerable<UserDto>>();

            return Ok(result);


        }
        /// <summary>
        /// Gets a users by submitted id
        /// </summary> 
        /// <param name="id"></param>
        /// <returns>requested users by id</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /users/{id:int}
        ///
        /// </remarks> 
        /// <response code="200">Returns users with submitted id</response>
        /// <response code="404">Returns not found if there is no users with submitted id</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {

            var result = await _unitOfWork.Users.GetByIdAsync(id);
            var resultDto = result.Adapt<UserDto>();

            return Ok(resultDto);


        }

        /// <summary>
        /// Update a user
        /// </summary> 
        /// <param name="id"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="parentId"></param>
        /// <returns>an updated users </returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /users/{id:int}
        ///     {
        ///        "id": 1,
        ///        "firstName": "Ahmed",
        ///        "lastName": "Sami",
        ///        "parentId": 1,
        ///        "email":"example@example.com",
        ///        
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns updated users</response>
        /// <response code="404">if there is no users record with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public IActionResult UpdateUserAsync(string id, UserDto UserDto)
        {

            var entity = _unitOfWork.Users.GetById(id);

            if (!ModelState.IsValid || UserDto is null)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }

            entity.FirstName = UserDto.FirstName;
            entity.LastName = UserDto.LastName;
            entity.Email = UserDto.Email;
            entity.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Complete();

            return Ok(entity);



        }

        /// <summary>
        /// Update partial data for a user
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
        ///     PATCH /api/users/{id:int}
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
        [HttpPatch("{id}")]
        public IActionResult UpdateStudentPartial(string id, JsonPatchDocument<User> users)

        {
            var entity = _unitOfWork.Users.GetById(id);
            if (!ModelState.IsValid || users is null)
                return BadRequest("Something went wrong");

            if (entity is null)
                return NotFound($"No record with this Id:{id}");



            users.ApplyTo(entity);
            entity.UpdatedAt = DateTime.UtcNow;


            _unitOfWork.Users.Update(entity);

            _unitOfWork.Complete();
            return NoContent();

        }

        /// <summary>
        /// Delete a user
        /// <param name="id"></param>
        /// <returns>message shows that users deleted successfully</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /users/{id:int}
        ///
        /// </remarks>
        /// <response code="200">Deletes a users</response>
        /// <response code="404">if there is no users with sumitted id</response>
        /// <response code="400">if submitted data was wrong</response>
        /// </summary> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult DeleteUserAsync(string id)
        {

            var entity = _unitOfWork.Users.GetById(id);

            if (!ModelState.IsValid)
            {
                return BadRequest("Something went wrong");

            }
            if (entity is null)
            {
                return NotFound($"No record with this id: {id}");

            }
            _unitOfWork.Users.Delete(entity);

            _unitOfWork.Complete();

            return Ok(entity);



        }
    }
}
