using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.DataBase.EntityConfig
{
    public class AvaliacaoConfiguration : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.Property(x => x.ProfessorId).IsRequired();

            builder.Property(x => x.Disciplina).IsRequired().HasColumnType("varchar(100)");

            builder.Property(x => x.Materia).IsRequired().HasColumnType("varchar(100)");

            builder.Property(x => x.Descricao).IsRequired().HasColumnType("varchar(100)");

            builder.HasOne(x => x.Professor)
                .WithMany(x => x.Avaliacoes)
                .HasForeignKey(x => x.ProfessorId);

            new EntityConfiguration();
        }
    }
}
