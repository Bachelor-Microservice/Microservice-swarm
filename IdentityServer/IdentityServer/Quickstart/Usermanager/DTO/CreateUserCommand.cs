namespace IdentityServer.Quickstart.Usermanager.DTO
{
    public class CreateUserCommand
    {
        
        public string Username { get; set; }

        public string Email { get; set; }
        
        public string Password { get; set; }
        
    }
}