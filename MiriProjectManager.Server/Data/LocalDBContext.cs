using Microsoft.EntityFrameworkCore;
using MiriProjectManager.Server.Models;
using MiriProjectManager.Server.DTOs;

namespace MiriProjectManager.Server.Data
{
    public class LocalDBContext : DbContext
    {
            public DbSet<User> Users { get; set; }
            public DbSet<Project> Projects { get; set; }
            public DbSet<TaskItem> TaskItems { get; set; }

            public LocalDBContext(DbContextOptions<LocalDBContext> options) : base(options)
            {
            }
        public DbSet<MiriProjectManager.Server.DTOs.ProjectDTO> ProjectDTO { get; set; } = default!;
        public DbSet<MiriProjectManager.Server.DTOs.TaskItemDTO> TaskItemDTO { get; set; } = default!;
        public DbSet<MiriProjectManager.Server.DTOs.UserDTO> UserDTO { get; set; } = default!;
        }
    }
