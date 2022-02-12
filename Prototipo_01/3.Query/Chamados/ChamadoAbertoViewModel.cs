using System;

namespace Chamados._3.Query.Chamados
{
    public sealed class ChamadoAbertoViewModel
    {
        public ChamadoAbertoViewModel(Guid id, string descricao, DateTime dataCriacao)
        {
            Id = id;
            Descricao = descricao;
            DataCriacao = dataCriacao;
        }

        public Guid Id { get; }
        public string Descricao { get; }
        public DateTime DataCriacao { get; }
    }
}
