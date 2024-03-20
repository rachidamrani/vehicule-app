using static MyLibrary.Utils.Tools;
using static MyLibrary.Utils.UserActions;
using MyLibrary.Vehicules;

namespace MyLibrary
{
    public class VehiculeRepository
    {
        private List<Vehicule> _vehicules = new List<Vehicule>();
        public List<Vehicule> GetAllVehicules() => _vehicules;
 
        /// <summary>
        /// Afficher tous les véhicules
        /// </summary>
        public void ViewAllVehicules()
        {
            if (_vehicules.Count == 0)
            {
                Log("La liste des vehicules est vide !");
            }
            else
            {
                int index = 1;

                foreach (Vehicule vehicule in _vehicules)
                {
                    Log($"{index} => {vehicule}");
                    index++;
                }

            }
        }
        
        /// <summary>
        /// Afficher un véhicule spécifique en utilisant son Numéro
        /// </summary>
        public void ViewVehiculeByNum()
        {
            Log("Entrer le numero du véhicule : ");

            string userInput = GetUserInput().Trim();

            Vehicule? searchResult = _vehicules.Find(v => v.Numero == userInput);

            if (searchResult is not null)
            {
                Log(searchResult.ToString());
            }
            else
            {
                Log("Vehicule inexistant.");
            }
        }
        
        /// <summary>
        /// Stocker les véhicules déja enregistré dans la list _vehicules
        /// </summary>
        /// <param name="data"></param>
        public void SetVehicules(List<Vehicule> data) => _vehicules = data;
        
        /// <summary>
        /// Ajouter un nouveau véhicule
        /// </summary>
        /// <param name="vehicule">Ajouter un nouveau véhicule</param>
        public void AddNewVehicule(Vehicule vehicule)
        {
            Vehicule? vehiculeExists = _vehicules.Find(v => v.Numero == vehicule.Numero);

            if (vehiculeExists is not null) Log("Le vehicule existe déjà");
            else
            {
                _vehicules.Add(vehicule);
                Log("Véhicule crée !");
            };
        }
        
        /// <summary>
        /// Supprimer un véhicule par son Numéro
        /// </summary>
        public void DeleteVehiculeByNum()
        {
            Log("Entrer le numero du vehicule que vous voulez supprimer : ");

            string userInput = GetUserInput();

            int index = _vehicules.FindIndex(v => v.Numero == userInput);

            if (index != -1)
            {
                Log("Êtes-vous sures ? [O]/[N]");
                userInput = GetUserInput().ToLower();

                if (userInput == "o")
                {
                    _vehicules.RemoveAt(index);
                    Log("Vehicule supprimé avec succès !");
                }
                

            }
            else
            {
                Log("Vehicule inexistant !");
            }
        }
        
        /// <summary>
        /// Mettre à jour un véhicule
        /// </summary>
        public void UpdateVehiculeByNum()
        {
            Log("Entrer le numéro du véhicule que vous voulez modifier : ");
            string userInput = GetUserInput().Trim().ToLower();
            int index = _vehicules.FindIndex(v => v.Numero == userInput);

            if (index != -1)
            {
                Vehicule foundVehicule = _vehicules[index];

                string marque = GetVehiculeMarque();
                string modele = GetVehiculeModel();
                string number = GetVehiculeNumber();
                string type = GetVehiculeType();

                _vehicules.Remove(foundVehicule);

                if (type == "c")
                {
                    // Récupérer le poids du camion
                    double weight = GetVehiculeWeight();
                    Camion camion = new Camion(marque, modele, number, weight);

                    _vehicules.Insert(index, camion);

                    Log("Véhicule mis à jour avec succès !");
                }
                else
                {
                    // Récupérer la puissance de la voiture 
                    int puissance = GetVehiculePuissance();
                    Voiture voiture = new Voiture(marque, modele, number, puissance);
                    _vehicules.Insert(index, voiture);

                    Log("Véhicule modifié avec succès !");

                }
            }
            else
            {
                Log("Véhicule inexistant !");

            }

        }
    }
}
