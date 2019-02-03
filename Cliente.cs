using System;
using System.Linq;

namespace LP2_Atividade2
{
    public class Cliente
    {
        public int Id{get;set;}
        public string Nome{get;set;}
        public string Cpf{get;set;}

        public int Idade{get;set;}

        public void atualizar(Conta conta, Cliente cliente,BancoContext context)
        {
            var clienteAtualizado = context.Set<Cliente>().First(p => p.Id == cliente.Id);
            var contaAtualizada = context.Set<Conta>().First(p => p.Id == conta.Id);
            int opt = 1;
            for(;opt!=0;)
            {
                Console.WriteLine("--------Atualizar--------");
                Console.WriteLine("Nome                   -1");
                Console.WriteLine("Idade                  -2");
                Console.WriteLine("Cpf                    -3");
                Console.WriteLine("Sair                   -0");
                try
                {
                    opt = Int32.Parse(Console.ReadLine());
                }catch(Exception e)
                {
                    e.ToString();
                    opt = 100;
                }
                switch(opt)
                {
                    case 1 :
                        Console.WriteLine("Digite o novo nome");
                        string nome = Console.ReadLine();
                        clienteAtualizado.Nome = nome;
                        context.SaveChanges(); 
                        contaAtualizada.Titular = nome;
                        context.SaveChanges(); 
                    break;

                    case 2 :
                        Console.WriteLine("Digite a nova idade");
                        int idade;
                        try
                        {
                            idade = Int32.Parse(Console.ReadLine());
                            clienteAtualizado.Idade = idade;
                            context.SaveChanges(); 
                        }catch(Exception e)
                        {
                            e.ToString();
                            Console.WriteLine("Idade Invalida");
                        }
                    break;

                    case 3 :
                        Console.WriteLine("Digite o novo CPF");
                        string cpf = Console.ReadLine();
                        var checkCpf = context.Clientes.Where(b => b.Cpf == cpf)
                                                       .FirstOrDefault();
                        
                        if(checkCpf != null)
                        {
                            Console.WriteLine("CPF ja cadastrado em outra conta");
                            Console.WriteLine(" ");
                            break;
                        }
                        clienteAtualizado.Cpf = cpf;
                        context.SaveChanges(); 
                            
                    break;

                    case 0 : 
                    break;

                    default :
                        Console.WriteLine("Opção Invalida");
                        Console.WriteLine(" ");
                    break;
                   
                }
            }
        }
    }
}