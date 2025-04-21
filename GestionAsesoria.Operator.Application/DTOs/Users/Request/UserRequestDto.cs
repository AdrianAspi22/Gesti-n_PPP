using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.DTOs.Users.Request
{
    public class UserRequestDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
