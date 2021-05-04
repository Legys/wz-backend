namespace WzBeatsApi.Models
{

  public class User
  {

    public long Id { get; set; }
    public string Nickname { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }

    public User(string Nickname, string Password, bool IsAdmin)
    {
      this.Nickname = Nickname;
      this.Password = Password;
      this.IsAdmin = IsAdmin;

    }
  }
}
