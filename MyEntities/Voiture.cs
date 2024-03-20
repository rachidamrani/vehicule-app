using System.Text.Json.Serialization;

namespace MyLibrary.Vehicules
{
    public class Voiture : Vehicule
    {
        private int _puissance;

        public int Puissance { get => _puissance; set => _puissance = value; }


        public Voiture(string marque, string modele, string numero, int puissance)
        : base(marque, modele, numero)
        {
            _puissance = puissance;
        }


        public override string ToString() => base.ToString() + $" || Puissance: {_puissance}";
    }
}