using System;

namespace SanGiu.Taxi.DomainModel
{
    public class Taxi : DomainObject
    {
        static Random rnd = new Random((int)DateTime.Now.Ticks);

        public int Id { get; set; }
        public string Targa { get; set; }
        public int Km { get; set; }
        public double Prezzo { get; set; }
        public string Autista { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Targa} - km.{Km}";
        }

        public static Taxi CreateRandom()
        {
            Taxi t = new Taxi();
            t.Km = rnd.Next(0, 400000);
            t.Prezzo = rnd.NextDouble() * 8.0;
            t.Targa = $"Targa {rnd.Next(1, 500)}";
            t.Autista = $"Nome {rnd.Next(1, 5000)}";

            return t;
        }
    }
}