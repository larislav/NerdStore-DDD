using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Infrastructure.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            //Mapear o relacionamento
            // 1 : N =>  Categorias : Produtos
            // 1 Categoria pode estar relacionada a N produtos
            // 1 Produto só possui 1 Categoria

            //Categoria tem uma lista de produtos
            builder.HasMany(c => c.Produtos)
                .WithOne(p => p.Categoria)
                .HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Categorias");
        }
    }
}
