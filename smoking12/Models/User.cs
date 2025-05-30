using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int Account_ID { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birthday { get; set; }
    public bool Sex { get; set; }

    public Account Account { get; set; }
}
