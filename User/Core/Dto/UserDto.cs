namespace User.Core.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        
    }
}