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

            Console.WriteLine($"\nEstratégia : {_desconto.Nome}");
            Console.WriteLine($"Total bruto: R$ {total:F2}");
            Console.WriteLine($"Desconto   : R$ {desconto:F2}");
            Console.WriteLine($"Total final: R$ {total - desconto:F2}");
            Console.WriteLine(new string('-', 30));
        }
    }

    class Program
    {
        static void Main()
        {
            var pedido = new Pedido(new SemDesconto());
            pedido.AdicionarItem("Notebook",3500.00m);
            pedido.AdicionarItem("Mouse",89.90m);
            pedido.Finalizar();

            pedido.TrocarDesconto(new DescontoPremium());
            pedido.Finalizar();

            pedido.TrocarDesconto(new DescontoVIP());
            pedido.Finalizar();
        }
    }
}