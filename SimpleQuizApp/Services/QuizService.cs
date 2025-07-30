using Microsoft.EntityFrameworkCore;
using SimpleQuizApp.Data;
using SimpleQuizApp.Models;

namespace SimpleQuizApp.Services
{
    // Handles retrieving and processing quiz data to db
    public class QuizService
    {
        private readonly QuizDbContext _quiz_context;
        private readonly UserQuizDbContext _user_quiz_context;

        public QuizService(QuizDbContext quiz_context, UserQuizDbContext user_quiz_context)
        {
            _quiz_context = quiz_context;
            _user_quiz_context = user_quiz_context;
        }

        // Template quizzes

        // Adds a new quiz to the quiz database
        public void AddQuiz(Quiz quiz)
        {
            if (quiz != null)
            {
                _quiz_context.Quizzes.Add(quiz);
                _quiz_context.SaveChanges();
            }
        }

        // Retrieves all quizzes from the database
        public List<Quiz> GetAllQuizzes()
        {
            return _quiz_context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).ToList();
        }

        // Retrieves a quiz by its ID
        public Quiz GetQuizById(int id)
        {
            return _quiz_context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).FirstOrDefault(q => q.Id == id);
        }

        // User quizzes

        // Saves a quiz to the user quiz database
        public void SaveUserQuiz(Quiz quiz)
        {
            if (quiz != null)
            {
                _user_quiz_context.Quizzes.Update(quiz);
                _user_quiz_context.SaveChanges();
            }
        }
        // Retrieves all user quizzes
        public List<Quiz> GetUserQuizzes()
        {
            return _user_quiz_context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).ToList();
        }
        // Retrieves a user quiz by its ID
        public Quiz GetUserQuizById(int id)
        {
            return _user_quiz_context.Quizzes.Include(q => q.Questions).ThenInclude(q => q.Options).FirstOrDefault(q => q.Id == id);
        }
        // Submits a quiz and marks it as submitted
        public void SubmitUserQuiz(Quiz quiz)
        {
            if (quiz != null)
            {
                quiz.IsSubmitted = true; // Mark quiz as submitted
                _user_quiz_context.Quizzes.Update(quiz); // Update quiz in the user quiz database
                _user_quiz_context.SaveChanges();
            }
        }
        // Deletes a quiz by its ID
        public void DeleteUserQuiz(int id)
        {
            var quiz = _user_quiz_context.Quizzes.Find(id);
            if (quiz != null)
            {
                _user_quiz_context.Quizzes.Remove(quiz);
                _user_quiz_context.SaveChanges();
            }
        }

        // Adds a new quiz to the quiz database
        public void AddUserQuiz(Quiz quiz)
        {
            if (quiz != null)
            {
                _user_quiz_context.Quizzes.Add(quiz);
                _user_quiz_context.SaveChanges();
            }
        }
    }
}
