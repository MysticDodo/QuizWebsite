using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleQuizApp.Models
{
    public class Quiz
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Quiz title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Quiz description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "At least one question is required.")]
        public List<Question> Questions { get; set; } = new List<Question>();
        public int ?TotalScore { get; set; }

        public bool IsSubmitted { get; set; } = false;
        public Quiz(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public Quiz() { }
        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }
        
    }
}
