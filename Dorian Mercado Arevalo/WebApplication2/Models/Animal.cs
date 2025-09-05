namespace WebApplication2.Models
{
    public abstract class Animal
    {
        public string Nombre { get; set; }
        public abstract string HacerSonido();
    }
    public class Perro : Animal
    {
        public override string HacerSonido() => "Guau Guau 🐶";
    }

    public class Gato : Animal
    {
        public override string HacerSonido() => "Miau Miau 🐱";
    }

    public class Vaca : Animal
    {
        public override string HacerSonido() => "Muuu 🐄";
    }
    public class AnimalRequest
    {
        public string Nombre { get; set; } // "perro", "gato", "vaca"
    }

}
