using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeAid.Shared
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; } = String.Empty;
        public DateTime PostDate { get; set; }
        public bool MessageEdit { get; set; }


        // Relations
        [ForeignKey(nameof(Thread))]
        public int ThreadId { get; set; }
        public ThreadModel Thread { get; set; }

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public UserModel? User { get; set; }
    }
}
