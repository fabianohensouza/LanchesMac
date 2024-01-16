using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    public class PedidoDetalhe
    {
        public int PedidoDetalheId { get; set; }
        public int Quantidade { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }


        // Creating the relationship One To Many with the Pedido and Lanche Classes
        public virtual Lanche Lanche { get; set; }
        public int LancheId { get; set; }
        public virtual Pedido Pedido { get; set; }
        public int PedidoId { get; set; }
    }
}