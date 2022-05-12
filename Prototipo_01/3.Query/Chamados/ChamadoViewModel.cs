using System;

namespace Chamados._3.Query.Chamados
{
    public sealed class ChamadoViewModel
    {
        public ChamadoViewModel(string id, string descricao, DateTime dataCriacao, string status, string detalhes, TecnicoAtribuidoViewModel tecnicoAtribuido = null)
        {
            Id = id;
            Descricao = descricao;
            DataCriacao = dataCriacao;
            Status = status;
            TecnicoAtribuido = tecnicoAtribuido;
            Detalhes = detalhes;
        }

        public string Id { get; }
        public string Descricao { get; }
        public DateTime DataCriacao { get; }
        public string Status { get; }
        public string Detalhes { get; }
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
