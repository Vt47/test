using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smoking.Models
{
    [Table("User")] // Thêm dòng này để map đúng tên bảng
    public class User
    {
        [Key]
        public int Account_ID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? birthday { get; set; }
        public bool Sex { get; set; }
        public string Role { get; set; }
    }
}