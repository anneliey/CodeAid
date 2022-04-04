using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAid.Shared
{
    public class IdentityUserDto
    {
        public string Username { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
