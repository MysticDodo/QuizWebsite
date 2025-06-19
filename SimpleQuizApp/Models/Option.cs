using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleQuizApp.Models
{
    public class Option
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Option text is required.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "IsCorrect flag is required.")]
        public bool IsCorrect { get; set; } = false;
        public Option() { }
        public Option(string text)
        {
            Text = text;
        }

        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }
    }
}
