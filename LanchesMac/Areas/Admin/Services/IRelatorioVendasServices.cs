using LanchesMac.Models;

namespace LanchesMac.Areas.Admin.Services
{
    public interface IRelatorioVendasServices
    {
        Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
    }
}
