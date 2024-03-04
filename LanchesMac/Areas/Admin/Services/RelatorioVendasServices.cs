using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
    public class RelatorioVendasServices : IRelatorioVendasServices
    {
        private readonly AppDbContext _context;

        public RelatorioVendasServices(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            //IQuerable data to fitire expansion of the query if necessary
            var result = from obj in _context.Pedidos select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.PedidoEnviado >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            return await result
                        .Include(x => x.PedidoItens)
                        .ThenInclude(x => x.Lanche)
                        .OrderByDescending(x => x.PedidoEnviado)
                        .ToListAsync();
        }
    }
}
