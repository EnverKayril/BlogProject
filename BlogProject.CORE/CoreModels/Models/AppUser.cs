using BlogProject.CORE.CoreModels.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlogProject.CORE.CoreModels.Models
{
    public class AppUser : IdentityUser , IBaseEntity
    {
        private string firstName;
        private string normalizedFirstName;
        private string lastName;
        private string normalizedLastName;

        public string FirstName 
        {
            get { return firstName; }
            set {  firstName = value; } 
        }

        public string NormalizedFirstName
        {
            get { return normalizedFirstName; }
            set { normalizedFirstName = firstName.ToUpper(); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string NormalizedLastName
        {
            get { return normalizedLastName; }
            set { normalizedLastName = lastName.ToUpper(); }
        }

        public string? Photo { get; set; }
        public virtual int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
