using System.ComponentModel.DataAnnotations;

namespace SweatSpace.Api.Business.Dtos
{
    public class LoginUserRequest
    {
        private string _userName;

        [Required]
        public string UserName { get => _userName; set => _userName = value.Trim(); }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}