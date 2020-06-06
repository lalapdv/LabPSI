using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PUC.LDSI.Domain.Entities;

namespace PUC.LDSI.DataBase.EntityConfig
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            //Indica que o atributo Nome é obrigatório (não nulo)
            builder.Property(x => x.Nome).IsRequired();

            //Indica que o atributo nome é do tipo varchar de 100 caracteres
            builder.Property(x => x.Nome).HasColumnType("varchar(100)");

            //HasOne(x => x.Turma) = Indica que Turma é o atributo de relacionamento (Aluno possui uma Turma)
            //WithMany(x => x.Alunos) = Indica que a lista Alunos é o atributo de relacionamento dentro da entidade relacionada (Turma possui varios Alunos)
            //HasForeignKey(x => x.TurmaId) = Indica que o atributo TurmaId deve ser considerado como a chave estrangeira (foreign key)
            builder.HasOne(x => x.Turma).WithMany(x => x.Alunos).HasForeignKey(x => x.TurmaId);

            //Importante: Aplica as configurações dos atributos comuns que estão na entidade pai Entity
            new EntityConfiguration();
        }
    }
}
