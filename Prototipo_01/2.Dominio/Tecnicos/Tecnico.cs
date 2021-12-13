using System;

namespace Prototipo_01.Dominio.Tecnicos
{
    public sealed class Tecnico
    {
        public Tecnico(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; }
        public string Nome { get; }
    }
}
