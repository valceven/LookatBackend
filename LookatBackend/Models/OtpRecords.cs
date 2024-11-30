using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LookatBackend.Models
{
    public class OtpRecords
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(15)")]
        public string MobileNumber { get; set; }
        public int Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
