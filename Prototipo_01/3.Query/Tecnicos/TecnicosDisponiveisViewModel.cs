using System;
using System.Collections.Generic;

namespace Prototipo_01._3.Query.Tecnicos
{
    public sealed class TecnicosDisponiveisViewModel
    {
        public TecnicosDisponiveisViewModel(IEnumerable<TecnicoDisponivelViewModel> tecnicos)
        {
            Tecnicos = tecnicos;
        }

        public IEnumerable<TecnicoDisponivelViewModel> Tecnicos { get; }
    }

    public sealed class TecnicoDisponivelViewModel
    {
        public TecnicoDisponivelViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; }
        public string Nome { get; }
    }
}
