using WebApi_csharp.Models;

namespace WebApi_csharp.Services
{
    public interface IProductoService
    {
        IEnumerable<Producto> GetAll();
        Producto? GetById(Guid id);
        void Create(Producto producto);
        bool Update(Producto producto);
        bool Delete(Guid id);
    }
}
