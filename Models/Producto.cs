namespace WebApi_csharp.Models
{
    public class Producto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Sale { get; set; } = string.Empty;

    }
}
