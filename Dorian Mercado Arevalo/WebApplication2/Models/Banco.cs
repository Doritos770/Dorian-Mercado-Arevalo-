namespace WebApplication2.Models
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public string Titular { get; set; }

        private decimal saldo;

        public decimal Saldo
        {
            get => saldo;
            private set
            {
                if (value < 0)
                    throw new Exception("El saldo no puede ser negativo.");
                saldo = value;
            }
        }

        public CuentaBancaria(string titular, decimal saldoInicial)
        {
            Id = new Random().Next(1, 1000);
            Titular = titular;
            Saldo = saldoInicial;
        }

        public void Depositar(decimal monto)
        {
            if (monto <= 0)
                throw new Exception("El depósito debe ser mayor a 0.");
            Saldo += monto;
        }
    }
    public class DepositoRequest
    {
        public string Titular { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Monto { get; set; }
    }

}
