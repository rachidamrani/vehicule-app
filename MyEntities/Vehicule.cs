using MyProject.Utils;
using System.Text.Json.Serialization;

namespace MyLibrary.Vehicules
{
    [JsonDerivedType(typeof(Voiture), "voiture")]
    [JsonDerivedType(typeof(Camion), "camion")]
    public abstract class Vehicule
    {
        private string _marque = null!;
        private string _modele = null!;
        private string _numero = null!;

        public string Marque { get => _marque; private set => _marque = value; }
        public string Modele { get => _modele; private set => _modele = value; }
        public string Numero { get => _numero; private set => _numero = value; }

        public Vehicule(string marque, string modele, string numero)
        {
            Marque = marque;
            Modele = modele;
            Numero = numero;
        }

        public override string ToString() => $"Marque : {_marque} || Modèle : {_modele} || Numéro : {_numero} ";

    }
}