using Microsoft.EntityFrameworkCore; 
namespace LP2_Atividade2 {
         public class BancoContext: DbContext     
         {         
             public DbSet<Agencia> Agencias {get; set;}  
             public DbSet<Banco> Bancos {get; set;}  
             public DbSet<Cliente> Clientes {get; set;}  
             public DbSet<Conta> Contas {get; set;}         
             public DbSet<ContaCorrente> ContasCorrente {get; set;}         
             public DbSet<ContaPoupanca> ContasPoupanca {get; set;}   
             public DbSet<Solicitacao> Solicitacoes {get; set;}          
             protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)         
             {             optionsBuilder.UseSqlite("Data Source=banco.db");         
             }     
            } 
} 