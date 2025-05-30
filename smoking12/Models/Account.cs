using System.ComponentModel.DataAnnotations;
using smoking12.Models;

public class Account
{
    [Key]
    public int Account_ID { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User User { get; set; }
    public Member Member { get; set; }
}
