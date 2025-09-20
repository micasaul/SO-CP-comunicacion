using WebApi_csharp.Models;

namespace WebApi_csharp.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private List<Producto> listaProductos  = new List<Producto>
        {
            new Producto { Id = Guid.NewGuid(), Name = "Freezing", Sale = "10%" },
            new Producto { Id = Guid.NewGuid(), Name = "Bracing", Sale = "15%" },
            new Producto { Id = Guid.NewGuid(), Name = "Chilly", Sale = "20%" },
            new Producto { Id = Guid.NewGuid(), Name = "Cool", Sale = "25%" },
            new Producto { Id = Guid.NewGuid(), Name = "Mild", Sale = "30%" },
            new Producto { Id = Guid.NewGuid(), Name = "Warm", Sale = "35%" },
            new Producto { Id = Guid.NewGuid(), Name = "Balmy", Sale = "40%" },
            new Producto { Id = Guid.NewGuid(), Name = "Hot", Sale = "45%" },
            new Producto { Id = Guid.NewGuid(), Name = "Sweltering", Sale = "50%" },
            new Producto { Id = Guid.NewGuid(), Name = "Scorching", Sale = "60%" }
        };

        // Obtener todos
        public IEnumerable<Producto> GetAll()
        {
            return listaProductos;
        }

        // 🔹 Obtener por Id
        public Producto? GetById(Guid id)
        {
            return listaProductos.FirstOrDefault(p => p.Id == id);
        }

        // 🔹 Agregar
        public void Add(Producto producto)
        {
            producto.Id = Guid.NewGuid(); // asigna un nuevo Id
            listaProductos.Add(producto);
        }

        // 🔹 Actualizar
        public bool Update(Producto producto)
        {
            var existing = GetById(producto.Id);
            if (existing == null)
                return false;

            existing.Name = producto.Name;
            existing.Sale = producto.Sale;
            return true;
        }

        // 🔹 Eliminar
        public bool Delete(Guid id)
        {
            var producto = GetById(id);
            if (producto == null)
                return false;

            return listaProductos.Remove(producto);
        }

    }


}
