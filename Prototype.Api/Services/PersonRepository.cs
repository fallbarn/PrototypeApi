using Prototype.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype.Api.DbContexts;

namespace Prototype.Api.Services
{
    /// <summary>
    /// Repoository for Person records
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        // field to hold the persistence context, all interactions with ther database will
        // happen through this
        private readonly PersonContext _context;

        public PersonRepository(PersonContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Add a person
        /// </summary>
        /// <param name="person">The person to add</param>
        public void AddPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _context.Person.Add(person);
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="person">The person to delete</param>
        public void DeletePerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            _context.Person.Remove(person);
        }

        /// <summary>
        /// Get all people
        /// </summary>
        /// <returns>IEnumberable of Person</returns>
        public IEnumerable<Person> GetPeople()
        {
            return _context.Person.ToList<Person>();
        }

        /// <summary>
        /// Get a single person
        /// </summary>
        /// <param name="Id">Id of the person to retrieve</param>
        /// <returns>Person</returns>
        public Person GetPerson(int Id)
        {
            return _context.Person.FirstOrDefault(p => p.Id == Id);
        }

        /// <summary>
        /// Check if the person exists
        /// </summary>
        /// <param name="Id">Id of the person to check</param>
        /// <returns>bool as to whether the person exists</returns>
        public bool PersonExists(int Id)
        {
            return _context.Person.Any(p => p.Id == Id);
        }

        /// <summary>
        /// Save any changes to persistence context 
        /// </summary>
        /// <returns>>bool as to whether the change was successful</returns>
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person">The person to update</param>
        public void UpdatePerson(Person person)
        {
            // no code implementation here
            // This is because changes are made to the context directly by the controller
            // as the controller has a reference to the object returned by the context
        }
    }
}
