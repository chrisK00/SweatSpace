using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Requests
{
    public class LoginUserRequest
    {
        private string _userName;

        [Required]
        public string UserName { get => _userName; init => _userName = value.Trim(); }

        [Required, MinLength(6)]
        public string Password { get; init; }
    }
}