namespace WebApplication2.Models
{
    public class EmpleadoBase : Empleado
    {
        public string Puesto { get; set; }

        public override string GetDescripcion()
        {
            return $"{Nombre} (Empleado) - Puesto: {Puesto}";
        }
    }
}
