using System.ComponentModel.DataAnnotations;

namespace TaskAlfa.Data.Models
{
    public class ErrorMessage
    {
        [Required]
        public string Message { get; set; }

        public string UserData { get; set; }
    }
}
