using System.ComponentModel.DataAnnotations;

namespace WzBeatsApi.Models
{
  public class UserLogin
  {
    [Required(ErrorMessage = "Nickname is missing")]
    public string Nickname { get; set; }

    [Required(ErrorMessage = "Password is missing")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public UserLogin(string Nickname, string Password)
    {
      this.Nickname = Nickname;
      this.Password = Password;
    }
  }
}
