namespace WebApplication2.Models
{
    
        public abstract class Empleado
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public abstract string GetDescripcion();
       }    
}
