using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System.Text.Json.Nodes;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/Practice")]
    [ApiController]
    //1
    public class Practice : ControllerBase
    {
        [HttpPost("FiltrarPares")]
        public ActionResult<List<int>> FiltrarPares([FromBody] List<int> numeros)
        {
            try
            {
                var pares = numeros.Where(n => n % 2 == 0).ToList();
                var impares = numeros.Where(n => n % 2 != 0).ToList();
                return Ok(pares);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        //----------------------------------
        //2
        private static Dictionary<string, string> diccionario = new Dictionary<string, string>
            {
                { "hello", "hola" },
                { "world", "mundo" },
                { "computer", "computadora" },
                { "programming", "programación" }
            };


        [HttpGet("Traductor/{palabra}")]
        public IActionResult Traductor(string palabra)
        {

            try
            {
                if (diccionario.TryGetValue(palabra, out string? traduccion))
                {
                    return Ok(traduccion);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        //3
        [HttpPost("ContarPalabras")]
        public ActionResult<int> Create([FromBody] string Oracion)
        {

            try
            {
                int NumeroPal = Oracion.Count(char.IsLetter);
                return Ok(NumeroPal);
            }
            catch (Exception error)
            {

                return BadRequest(error.Message);
            }

        }
        //4
        [HttpPost("stack")]
        public ActionResult<List<int>> StackOperaciones([FromBody] List<string> operaciones)
        {
            try
            {
                var pila = new Stack<int>();

                foreach (var operacion in operaciones)
                {
                    // Si la operación comienza con "push"
                    if (operacion.StartsWith("push", StringComparison.OrdinalIgnoreCase))
                    {
                        // ejemplo: "push 10"
                        var partes = operacion.Split(' ');
                        if (partes.Length == 2 && int.TryParse(partes[1], out int valor))
                        {
                            pila.Push(valor);
                        }
                        else
                        {
                            return BadRequest($"Operación inválida: {operacion}");
                        }
                    }
                    else if (operacion.Equals("pop", StringComparison.OrdinalIgnoreCase))
                    {
                        if (pila.Count > 0)
                            pila.Pop();
                        else
                            return BadRequest("No se puede hacer pop en una pila vacía.");
                    }
                    else
                    {
                        return BadRequest($"Operación desconocida: {operacion}");
                    }
                }

                return Ok(pila.ToList());
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        //5
        [HttpGet("Productos")]
        public ActionResult<List<Producto>> Productos()
        {
            List<Producto> productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Laptop", Precio = 1500.00m },
                new Producto { Id = 2, Nombre = "Smartphone", Precio = 800.00m },
                new Producto { Id = 3, Nombre = "Tablet", Precio = 400.00m },
                new Producto { Id = 4, Nombre = "Monitor", Precio = 300.00m },
                new Producto { Id = 5, Nombre = "Teclado", Precio = 100.00m }
            };

            return Ok(productos);
        }

        //6
        [HttpGet("empleados")]
        public ActionResult<IEnumerable<string>> GetEmpleados()
        {
            var empleados = new List<Empleado>
    {
        new EmpleadoBase { Id = 1, Nombre = "Ana", Puesto = "Contadora" },
        new EmpleadoBase { Id = 2, Nombre = "Luis", Puesto = "Desarrollador" },
        new Gerente { Id = 3, Nombre = "María", NumeroSubordinados = 5 }
    };

            // Usamos polimorfismo para obtener la descripción de cada uno
            var descripciones = empleados.Select(e => e.GetDescripcion()).ToList();

            return Ok(descripciones);
        }
        //7
        [HttpPost("calcular")]//{ "Operacion": "sumar", "A": 8, "B": 5 }
        public ActionResult<int> Calcular([FromBody] OperacionRequest request)
        {
            ICalculadora calculadora = new Calculadora();

            return request.Operacion.ToLower() switch
            {
                "sumar" => Ok(calculadora.Sumar(request.A, request.B)),
                "restar" => Ok(calculadora.Restar(request.A, request.B)),
                _ => BadRequest("Operación inválida. Usa 'sumar' o 'restar'.")
            };
        }
        //8
        [HttpPost("animal")]//{ "Nombre": "gato" }

        public ActionResult<string> CrearAnimal([FromBody] AnimalRequest request)
        {
            string nombre = request.Nombre;
            Animal? animal = nombre?.ToLower(System.Globalization.CultureInfo.CurrentCulture) switch
            {
                "perro" => new Perro { Nombre = "Perro" },
                "gato" => new Gato { Nombre = "Gato" },
                "vaca" => new Vaca { Nombre = "Vaca" },
                _ => null
            };

            if (animal == null)
                return BadRequest("Animal no reconocido. Usa: perro, gato o vaca.");

            return Ok(animal.HacerSonido());
        }
        //9
        [HttpPost("depositar")]/*
                                "Titular": "Ana",
                                "SaldoInicial": 100,
                                "Monto": 50}
                                */
        public ActionResult<CuentaBancaria> Depositar([FromBody] DepositoRequest request)
        {
            try
            {
                var cuenta = new CuentaBancaria(request.Titular, request.SaldoInicial);
                cuenta.Depositar(request.Monto);

                return Ok(cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //10
        
        [HttpPost("agregar-item")]
        public ActionResult<List<string>> AgregarItem([FromBody] ItemRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre))
                return BadRequest("El nombre no puede estar vacío.");

            ItemStore.Items.Add(request.Nombre);
            return Ok(ItemStore.Items);
        }

        
        [HttpGet("items")]
        public ActionResult<List<string>> ObtenerItems()
        {
            return Ok(ItemStore.Items);
        }

        
        [HttpDelete("eliminar-item/{nombre}")]
        public ActionResult<List<string>> EliminarItem(string nombre)
        {
            if (!ItemStore.Items.Remove(nombre))
                return NotFound($"El ítem '{nombre}' no existe.");

            return Ok(ItemStore.Items);
        }

    }
}
               

        //-----------------------------------------------------
    

