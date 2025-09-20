using WebApi_csharp.Models;
using WebApi_csharp.Repository;

namespace WebApi_csharp.Services
{
    public class ProductoService: IProductoService
    {
       
       private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Producto> GetAll()
        {
            return _repository.GetAll();
        }

        public Producto? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public void Create(Producto producto)
        {
            // acá podrías validar datos antes de guardar
            if (string.IsNullOrWhiteSpace(producto.Name))
                throw new ArgumentException("El nombre del producto es obligatorio");

            _repository.Add(producto);
        }

        public bool Update(Producto producto)
        {
            return _repository.Update(producto);
        }

        public bool Delete(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
