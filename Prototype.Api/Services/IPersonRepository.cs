using Prototype.Api.Entities;
using System.Collections.Generic;

namespace Prototype.Api.Services
{
    /// <summary>
    /// Interface for the Person Repository
    /// </summary>
    public interface IPersonRepository
    {
        /// <summary>
        /// Get All People
        /// </summary>
        /// <returns></returns>
        IEnumerable<Person> GetPeople();

        /// <summary>
        /// Get one person by the Id
        /// </summary>
        /// <param name="Id">Id of the person to return</param>
        /// <returns>A person </returns>
        Person GetPerson(int Id);

        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="person">Person to be added</param>
        void AddPerson(Person person);

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="person">Person to be deleted</param>
        void DeletePerson(Person person);

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person">Person to be updated</param>
        void UpdatePerson(Person person);

        /// <summary>
        /// Check the person exists
        /// </summary>
        /// <param name="Id">Id of the person the checl</param>
        /// <returns>bool as to whether the person exists</returns>
        bool PersonExists(int Id);

        /// <summary>
        /// Save the changes
        /// </summary>
        /// <returns>bool as to whether the change was successful</returns>
        bool Save();
    }
}
