﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Infrastructure.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        //Mapear o banco de dados
        //Mapear tudo o que tenho para tabela de produtos
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(c => c.Dimensoes, cm =>
             {
                 //Transformando o objeto de valor 'Dimensoes'
                 //em colunas da minha tabela 'Produto'
                 cm.Property(c => c.Altura)
                 .HasColumnName("Altura")
                 .HasColumnType("int");

                 cm.Property(c => c.Largura)
                .HasColumnName("Largura")
                .HasColumnType("int");

                 cm.Property(c => c.Profundidade)
                .HasColumnName("Profundidade")
                .HasColumnType("int");
             });

            builder.ToTable("Produtos");
        }
    }
}
