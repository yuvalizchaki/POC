using Microsoft.EntityFrameworkCore;

namespace POC.Infrastructure;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{

}