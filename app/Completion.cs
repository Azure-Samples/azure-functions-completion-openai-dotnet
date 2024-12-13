using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenAI.TextCompletion;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace CompletionSample;

/// <summary>
/// Defines HTTP APIs for interacting with the text completion endpoint
/// </summary>
class CompletionSample
{
    readonly ILogger<CompletionSample> logger;

    public CompletionSample(ILogger<CompletionSample> logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// HTTP POST function that sends user content to the text completion and returns results
    /// </summary>
    [Function(nameof(PostUserContent))]
    public IActionResult PostUserContent(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
            HttpRequestData req,
        [TextCompletionInput("{content}", Model = "%CHAT_MODEL_DEPLOYMENT_NAME%")] TextCompletionResponse response)
    {
        return new OkObjectResult(response.Content);
    }
}
