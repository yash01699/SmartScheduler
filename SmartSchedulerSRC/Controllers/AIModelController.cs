using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartScheduler.Controllers;
using SmartScheduler.Models;
using SmartScheduler.Repositories;
using SmartScheduler.Services;

[ApiController]
[Route("api/todo")]
public class AIModelController : ControllerBase
{
    private static readonly string apiKey = "YOUR-API-KEY"; // 🔹 Replace with your Mistral API Key
    private static readonly string apiUrl = "https://api.mistral.ai/v1/chat/completions";

    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _service;

    public AIModelController(ILogger<TodoController> logger, ITodoService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    [Route("generateSchedule")]
    public async Task<OkObjectResult> Schedule()
    {

        List<TodoItem> ls = (List<TodoItem>)_service.GetTodos();
        string prompt = "Generate an optimized schedule based on these tasks and give the result in a table with clear start time and end time and few breaks in between (e.g lunch):'\n'";

        foreach (TodoItem t in ls)
        {
            if (!t.IsComplete)
            {
                String x = "Name: " + t.Name + " with priority:" + t.Priority + " which will take " + t.Time + " hrs of time.'\n'";
                prompt += x;
            }
        }

        var schedule = await GenerateSchedule(prompt);
        return Ok(schedule);
    }

    public async Task<string?> GenerateSchedule(string prompt)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "mistral-large-latest", // Use "mistral-7b" or "mistral-8x7b"
                messages = new[]
                {
                    new { role = "system", content = "You are an AI scheduling assistant." },
                    new { role = "user", content = prompt }
                }
            };

            var jsonRequest = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            string jsonResponse = await response.Content.ReadAsStringAsync();

            
            dynamic result = JsonConvert.DeserializeObject(jsonResponse);
            return result?.choices[0].message.content;   
        }
    }
}
