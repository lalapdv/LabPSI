using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.DataBase.EntityConfig
{
    public class OpcaoAvaliacaoConfiguration : IEntityTypeConfiguration<OpcaoAvaliacao>
    {
        public void Configure(EntityTypeBuilder<OpcaoAvaliacao> builder)
        {
            builder.Property(x => x.Descricao).IsRequired().HasColumnType("varchar(100)");

            builder.Property(x => x.Verdadeira).HasColumnType("bit");

            builder.HasOne(x => x.Questao)
                .WithMany(x => x.Opcoes)
                .HasForeignKey(x => x.QuestaoId);

            new EntityConfiguration();
        }
    }
}
