using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleQuizApp.Data;
using SimpleQuizApp.Models;
using SimpleQuizApp.Services;
using System.Diagnostics;


namespace SimpleQuizApp.Controllers
{
    public class QuizController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QuizService _quizService;
        private readonly QuizLoader _quizLoader = new QuizLoader();

        public QuizController(ILogger<HomeController> logger, QuizService quizService)
        {
            _logger = logger;
            _quizService = quizService;
        }

        // Displays all quiz pre sets for my quizzes tab
        public IActionResult UserQuiz()
        {
            var quizzes = _quizService.GetUserQuizzes();
            return View(quizzes);
        }

        // Open quiz by ID (for user quizzes)
        public IActionResult OpenQuiz(int id)
        {
            var quiz = _quizService.GetUserQuizById(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // Submit quiz answers
        [HttpPost]
        public IActionResult SubmitQuiz([FromForm] Quiz quiz, string actionType)
        {

            // Check if form is valid
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid. Errors: " + ModelState.ErrorCount);
                // Log errors in the model binding for debugging
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        _logger.LogWarning($"ModelState error for {key}: {error.ErrorMessage}");
                    }
                }

                // Re-show the form
                return View("OpenQuiz", quiz);
            }
            // Check if quiz is submitted or saved
            if (actionType == "save")
            {
                // Logic to save quiz without submission
                _quizService.SaveUserQuiz(quiz); // Save the quiz to the user quiz database
                return RedirectToAction("UserQuiz");
            }
            else if (actionType == "submit")
            {
                // Logic to handle quiz submission
                quiz.TotalScore = 0; // Initialize total score
                                     // Loops through questions and options to check answers
                foreach (var question in quiz.Questions)
                {
                    foreach (var option in question.Options)
                    {
                        if (option.Id == question.ChosenOption && option.IsCorrect)
                        {
                            quiz.TotalScore++; // Increment score for correct answer
                        }
                    }
                    _logger.LogInformation(question.ChosenOption.ToString());

                }
                _quizService.SubmitUserQuiz(quiz); // Mark quiz as submitted and save to user quiz database
                return RedirectToAction("OpenQuiz", new { id = quiz.Id });
            }
            return View(quiz);
        }

        // Display quiz score
        public IActionResult QuizScore(Quiz quiz)
        {
            if (quiz == null || quiz.TotalScore < 0)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // Loads all quizzes from quiz db
        public IActionResult NewQuiz()
        {
            List<Quiz> quizzes = _quizService.GetAllQuizzes();
            return View(quizzes);
        }

        // Add a new quiz to the user quiz database
        [HttpPost]
        public IActionResult AddQuiz([FromForm]int quizId)
        {
            // get the quiz from the quiz database by ID
            Quiz quiz = _quizService.GetQuizById(quizId);
            quiz.Id = 0; // Reset the ID to ensure a new entry in the user quiz database
            foreach (var question in quiz.Questions) // Reset the IDs of questions and options
            {
                question.Id = 0;
                foreach (var option in question.Options)
                {
                    option.Id = 0;
                }
            }
            // add the quiz to the user quiz database
            _quizService.AddUserQuiz(quiz);
            _logger.LogInformation("New quiz added: " + quiz.Title);
            return RedirectToAction("UserQuiz");
        }

        // Creates a brand new quiz and saves it to the database
        // Possibly make so can pass the failed quiz from create quiz so dont have to re-enter all the data
        public IActionResult CreateQuiz()
        {
            var newQuiz = new Quiz();
            return View(newQuiz);
        }

        // Saves the new quiz created to the database
        [HttpPost]
        public IActionResult SaveQuiz(Quiz quiz)
        {
            // Validate all quizzes and their questions/options
            if (!ModelState.IsValid)
            {
                // Optionally log validation errors
                foreach (var key in ModelState.Keys)
                {
                    var errors = ModelState[key].Errors;
                    foreach (var error in errors)
                    {
                        _logger.LogWarning($"ModelState error for {key}: {error.ErrorMessage}");
                    }
                }
                // Return the form view with validation errors
                return RedirectToAction("CreateQuiz"); 
            }
            _quizService.AddQuiz(quiz); // Add the quiz to the quiz database
            _logger.LogInformation("New quiz created: " + quiz.Title);
            return RedirectToAction("NewQuiz");
        }

        // Imports a quiz from a JSON file
        public async Task<IActionResult> ImportQuiz(IFormFile jsonFile)
        {
            if (jsonFile != null && jsonFile.Length > 0)
            {
                Quiz quiz = await _quizLoader.LoadQuiz(jsonFile); // Await the Task to get the Quiz object.

                if (!ModelState.IsValid)
                {
                    // Optionally log validation errors
                    foreach (var key in ModelState.Keys)
                    {
                        var errors = ModelState[key].Errors;
                        foreach (var error in errors)
                        {
                            _logger.LogWarning($"ModelState error for {key}: {error.ErrorMessage}");
                        }
                    }
                    // Return the form view with validation errors
                    return RedirectToAction("CreateQuiz");
                }

                // Add the quiz to the database
                _quizService.AddQuiz(quiz);

                return RedirectToAction("NewQuiz");
            }

            return RedirectToAction("CreateQuiz");
        }

        public IActionResult DeleteUserQuiz(int id)
        {
            var quiz = _quizService.GetUserQuizById(id);
            if (quiz != null)
            {
                _quizService.DeleteUserQuiz(id); // Delete the quiz from the user quiz database
                _logger.LogInformation("Quiz deleted: " + quiz.Title);
            }
            return RedirectToAction("UserQuiz");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
