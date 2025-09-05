namespace WebApplication2.Models
{
    public class Gerente : Empleado
    {
        public int NumeroSubordinados { get; set; }

        public override string GetDescripcion()
        {
            return $"{Nombre} (Gerente) - Subordinados: {NumeroSubordinados}";
        }
    }
}
