namespace LP2_Atividade2
{
    public class Solicitacao
    {
        public int Id{get;set;}
        public virtual Conta Conta{get;set;}
        public string Movimentacao{get;set;}
    }
}