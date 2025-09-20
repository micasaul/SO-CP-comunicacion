using WebApi_csharp.Models;

namespace WebApi_csharp.Repository
{
    public interface IProductoRepository
    {
        IEnumerable<Producto> GetAll();
        Producto? GetById(Guid id);
        void Add(Producto producto);
        bool Update(Producto producto);
        bool Delete(Guid id);
    }
}
