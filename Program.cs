using System;
using System.Linq;

namespace LP2_Atividade2
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BancoContext();
            InitOperator(context);
            int menu = 0;
            int opt = 1;
            for(; opt!=0;)
            {
                
                if(menu == 0) menu = MenuPrimario(opt);
                opt = menu;
                if(menu == 1) menu = MenuContaSelec(opt,context);
                else if(menu == 2) menu = CriarConta(context);
                if(opt == 100)menu = 0;
                
            }
            Console.WriteLine(" ");
            Console.WriteLine("Volte sempre!");
        }

        public static void InitOperator(BancoContext context)
        {  
                if(!context.Bancos.Any())
                {
                    // Console.WriteLine("aqui");
                    var newBanco = new Banco() { Nome = "Banco do Brasil" };     
                    context.Add(newBanco);     
                    context.SaveChanges();

                    if(!context.Agencias.Any())
                    {
                        var newAgencia = new Agencia() { Numero = "0121" , Banco = newBanco};     
                        context.Add(newAgencia);     
                        context.SaveChanges();
                    }
                    

                }
            
        }

        public static int MenuPrimario(int opt)
        {
            Console.WriteLine(" ");
            Console.WriteLine("------------Menu------------");
            Console.WriteLine("Acessar Conta            - 1");
            Console.WriteLine("Criar Conta              - 2");
            Console.WriteLine("Sair                     - 0");
            Console.WriteLine(" ");
            try
            {
                opt = Int32.Parse(Console.ReadLine());
            }catch(Exception e)
            {
                e.ToString();
                Console.WriteLine("Opção Invalida");
                Console.WriteLine(" ");
                opt = 100;
            }
            return opt;
        }

        static int MenuContaSelec(int opt, BancoContext context)
        {
            Conta conta = new Conta();
            bool access = false;
            for(;opt!=0;)
            {
                Console.WriteLine(" ");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Conta Corrente           - 1");
                Console.WriteLine("Conta Poupanca           - 2");
                Console.WriteLine("Sair                     - 0");
                Console.WriteLine(" ");
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
                        Console.WriteLine(" ");
                        conta = VerificarContaCorrente(opt,context);
                        if(conta==null) {
                            access=false;
                        }
                        else if(conta!=null){
                            access=true;
                        }
                        if(access==true)MenuContaCorrente(opt,context,conta);
                    break;

                    case 2 :
                        Console.WriteLine(" ");
                        conta = VerificarContaPoupanca(opt,context);
                        if(conta==null) {
                            access=false;
                        }
                        else if(conta!=null){
                            access=true;
                        }
                        if(access==true)MenuContaPoupanca(opt,context,conta);
                    break;

                    case 0 :break;

                    default : 
                        Console.WriteLine("Opção Invalida");
                        Console.WriteLine(" ");
                    break;
                }
            }

            return opt;
        }

        static void MenuContaCorrente(int opt, BancoContext context, Conta conta)
        {
            Console.WriteLine(" ");
            Console.WriteLine("Bem Vindo " + conta.Titular);
            for(;opt!=0;)
            {
                Console.WriteLine(" ");
                Console.WriteLine("-------Conta Corrente-------");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Sacar                 - 1");
                Console.WriteLine("Depositar             - 2");
                Console.WriteLine("Olhar Saldo           - 3");
                Console.WriteLine("Atualizar dados       - 4");
                Console.WriteLine("Excluir esta conta    - 5");
                Console.WriteLine("Voltar                - 6");
                Console.WriteLine(" ");
                try
                {
                    opt = Int32.Parse(Console.ReadLine());
                }catch(Exception e)
                {
                    e.ToString();
                    opt = 100;
                }
                Console.WriteLine(" ");
                switch(opt)
                {
                    case 1: 
                        sacarCorrente(conta,context);
                    break;

                    case 2: 
                        depositarCorrente(conta,context);
                    break;

                    case 3: 
                        OlharSaldo(conta);
                    break;

                    case 4: 
                        AtualizarDados(conta,context,1);
                    break;

                    case 5: 
                        opt = deletarConta(context,conta,1);
                    break;

                    case 6 : 
                        opt = 0;
                    break;
                    default :
                        Console.WriteLine("Opção Invalida");
                        Console.WriteLine(" ");
                        opt = 1;
                    break;
                }
            }
        }

        static void MenuContaPoupanca(int opt, BancoContext context, Conta conta)
        {   
            Console.WriteLine(" ");
            Console.WriteLine("Bem Vindo " + conta.Titular);
            for(;opt!=0;)
            {
                Console.WriteLine(" ");
                Console.WriteLine("-------Conta Poupanca-------");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Sacar                 - 1");
                Console.WriteLine("Depositar             - 2");
                Console.WriteLine("Olhar Saldo           - 3");
                Console.WriteLine("Atualizar dados       - 4");
                Console.WriteLine("Excluir esta conta    - 5");
                Console.WriteLine("Voltar                - 6");
                Console.WriteLine(" ");
                try
                {
                    opt = Int32.Parse(Console.ReadLine());
                }catch(Exception e)
                {
                    e.ToString();
                    opt = 100;
                }
                Console.WriteLine(" ");
                switch(opt)
                {
                    case 1: 
                        sacarPoupanca(conta,context);
                    break;

                    case 2: 
                        depositarPoupanca(conta,context);
                    break;

                    case 3: 
                        OlharSaldo(conta);
                    break;

                    case 4: 
                        AtualizarDados(conta,context,2);
                    break;

                    case 5: 
                        opt = deletarConta(context,conta,2);
                    break;

                    case 6 :
                        opt = 0;
                    break;
                    default :
                        Console.WriteLine("Opção Invalida");
                        Console.WriteLine(" ");
                        opt = 1;
                    break;
                }
            }
        }
        public static int CriarConta(BancoContext context)
        {
                string cpf;
                int idade;
                string agencia;
                Agencia agenciaCliente = new Agencia();
                string nome;
                    Console.WriteLine("Digite seu cpf");
                    cpf = Console.ReadLine();
                    try
                    {
                        var clienteCadastrado = context.Clientes.Where(b => b.Cpf == cpf)
                                                       .FirstOrDefault();
                        if(clienteCadastrado!=null)
                        {
                            Console.WriteLine("Este cpf já esta cadastrado");
                            return 0;
                        }
                    }catch(Exception e)
                    {
                        Console.WriteLine("Estamos passando pro problemas tecnicos!");
                        Console.WriteLine("Tente novamente dentro de alguns minutos");
                        Console.WriteLine(" ");
                        return 0;

                    }
                    Console.WriteLine("Digite seu Nome");
                    nome = Console.ReadLine();
                    Console.WriteLine("Digite sua idade");
                    try
                    {
                        idade =Int32.Parse(Console.ReadLine());
                    }catch(Exception e)
                    {
                        e.ToString();
                        Console.WriteLine("Idade Invalida");
                        return 0;
                    }
                    Console.WriteLine("Selecione uma de nossas Agencias:");
                    var bancos = context.Set<Banco>(); 
                    foreach (var b in bancos) 
                    {     
                        if(b.Nome == "Banco do Brasil")
                            Console.WriteLine("--------" + b.Nome + "--------");
                    } 
                    var agencias = context.Set<Agencia>(); 
                    foreach (var a in agencias) 
                    {     
                        Console.WriteLine(a.Numero);
                    } 
                    Console.WriteLine(" ");
                    bool error = true;
                    for(;error!=false;)
                    {
                        Console.WriteLine("Digite a agencia que deseja");
                        agencia = Console.ReadLine();
                        try{
                            var agenciaSelecionada = context.Agencias.Where(b => b.Numero == agencia)
                                                                     .FirstOrDefault();
                            agenciaCliente = agenciaSelecionada;
                            error = false;
                        }catch(Exception e)
                        {
                            e.ToString();
                            Console.WriteLine("Agencia Não Encontrada");
                            Console.WriteLine(" ");
                            error = true;
                        }
                    }
                    int contaType = 0;
                    for(;contaType!=1 && contaType!=2;)
                    {
                        Console.WriteLine("Escolha um tipo de conta:");
                        Console.WriteLine(" ");
                        Console.WriteLine("Conta Corrente          1");
                        Console.WriteLine("Conta Poupanca          2");
                        try
                        {
                            contaType = Int32.Parse(Console.ReadLine());
                        }catch(Exception e)
                        {
                            e.ToString();
                            contaType = 100;
                        }
                        switch(contaType)
                        {
                            case 1 :
                                CriarNovaContaCorrente(cpf,nome,idade,agenciaCliente,context);
                            break;

                            case 2 :
                                CriarNovaContaPoupanca(cpf,nome,idade,agenciaCliente,context);
                            break;

                            default :
                                Console.WriteLine("Tipo de conta Invalido");
                                Console.WriteLine(" ");
                            break;
                        }
                    }
            return 0;
        }

        public static void CriarNovaContaCorrente(string cpf,string nome, int idade,Agencia agencia,BancoContext context)
        {
                try
                {
                    var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };     
                    context.Add(newCliente);     
                    context.SaveChanges();
                    decimal saldo = 0;
                    var newConta = new Conta() {Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome};
                    context.Add(newConta);     
                    context.SaveChanges();
                    var newContaCorrente = new ContaCorrente() { Conta = newConta, Taxa = 0.10M};
                    context.Add(newContaCorrente);     
                    context.SaveChanges();
                    Console.WriteLine("Operação Realizada com sucesso!");
                    Console.WriteLine(" ");
                }catch(Exception e)
                {
                    e.ToString();
                    Console.WriteLine(e);
                    Console.WriteLine("Não foi possivel realizar esta ação!");
                    Console.WriteLine(" ");
                }
            
        }

        public static void CriarNovaContaPoupanca(string cpf,string nome, int idade,Agencia agencia,BancoContext context)
        {
                try
                {
                    var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };     
                    context.Add(newCliente);     
                    context.SaveChanges();
                    decimal saldo = 0;
                    var newConta = new Conta() {Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome};
                    context.Add(newConta);     
                    context.SaveChanges();
                    decimal taxaJuros = 0;
                    var newContaPoupanca = new ContaPoupanca() { Conta = newConta, TaxaJuros = taxaJuros};
                    context.Add(newContaPoupanca);     
                    context.SaveChanges();
                    Console.WriteLine("Operação Realizada com sucesso!");
                    Console.WriteLine(" ");
                }catch(Exception e)
                {
                    e.ToString();
                    Console.WriteLine("Não foi possivel realizar esta ação!");
                    Console.WriteLine(" ");
                }
            
        }

        public static Conta VerificarContaCorrente(int opt, BancoContext context)
        {   
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("Digite o nome do Titular da conta");
            nome = Console.ReadLine();
            Console.WriteLine("Digite o Cpf da conta");
            cpf = Console.ReadLine();
                try{
                    var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente)
                                              .FirstOrDefault();
                    var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    if(contaCorrente == null)
                    {
                        Console.WriteLine("Conta não encontrada");
                        Console.WriteLine(" ");
                        return null;
                    }
                    }catch(Exception e)
                    {   
                        e.ToString();
                        Console.WriteLine("Conta não encontrada");
                        Console.WriteLine(" ");
                        return null;
                    }
            return conta;
        }

        public static Conta VerificarContaPoupanca(int opt, BancoContext context)
        {   
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("Digite o nome do Titular da conta");
            nome = Console.ReadLine();
            Console.WriteLine("Digite o Cpf da conta");
            cpf = Console.ReadLine();
                try{
                    var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente)
                                              .FirstOrDefault();
                    var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta)
                                              .FirstOrDefault();

                    if(contaPoupanca == null)
                    {
                        Console.WriteLine("Conta não encontrada");
                        Console.WriteLine(" ");
                        return null;
                    }
                    }catch(Exception e)
                    {   
                        e.ToString();
                        Console.WriteLine("Conta não encontrada");
                        Console.WriteLine(" ");
                        return null;
                    }
            return conta;
        }

        static void sacarCorrente(Conta conta, BancoContext context)
        {
                Console.WriteLine("Digite a quantidade a ser Sacada");
                decimal saque;
                try
                {
                    saque = Decimal.Parse(Console.ReadLine());
                    conta.Sacar(saque,conta,context,1);
                }catch(Exception e)
                {
                    e.ToString();
                    Console.WriteLine("Valor Invalido");
                }
        }

        static void depositarCorrente(Conta conta, BancoContext context)
        {
            Console.WriteLine("Digite a quantidade a ser Depositada");
            decimal deposito;
            try
            {
                deposito = Decimal.Parse(Console.ReadLine());
                conta.Depositar(deposito,conta,context,1);
            }catch(Exception e)
            {
                e.ToString();
                Console.WriteLine("Valor Invalido");
            }
        }

        static void sacarPoupanca(Conta conta, BancoContext context)
        {
                Console.WriteLine("Digite a quantidade a ser Sacada");
                decimal saque;
                try
                {
                    saque= Decimal.Parse(Console.ReadLine());
                    conta.Sacar(saque,conta,context,2);
                }catch(Exception e)
                {
                    e.ToString();
                    Console.WriteLine("Valor Invalido");
                }
        }

        static void depositarPoupanca(Conta conta, BancoContext context)
        {
            Console.WriteLine("Digite a quantidade a ser Depositada");
            decimal deposito;
            try
            {
                deposito = Decimal.Parse(Console.ReadLine());
                conta.Depositar(deposito,conta,context,2);
            }catch(Exception e)
            {
                    e.ToString();
                    Console.WriteLine("Valor Invalido");
            }
        }

        static void OlharSaldo(Conta conta)
        {
            Console.WriteLine("Saldo disponivel: ");
            Console.WriteLine(conta.Saldo);
        }

        static void AtualizarDados(Conta conta, BancoContext context, int opt)
        {
            Console.WriteLine("digite novamente seu nome e cpf");
            Console.WriteLine("Digite o nome do Titular da conta");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o Cpf da conta");
            string cpf = Console.ReadLine();
                if(opt==1)
                {
                    try{
                    var clienteC = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    clienteC.atualizar(conta,clienteC,context);
                    }catch(Exception e)
                    {   
                        e.ToString();
                        Console.WriteLine("Credenciais incorretas");
                        Console.WriteLine(" ");
                    }
                } else if(opt==2)
                {
                    try{
                    var clienteP = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    clienteP.atualizar(conta,clienteP,context);
                    }catch(Exception e)
                    {   
                        e.ToString();
                        Console.WriteLine("Credenciais incorretas");
                        Console.WriteLine(" ");
                    }
                }
        }

        static int deletarConta(BancoContext context,Conta conta, int opt)
        {
                Cliente cliente = new Cliente();
                ContaCorrente contaC = new ContaCorrente();
                ContaPoupanca contaP = new ContaPoupanca();
                int option = 0;
                Console.WriteLine("digite novamente seu nome e cpf");
                Console.WriteLine("Digite o nome do Titular da conta");
                string nome = Console.ReadLine();
                Console.WriteLine("Digite o Cpf da conta");
                string cpf = Console.ReadLine();
                try{
                    if(opt == 1)
                    {
                        cliente = context.Set<Cliente>().Where(b => b.Cpf == cpf && b.Nome == nome)
                                                        .FirstOrDefault();
                        contaC = context.Set<ContaCorrente>().Where(b => b.Conta == conta)
                                                             .FirstOrDefault();
                        
                                                                
                    }
                    else if(opt == 2)
                    {
                        cliente = context.Set<Cliente>().Where(b => b.Cpf == cpf && b.Nome == nome)
                                                  .FirstOrDefault();
                        contaP = context.Set<ContaPoupanca>().Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    }
                    for(;option!=2;)
                    {
                        Console.WriteLine("Deseja realmente deletar esta conta?");
                        Console.WriteLine("Sim -                              1");
                        Console.WriteLine("Não -                              2");
                        try
                        {
                            option =Int32.Parse(Console.ReadLine());
                        }catch(Exception e)
                        {
                            e.ToString();
                            option = 100;
                        }
                        switch(option)
                        {
                            case 1 :
                                if(opt == 1) 
                                {
                                    var solicitacao = context.Set<Solicitacao>();
                                    foreach(var s in solicitacao)
                                    {
                                        if(s.Conta == conta)
                                        {
                                            context.Remove(s);
                                        }
                                    }
                                    context.Remove(contaC);
                                    context.Remove(conta);
                                    context.Remove(cliente);
                                    context.SaveChanges(); 
                                }
                                else if(opt == 2)
                                {
                                    var solicitacao = context.Set<Solicitacao>();
                                    foreach(var s in solicitacao)
                                    {
                                        if(s.Conta == conta)
                                        {
                                            context.Remove(s);
                                        }
                                    }
                                    context.Remove(contaP);
                                    context.Remove(conta);
                                    context.Remove(cliente);
                                    context.SaveChanges(); 
                                }
                                Console.WriteLine("Operação Realizada com sucesso!");
                                Console.WriteLine(" ");

                            break;

                            case 2 :
                            break;

                            default :
                                Console.WriteLine("Opção Invalida");
                                Console.WriteLine(" ");
                            break;
                        }
                        if(option == 1) return 0;
                    }
                    
                    }catch(Exception e)
                    {   
                        e.ToString();
                        Console.WriteLine("Credenciais incorretas");
                        Console.WriteLine(" ");
                    }
                
            return 5;
        } 
    }
}
