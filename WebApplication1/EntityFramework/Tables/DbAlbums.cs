using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.EntityFramework.Tables;

public class DbAlbums
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime ReleaseDate { get; set; }
    public ICollection<DbSongs> Songs { get; set; } = new List<DbSongs>();
}

public class DbGroupConfiguration : IEntityTypeConfiguration<DbAlbums>
{
    public void Configure(EntityTypeBuilder<DbAlbums> builder)
    {
        builder
            .ToTable("Albums");
        builder
            .HasKey(l => l.Id);
        builder
            .HasMany(l => l.Songs)
            .WithOne(r => r.Album);
    }
}