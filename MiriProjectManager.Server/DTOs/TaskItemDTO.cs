namespace MiriProjectManager.Server.DTOs
{
    public class TaskItemDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public required bool IsCompleted { get; set; } = false;
        public required int ProjectId { get; set; }
    }
}
