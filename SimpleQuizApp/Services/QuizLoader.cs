using SimpleQuizApp.Models;
using System.Text.Json;

namespace SimpleQuizApp.Services

// currently unused, but may be used in the future for editing or adding new quizzes
{
    // This class is responsible for loading quizzes from a json file
    public class QuizLoader
    {
        // Load quizzes from the specified file path
        public async Task<Quiz> LoadQuiz(IFormFile quizFile)
        {
            using var stream = new StreamReader(quizFile.OpenReadStream());
            var json = await stream.ReadToEndAsync();
            return JsonSerializer.Deserialize<Quiz>(json);
        }


        //Save quizzes to the specified file path, for if editing or adding new quizzes
        //public void SaveQuizzes(List<Quiz> quizzes)
        //{
        //    var json = JsonSerializer.Serialize(quizzes, new JsonSerializerOptions { WriteIndented = true });
        //    File.WriteAllText(_filePath, json);
        //}
    }
}
