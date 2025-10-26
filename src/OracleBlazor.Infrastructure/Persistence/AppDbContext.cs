using OracleBlazor.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OracleBlazor.Application.Interfaces;
using OracleBlazor.Infrastructure.Repositories;

namespace OracleBlazor.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ICurrentUser _currentUser;
    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUser currentUser) : base(options)
    {
        _currentUser = currentUser;
    }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        _currentUser = new NullCurrentUser();
    }

    // DbSets
    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<User> Users => Set<User>();
    public DbSet<IP> AllowedIps => Set<IP>();

    // --- GLOBAL CONVENTIONS ---
    // EF Core 7+ : Tüm bool'lar NUMBER(1), tüm decimal'lar NUMBER(18,2)
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<bool>()
               .HaveConversion(typeof(BoolToZeroOneConverter<short>))
               .HaveColumnType("NUMBER(1)");

        builder.Properties<decimal>()
               .HavePrecision(18, 2); // eşdeğeri: .HaveColumnType("NUMBER(18,2)")
    }

    protected override void OnModelCreating(ModelBuilder b)
    {
        // Enumları string saklayalım (NVARCHAR2)
        var catConv = new EnumToStringConverter<AssetCategory>();
        var locConv = new EnumToStringConverter<AssetLocation>();
        var statConv = new EnumToStringConverter<AssetStatus>();
        var boolToShort = new BoolToZeroOneConverter<short>();

        b.Entity<IP>(e =>
        {
            e.ToTable("ALLOWED_IPS");
            e.HasKey(x => x.Id).HasName("PK_ALLOWED_IPS");
            e.Property(x => x.Id)
             .HasColumnType("RAW(16)");

            e.Property(x => x.Ip).HasColumnType("NVARCHAR2(32)").IsRequired();
            e.Property(x => x.CreatedAt).HasColumnType("TIMESTAMP(7)").IsRequired();
            e.Property(x => x.UpdatedAt).HasColumnType("TIMESTAMP(7)").IsRequired();

            e.Property(x => x.CreatedBy).HasColumnType("RAW(16)");
            e.Property(x => x.UpdatedBy).HasColumnType("RAW(16)");
        });

        // ASSETS
        b.Entity<Asset>(e =>
        {
            e.ToTable("ASSETS");
            e.HasKey(x => x.Id).HasName("PK_ASSETS");
            e.Property(x => x.Id)
             .HasColumnType("RAW(16)");

            // Kolonlar
            e.Property(x => x.Tag).HasMaxLength(64).IsRequired();
            e.Property(x => x.Name).HasMaxLength(128).IsRequired();

            e.Property(x => x.Category).HasConversion(catConv).HasMaxLength(32);
            e.Property(x => x.Location).HasConversion(locConv).HasMaxLength(32);
            e.Property(x => x.Status).HasConversion(statConv).HasMaxLength(32);
            e.Property(x => x.PurchaseDate).HasColumnType("TIMESTAMP(7)");
            e.Property(x => x.CreatedAt).HasColumnType("TIMESTAMP(7)").IsRequired();
            e.Property(x => x.UpdatedAt).HasColumnType("TIMESTAMP(7)").IsRequired();

            e.Property(x => x.CreatedBy).HasColumnType("RAW(16)");
            e.Property(x => x.UpdatedBy).HasColumnType("RAW(16)");



            e.HasIndex(x => new { x.Category, x.Location })
             .HasDatabaseName("IX_ASSETS_CAT_LOC");
        });

        // USERS (istersen APP_USERS yapabilirsin)
        b.Entity<User>(e =>
        {
            e.ToTable("USERS");
            e.HasKey(x => x.Id).HasName("PK_USERS");

            e.Property(x => x.Id).HasColumnType("RAW(16)");
            e.Property(x => x.UserName).HasMaxLength(128).IsRequired();
            e.Property(x => x.Password).HasMaxLength(128).IsRequired();
            e.Property(x => x.RealName).HasMaxLength(128).IsRequired();

            e.Property(x => x.CreatedAt).HasColumnType("TIMESTAMP(7)").IsRequired();
            e.Property(x => x.UpdatedAt).HasColumnType("TIMESTAMP(7)").IsRequired();
            e.Property(x => x.CreatedBy).HasColumnType("RAW(16)");
            e.Property(x => x.UpdatedBy).HasColumnType("RAW(16)");

            // Bool'lar global convention'dan NUMBER(1) olur
        });
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;
        foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity is IEntity entity))
        {

            if (entry.Entity is IEntity entity)
            {
                switch (entry.State)
                {

                    case EntityState.Added:
                        entity.CreatedAt = DateTime.Now;
                        entity.CreatedBy = new Guid(userId);
                        break;
                    case EntityState.Modified:
                        entity.UpdatedAt = DateTime.Now;
                        entity.UpdatedBy = new Guid(userId);
                        break;
                    case EntityState.Deleted:
                        entity.IsDeleted = true;
                        entity.UpdatedAt = DateTime.Now;
                        entity.UpdatedBy = new Guid(userId);

                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}


public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var cs = config.GetConnectionString("Oracle")
                 ?? throw new InvalidOperationException("OracleDb connection string missing.");

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseOracle(cs, o =>
            {
                // Migration’ları bu assembly’de tutuyorsanız:
                // o.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            })
            .Options;

        return new AppDbContext(options);
    }
}