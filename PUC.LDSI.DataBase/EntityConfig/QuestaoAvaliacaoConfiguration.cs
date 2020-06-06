using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.DataBase.EntityConfig
{
    public class QuestaoAvaliacaoConfiguration : IEntityTypeConfiguration<QuestaoAvaliacao>
    {
        public void Configure(EntityTypeBuilder<QuestaoAvaliacao> builder)
        {
            builder.Property(x => x.Enunciado).IsRequired().HasColumnType("varchar(255)");

            builder.HasOne(x => x.Avaliacao)
                .WithMany(x => x.Questoes)
                .HasForeignKey(x => x.AvaliacaoId);

            new EntityConfiguration();
        }
    }
}
