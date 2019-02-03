using System.Collections.Generic;

namespace LP2_Atividade2
{
    public class Agencia
    {
        public int Id{get;set;}
        public Agencia()
        {
            Contas = new List<Conta>();
        }
        public string Numero{get;set;}
        public virtual Banco Banco{get;set;}
    
        public virtual ICollection<Conta> Contas{get;set;}
    }
}