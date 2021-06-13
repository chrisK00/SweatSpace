using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class RegisterUserRequest
    {
        private string _userName;
        private string _email;

        [Required]
        public string UserName { get => _userName; init => _userName = value.Trim(); }

        [Required]
        public string Email { get => _email; init => _email = value.Trim(); }

        [Required, MinLength(6)]
        public string Password { get; init; }
    }
}