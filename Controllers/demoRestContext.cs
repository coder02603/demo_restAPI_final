using Microsoft.EntityFrameworkCore;
public partial class demoRestContext : DbContext{
    public demoRestContext(){}

    public demoRestContext(DbContextOptions<demoRestContext> options) : base(options){}

    public virtual DbSet<Pokemon> Pokemon {get; set;} = null!;
}