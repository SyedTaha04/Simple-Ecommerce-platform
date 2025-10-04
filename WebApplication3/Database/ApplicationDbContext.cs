using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace WebApplication3.Database;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    public DbSet<SignUpPage> SignupPage { get; set; }
    public DbSet<Products> Products { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Images> Images { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    
}