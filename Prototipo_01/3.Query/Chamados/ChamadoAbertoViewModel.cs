using System;

namespace Chamados._3.Query.Chamados
{
    public sealed class ChamadoAbertoViewModel
    {
        public ChamadoAbertoViewModel(int id, string descricao, DateTime dataCriacao)
        {
            Id = id;
            Descricao = descricao;
            DataCriacao = dataCriacao;
        }

        public int Id { get; }
        public string Descricao { get; }
        public DateTime DataCriacao { get; }
    }
}
