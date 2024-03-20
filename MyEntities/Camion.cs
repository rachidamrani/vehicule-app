using System.Text.Json.Serialization;

namespace MyLibrary.Vehicules
{
    public class Camion : Vehicule
    {
        private double _poids;

        public double Poids { get => _poids; set => _poids = value; }


        public Camion(string marque, string modele, string numero, double poids)
        : base(marque, modele, numero)
        {
            _poids = poids;
        }

        public override string ToString() => base.ToString() + $"|| Poids: {_poids}";

    }
}