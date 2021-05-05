using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class RegisterUserRequest
    {
        private string _userName;
        private string _email;

        [Required]
        public string UserName { get => _userName; set => _userName = value.Trim(); }

        [Required]
        public string Email { get => _email; set => _email = value.Trim(); }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}