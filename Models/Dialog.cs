using System.ComponentModel.DataAnnotations;

namespace Chat.Models
{
    public class Dialog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string User1 { get; set; }
        [Required]
        public string User2 { get; set; }
    }
}
