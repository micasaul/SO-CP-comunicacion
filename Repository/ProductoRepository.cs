using WebApi_csharp.Models;

namespace WebApi_csharp.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private List<Producto> listaProductos  = new List<Producto>
        {
            new Producto { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Freezing", Sale = "10%" },
            new Producto { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Bracing", Sale = "15%" },
            new Producto { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Chilly", Sale = "20%" },
            new Producto { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Cool", Sale = "25%" },
            new Producto { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Mild", Sale = "30%" },
            new Producto { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Name = "Warm", Sale = "35%" },
            new Producto { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Name = "Balmy", Sale = "40%" },
            new Producto { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Name = "Hot", Sale = "45%" },
            new Producto { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Name = "Sweltering", Sale = "50%" },
            new Producto { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Scorching", Sale = "60%" },
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
