using Tasks.Domain.Tasks;
using TaskStatus = Tasks.Domain.Tasks.TaskStatus;

namespace Tasks.Application.Tasks.DTOs
{
    /// <summary>
    /// Represents the full details of a task, including time logs and assignees.
    /// </summary>
    /// <param name="Id">The unique identifier of the task.</param>
    /// <param name="Title">The title of the task.</param>
    /// <param name="Description">The optional description of the task.</param>
    /// <param name="Status">The current workflow status of the task.</param>
    /// <param name="Priority">The priority level of the task.</param>
    /// <param name="Deadline">The optional deadline date and time.</param>
    /// <param name="Assignee">The assigned user details, if any.</param>
    /// <param name="CreatedBy">The user who created the task.</param>
    /// <param name="CreatedAt">The timestamp when the task was created.</param>
    /// <param name="UpdatedAt">The optional timestamp when the task was last updated.</param>
    /// <param name="Tags">The list of tag names associated with the task.</param>
    /// <param name="TimeLogs">The list of time logs logged against the task.</param>
    public record TaskDetailsResult(
        Guid Id,
        string Title,
        string? Description,
        TaskStatus Status,
        Priority Priority,
        DateTime? Deadline,
        UserResult? Assignee,
        UserResult CreatedBy,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        IReadOnlyList<string> Tags,
        IReadOnlyList<TimeLogResult> TimeLogs)
    {
        /// <summary>
        /// Gets the assigned user details.
        /// </summary>
        public UserResult? AssignedUser => Assignee;

        /// <summary>
        /// Gets the creating user details.
        /// </summary>
        public UserResult CreatedByUser => CreatedBy;
    }
}
