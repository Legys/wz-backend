using System.ComponentModel.DataAnnotations;

namespace WzBeatsApi.Models
{
  public class UserRegister
  {
    [Required(ErrorMessage = "Nickname is empty")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Password is missing")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords are not matches")]
    public string ConfirmPassword { get; set; }
  }
}
