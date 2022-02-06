namespace Sims.Toolkit.Api.Helpers;

/// <summary>
/// Represents the progress returned from an asynchronous task.
/// </summary>
public class ProgressReport
{
    /// <summary>
    /// Gets the message returned from an asynchronous task.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Initializes an instance of the progress report.
    /// </summary>
    /// <param name="message">The message to be returned.</param>
    public ProgressReport(string message)
    {
        this.Message = message;
    }
}
