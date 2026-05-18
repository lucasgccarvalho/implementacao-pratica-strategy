using System;
using System.Collections.Generic;

namespace ImplementacaoPraticaStrategy
{
    interface IDesconto
    {
        decimal Calcular(decimal total);
        string Nome { get; }
    }
    class SemDesconto : IDesconto
    {
        public decimal Calcular(decimal total) => 0;
        public string Nome => "Sem desconto";
    }

    class DescontoPremium : IDesconto
    {
        public decimal Calcular(decimal total) => total * 0.10m;
        public string Nome => "Premium (10%)";
    }

    class DescontoVIP : IDesconto
    {
        public decimal Calcular(decimal total) => total * 0.25m;
        public string Nome => "VIP (25%)";
    }

    class DescontoPlus : IDesconto
    {
        public decimal Calcular(decimal total) => total * 0.35m;
        public string Nome => "Plus (35%)";
    }

    class DescontoMax : IDesconto
    {
        public decimal Calcular(decimal total) => total * 0.50m;
        public string Nome => "Max (50%)";
    }

    class Pedido
    {
        private IDesconto _desconto;
        private List<(string nome, decimal preco)> _itens = new();
        public Pedido(IDesconto desconto) => _desconto = desconto;
        public void TrocarDesconto(IDesconto desconto) => _desconto = desconto;
        public void AdicionarItem(string nome, decimal preco) => _itens.Add((nome, preco));
        public void Finalizar()
        {
            decimal total = 0;
            foreach (var item in _itens) total += item.preco;

            decimal desconto = _desconto.Calcular(total);

            Console.WriteLine($"\nDesconto: {_desconto.Nome}");
            Console.WriteLine($"Total bruto: R$ {total:F2}");
            Console.WriteLine($"Desconto: R$ {desconto:F2}");
            Console.WriteLine($"Total final: R$ {total - desconto:F2}");
            Console.WriteLine(new string('-', 30));
        }
    }

    class Program
    {
        static void Main()
        {
            var pedido = new Pedido(new SemDesconto());

            while (true)
            {
                Console.Write("Nome do item: ");
                string nome = Console.ReadLine();

                Console.Write("Preço do item: R$ ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal preco))
                {
                    Console.WriteLine("Preço inválido, tente novamente.\n");
                    continue;
                }

                pedido.AdicionarItem(nome, preco);
                Console.WriteLine($"  \"{nome}\" adicionado!\n");

                Console.Write("Adicionar outro item? (s/n): ");
                string resposta = Console.ReadLine();

                if (resposta?.ToLower() != "s")
                    break;

                Console.WriteLine();
            }

            Console.WriteLine(new string('-', 35));

            pedido.TrocarDesconto(new SemDesconto());
            pedido.Finalizar();

            pedido.TrocarDesconto(new DescontoPremium());
            pedido.Finalizar();

            pedido.TrocarDesconto(new DescontoVIP());
            pedido.Finalizar();
        }
    }
}