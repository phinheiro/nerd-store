using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NerdStore.Catalogo.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(p => p.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(p => p.Dimensoes, cm =>
            {
                cm.Property(p => p.Largura)
                    .HasColumnName("Largura")
                    .HasColumnType("int");

                cm.Property(p => p.Profundidade)
                    .HasColumnName("Profundidade")
                    .HasColumnType("int");

                cm.Property(p => p.Altura)
                    .HasColumnName("Altura")
                    .HasColumnType("int");
            });

            builder.ToTable("Produtos");
        }
    }
}
