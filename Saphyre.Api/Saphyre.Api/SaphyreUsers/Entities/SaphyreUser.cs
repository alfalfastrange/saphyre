using System.ComponentModel.DataAnnotations;

namespace Saphyre.Api.SaphyreUsers.Entities
{
    public class SaphyreUser
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        internal void Update(
            string firstName,
            string lastName,
            DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
        }
    }
}
