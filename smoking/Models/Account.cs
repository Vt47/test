using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smoking.Models
{
    [Table("Account")] // Map đúng tên bảng
    public class Account
    {
        [Key]
        public int Account_ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}