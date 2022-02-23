namespace Sims.Core;

/// <summary>
///     Represents the progress returned from an asynchronous task.
/// </summary>
public class ProgressReport
{
    /// <summary>
    ///     Initializes an instance of the progress report.
    /// </summary>
    /// <param name="message">The message to be returned.</param>
    public ProgressReport(string message)
    {
        Message = message;
    }

    /// <summary>
    ///     Gets the message returned from an asynchronous task.
    /// </summary>
    public string Message { get; }
}
