namespace Sims.Core;

/// <summary>
///     Represents the progress returned from an asynchronous task.
/// </summary>
public class AsyncProgressReport
{
    /// <summary>
    ///     Initializes an instance of the progress report.
    /// </summary>
    /// <param name="message">The message to be returned.</param>
    public AsyncProgressReport(string message)
    {
        Message = message;
    }

    /// <summary>
    ///     Gets the message returned from an asynchronous task.
    /// </summary>
    public string Message { get; }
}
