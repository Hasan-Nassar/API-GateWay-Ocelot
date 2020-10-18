namespace User.Core.Dto
{
    public class CreateUserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
    }
}