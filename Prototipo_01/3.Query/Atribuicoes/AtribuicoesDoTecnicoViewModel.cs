using System;
using System.Collections.Generic;

namespace Prototipo_01.Query
{
    public sealed class AtribuicoesDoTecnicoViewModel
    {
        public AtribuicoesDoTecnicoViewModel(Guid id, string nome, IEnumerable<ChamadoDoTecnicoViewModel> chamados)
        {
            Id = id;
            Nome = nome;
            Chamados = chamados;
        }

        public Guid Id { get; }
        public string Nome { get; }
        public IEnumerable<ChamadoDoTecnicoViewModel> Chamados{ get; }

    }

    public sealed class ChamadoDoTecnicoViewModel
    {
        public ChamadoDoTecnicoViewModel(Guid id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public Guid Id { get; }
        public string Descricao { get; }
    }
}
