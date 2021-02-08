using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Api.Services;
using Prototype.Api.Models;
using Microsoft.AspNetCore.Http;
using Prototype.Api.Entities;

namespace Prototype.Api.Controllers
{
    [ApiController]
    [Route("api/persons")]
    [Produces("application/json")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        /// <summary>
        /// Get All People
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetPeople()
        {
            // get people from the repository
            IEnumerable<Person> peopleFromRepo = _personRepository.GetPeople();

            // Create Dto to be used to return data
            List<PersonDto> peopleToReturn = new List<PersonDto>();

            // Loop through each person from repository and add to the return Dto
            foreach (Person person in peopleFromRepo)
            {
                peopleToReturn.Add(new PersonDto
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName
                });
            }

            // return OK status code and Dto
            return peopleToReturn;
        }

        /// <summary>
        /// Get a person
        /// </summary>
        /// <param name="Id">The Id of the person to return</param>
        /// <returns>PersonDto</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{Id}", Name = "GetPerson")]
        public ActionResult<PersonDto> GetPerson(int Id)
        {
            // get the specific person from the repository
            Person personFromRepo = _personRepository.GetPerson(Id);

            if (personFromRepo == null)
            {
                return NotFound();
            }

            // map person from repository to the return Dto
            PersonDto personToReturn = new PersonDto
            {
                Id = personFromRepo.Id,
                FirstName = personFromRepo.FirstName,
                LastName = personFromRepo.LastName
            };

            // return OK status code and the Dto
            return Ok(personToReturn);
        }

        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="person">The person to create</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult AddPerson(PersonForCreationDto person)
        {
            // check  if the Dto we have been passed is valid
            // if not return a bad request status code
            // and the associated validation faults
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // map dto to a person entity
            Person personToAdd = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName
            };

            // add person to repository and save
            _personRepository.AddPerson(personToAdd);
            _personRepository.Save();

            PersonDto persontoReturn = new PersonDto
            {
                Id = personToAdd.Id,
                FirstName = personToAdd.FirstName,
                LastName = personToAdd.LastName
            };

            // return created status code and the details of what was added
            return CreatedAtRoute("GetPerson", new { persontoReturn.Id}, persontoReturn);

        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="Id">The Id of the person to delete</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{Id}")]
        public ActionResult DeletePerson(int Id)
        {
            // get the specific person from the repository
            Person personFromRepo = _personRepository.GetPerson(Id);

            if (personFromRepo == null)
            {
                return NotFound();
            }

            _personRepository.DeletePerson(personFromRepo);
            _personRepository.Save();

            // return success status code of type NoContent
            return NoContent();
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="Id">The Id of the person to update</param>
        /// <param name="person">The Dto containing the new person details</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        [HttpPut("{Id}")]
        public ActionResult UpdatePerson(int Id, PersonDto person)
        {
            // check  if the Dto we have been passed is valid
            // if not return a bad request status code
            // and the associated validation faults
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // get the specific person from the repository
            Person personFromRepo = _personRepository.GetPerson(Id);

            if (personFromRepo == null)
            {
                return NotFound();
            }

            // update the object received from the repository
            personFromRepo.FirstName = person.FirstName;
            personFromRepo.LastName = person.LastName;

            //update person in repository and save
            _personRepository.UpdatePerson(personFromRepo);
            _personRepository.Save();

            // return success status code of type NoContent
            return NoContent();
        }
    }
}
