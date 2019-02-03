using System.Collections.Generic;

namespace LP2_Atividade2
{
    public class Banco
    {
        public int Id{get;set;}
        public Banco()
        {
            Agencias = new List<Agencia>();
        }
        public string Nome{get;set;}
    
        public virtual ICollection<Agencia> Agencias{get;set;}
    }
}