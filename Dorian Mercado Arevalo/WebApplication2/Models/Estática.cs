namespace WebApplication2.Models
{
    public static class ItemStore
    {
        public static List<string> Items { get; set; } = new List<string>();
    }
    public class ItemRequest
    {
        public string Nombre { get; set; }
    }

}
