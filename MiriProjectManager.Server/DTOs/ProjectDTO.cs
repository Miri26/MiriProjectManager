namespace MiriProjectManager.Server.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string Username { get; set; }
    }
}
