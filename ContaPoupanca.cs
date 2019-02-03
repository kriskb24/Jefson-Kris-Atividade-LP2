namespace LP2_Atividade2
{
    public class ContaPoupanca
    {
        public int Id{get;set;}
        public decimal TaxaJuros{get;set;}
    
        public virtual Conta Conta{get;set;}
    }
}