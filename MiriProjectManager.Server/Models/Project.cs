namespace MiriProjectManager.Server.Models
{
    public class Project
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; } = null;
        public DateTime CreationDate { get; set; }
        public required string Username { get; set; }
    }
}
