using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserFrom { get; set; }
        [Required]
        public string UserTo { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsRead { get; set; }
    }
}

