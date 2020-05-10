using System;

namespace IdentityServer.Quickstart.Usermanager.DTO
{
    public class UpdateEmailCommand
    {
        public Guid UserId { get; set; }
        
        public string Email { get; set; }
    }
}