using System.Collections.Generic;

namespace LP2_Atividade2
{
    public class ContaCorrente
    {
        public int Id{get;set;}
        public decimal Taxa{get;set;}
        public virtual Conta Conta{get;set;}
    }
}