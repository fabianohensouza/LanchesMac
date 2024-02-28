using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.ViewModel
{
    public class PedidoLancheViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }
    }
}
