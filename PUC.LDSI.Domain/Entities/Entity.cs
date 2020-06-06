using System;

namespace PUC.LDSI.Domain.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public abstract string[] Validate();
    }
}
