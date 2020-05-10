using System;

namespace IdentityServer.Quickstart.Usermanager.DTO
{
    public class UpdatePasswordCommand
    {
        public Guid UserId { get; set; }
        
        public string CurrentPassword { get; set; }
        
        public string NewPassword { get; set; }
    }
}