using System;

namespace Chamados._3.Query.Chamados
{
    public sealed class ChamadoViewModel
    {
        public ChamadoViewModel(Guid id, string descricao, DateTime dataCriacao, string status, TecnicoAtribuidoViewModel tecnicoAtribuido = null)
        {
            Id = id;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Status = status;
            TecnicoAtribuido = tecnicoAtribuido;
        }

        public Guid Id { get; }
        public string Descricao { get; }
        public DateTime DataCriacao { get; }
        public string Status { get; }
        public TecnicoAtribuidoViewModel TecnicoAtribuido { get; }
    }

    public sealed class TecnicoAtribuidoViewModel
    {
        public TecnicoAtribuidoViewModel(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Guid Id { get; }
        public string Nome { get; }
    }
}
