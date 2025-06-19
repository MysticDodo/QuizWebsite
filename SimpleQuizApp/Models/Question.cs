using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleQuizApp.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Question text is required.")]
        public string Text { get; set; }
        [Required(ErrorMessage = "At least two options are required.")]
        [MinLength(2, ErrorMessage = "At least two options are required.")]
        public List<Option> Options { get; set; } = new List<Option>();

        public int? ChosenOption { get; set; }
        public Question(string text, List<Option> options)
        {
            Text = text;
            Options = options;
            
        }
        public Question() { }

        //Relations
        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public Quiz? Quiz { get; set; }

    }
}
