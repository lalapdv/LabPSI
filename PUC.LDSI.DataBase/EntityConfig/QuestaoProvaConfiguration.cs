using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.DataBase.EntityConfig
{
    public class QuestaoProvaConfiguration : IEntityTypeConfiguration<QuestaoProva>
    {
        public void Configure(EntityTypeBuilder<QuestaoProva> builder)
        {
            builder.Property(x => x.Nota).HasColumnType("decimal(10,4)");

            builder.HasOne(x => x.QuestaoAvaliacao)
                .WithMany(x => x.QuestoesProva)
                .HasForeignKey(x => x.QuestaoId);

            builder.HasOne(x => x.Prova)
                .WithMany(x => x.QuestoesProva)
                .HasForeignKey(x => x.ProvaId);

            new EntityConfiguration();
        }
    }
}
