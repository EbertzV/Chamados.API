using Prototipo_01.Crosscutting;
using System;

namespace Prototipo_01.Dominio.Chamados
{
    public sealed class Chamado
    {
        public Chamado(Guid id, EStatusChamado status, string descricao)
        {
            Id = id;
            Status = status;
            Descricao = descricao;
        }

        public Guid Id { get; }
        public string Descricao { get; }
        public EStatusChamado Status { get; private set; }

        public override int GetHashCode()
            => Id.GetHashCode() ^
                Status.GetHashCode() ^
                Descricao.GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj is Chamado chamado && chamado != null)
                return chamado.Id == Id;
            return false;
        }

        public Chamado Novo(string descricao)
            => new Chamado(Guid.NewGuid(), EStatusChamado.Aberto, descricao);

        public Resultado<Chamado> Encerrar()
        {
            if (Status == EStatusChamado.Arquivado)
                return Resultado<Chamado>.NovaFalha(Falha.NovaFalha("Não é possível encerrar um chamado arquivado."));
            else if (Status == EStatusChamado.Encerrado)
                return Resultado<Chamado>.NovaFalha(Falha.NovaFalha("O chamado já está encerrado."));
            else
            {
                Status = EStatusChamado.Encerrado;
                return Resultado<Chamado>.NovoSucesso(this);
            }
        }

        public Resultado<Chamado> Arquivar()
        {
            if (Status == EStatusChamado.Arquivado)
                return Resultado<Chamado>.NovaFalha(Falha.NovaFalha("O chamado já está arquivado."));
            else if (Status == EStatusChamado.Encerrado)
                return Resultado<Chamado>.NovaFalha(Falha.NovaFalha("Não é possível encerrar um chamado que já esteja arquivado."));
            else
            {
                Status = EStatusChamado.Arquivado;
                return Resultado<Chamado>.NovoSucesso(this);
            }
        }

        public Resultado<Chamado> Reabrir()
        {
            if (Status == EStatusChamado.Aberto)
                return Resultado<Chamado>.NovaFalha(Falha.NovaFalha("O chamado já está aberto."));
            else
            {
                Status = EStatusChamado.Aberto;
                return Resultado<Chamado>.NovoSucesso(this);
            }
        }
    }

    public enum EStatusChamado
    {
        Aberto,
        Arquivado, 
        Encerrado
    }
}
