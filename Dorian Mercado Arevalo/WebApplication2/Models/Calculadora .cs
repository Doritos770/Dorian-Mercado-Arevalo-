namespace WebApplication2.Models
{
    public class Calculadora : ICalculadora
    {
        public int Sumar(int a, int b) => a + b;
        public int Restar(int a, int b) => a - b;
    }

}
