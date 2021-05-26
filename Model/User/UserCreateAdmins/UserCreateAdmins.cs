using System.ComponentModel.DataAnnotations;

namespace WzBeatsApi.Models
{
  public class UserCreateAdmins
  {
    [Required(ErrorMessage = "First password is missing")]
    [DataType(DataType.Password)]
    public string FirstUserPassword { get; set; }

    [Required(ErrorMessage = "Second password is missing")]
    [DataType(DataType.Password)]
    public string SecondUserPassword { get; set; }

    public UserCreateAdmins(string firstUserPassword, string secondUserPassword)
    {
      this.FirstUserPassword = firstUserPassword;
      this.SecondUserPassword = secondUserPassword;
    }
  }
}
