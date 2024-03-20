using static MyLibrary.Utils.Tools;
using System.Globalization;
using System.Text.Json;
using MyLibrary.Vehicules;


namespace MyLibrary.Utils
{
    public class UserActions
    {
        public static void LoadActions()
        {
            // Afficher la liste des actions :
            Log(@"
            //////////////////////////////////////
            Quelle action voulez-vous effectuer ?
            -----------------------------
            1 - Créer un véhicule
            2 - Voir tous les vehicules
            3 - Voir un véhicule
            4 - Mettre à jour un véhicule
            5 - Supprimer un véhicule
            6 - Filtrer les véhicules
            7 - Trier / Ordonner les véhicules selon une propriété
            8 - Sauvegarder les véhicules
            -----------------------------
            0 - Quitter l'application
            //////////////////////////////////////");
        }
        public static int GetActionNumber()
        {
            Log("Entrer le numéro de l'action que vous voulez effectuer : ");

            string action = GetUserInput()!;
            bool inputIsValid = int.TryParse(action, out int input) && input >= 0 && input <= 8;

            while (!inputIsValid)
            {
                Log("Entrée invalide! Veuillez choisir un nombre compris entre 1 et 8 : ");
                action = GetUserInput()!;
                inputIsValid = int.TryParse(action, out input) && input >= 0 && input <= 8;
            }

            return input;
        }
        public static string GetVehiculeMarque()
        {
            Log("Marque (Sans chiffre) ? ");
            string marque = GetUserInput()!; // ne pas oublier de prendre en considaration les marques contenant des espaces "____ ____"
            bool inputIsValid = marque.All(char.IsLetter);

            while (!inputIsValid)
            {
                Log("Le nom de la marque ne doit pas contenir des chiffres : ");
                marque = GetUserInput()!;
                inputIsValid = marque.All(char.IsLetter);
            }

            return marque;
        }
        public static string GetVehiculeModel()
        {
            Log("Modèle ? ");
            string modele = GetUserInput()!;

            while (string.IsNullOrWhiteSpace(modele))
            {
                Log("Entrée invalide ! Réssayez : ");
                modele = GetUserInput()!;
            }

            return modele;
        }
        public static string GetVehiculeNumber()
        {
            Log("Numéro (Entre 4 et 6 chiffres) ? ");

            string userInput = GetUserInput()!;
            bool inputIsValid = ValidateVehiculeNumber(userInput);

            while (!inputIsValid)
            {
                Log("Entrée invalide! Le numéro du véhicule doit être composé de 4 à 6 chiffres : ");
                userInput = GetUserInput()!;
                inputIsValid = ValidateVehiculeNumber(userInput);
            }

            return userInput;
        }
        public static string GetVehiculeType()
        {
            Log("Quel type ? 'v' pour voiture, 'c' pour camion : ");

            string type = GetUserInput()!;
            bool typeIsValid = type.ToLower() == "c" || type.ToLower() == "v";

            while (!typeIsValid)
            {
                Log("Entrée invalide! Réssayez : ");
                type = GetUserInput()!;
                typeIsValid = type.ToLower() == "c" || type.ToLower() == "v";
            }

            return type;
        }
        public static int GetVehiculePuissance()
        {
            Log("Puissance ? ");

            string puissance = GetUserInput()!;
            bool inputIsValid = int.TryParse(puissance, out int puissanceValue);

            while (!inputIsValid)
            {
                Log("Entrée invalide ! La puissance de la voiture doit être un nombre entier  : ");
                puissance = GetUserInput()!;
                inputIsValid = int.TryParse(puissance, out puissanceValue);
            }

            return puissanceValue;
        }
        public static double GetVehiculeWeight()
        {
            Log("Poids ? ");
            string userInput = GetUserInput()!;

            bool inputIsValid = double.TryParse(userInput, CultureInfo.InvariantCulture, out double weight);

            while (!inputIsValid)
            {
                Log("Entrée invalide ! Le poids du camion doit être un nombre : ");
                userInput = GetUserInput()!;
                inputIsValid = double.TryParse(userInput, out weight);
            }

            return weight;
        }
        public static List<Vehicule>? LoadAllVehicules()
        {
            string fileContent = File.ReadAllText("data/vehicules_stores.json");

            if (string.IsNullOrEmpty(fileContent)) return null;
            else return JsonSerializer.Deserialize<List<Vehicule>>(fileContent);
        }
        public static async Task SaveAllVehicules(List<Vehicule> vehicules)
        {
            string jsonStr = JsonSerializer.Serialize(vehicules);

            await File.WriteAllTextAsync("data/vehicules_stores.json", jsonStr);

            Log("Liste des véhicules sauvegardés avec succès !");
        }
        public static void FilterVehicules(List<Vehicule> vehicules)
        {
            Log("Par quel critère voulez-vous filtrer les véhicules ? " +
                "Marque ? Modèle ? Poids ou Puissance ?");

            string criteria = GetUserInput().ToLower().Trim();

            List<string> criterias = new List<string>() {
                "marque", "modèle", "poids", "puissance"
            };

            while (!criterias.Contains(criteria.ToLower()))
            {
                Log("Entré invalide! Veuillez choisir l'un des critères : Marque ? Modèle ? Poids ou Puissance : ");
                criteria = GetUserInput().ToLower().Trim();
            }

            Log($"Quelle {criteria} cherchez-vous ? ");
            string searchKey = GetUserInput().ToLower().Trim();

            switch (criteria)
            {
                case "marque":
                    List<Vehicule> searchResult = vehicules.Where(v => v.Marque.ToLower() == searchKey).ToList();

                    if (searchResult.Count == 0) Log("Pas de résulat!");
                    foreach (var vehicule in searchResult)
                    {
                        Log(vehicule.ToString());
                    }
                    break;
                case "modèle":
                    searchResult = vehicules.Where(v => v.Modele.ToLower() == searchKey).ToList();
                    if (searchResult.Count == 0) Log("Pas de résulat!");
                    foreach (var vehicule in searchResult)
                    {
                        Log(vehicule.ToString());
                    }
                    break;
                case "poids":
                    List<Camion> searchResultCamions = vehicules.Where(v => v is Camion).Cast<Camion>().Where(c => c.Poids == double.Parse(searchKey)).ToList();
                    if (searchResultCamions.Count == 0) Log("Pas de résulat!");
                    foreach (Camion camion in searchResultCamions)
                    {
                        Log(camion.ToString());
                    }
                    break;
                case "puissance":
                    List<Voiture> searchResultVoiture = vehicules.Where(v => v is Voiture)
                        .Cast<Voiture>()
                        .Where(v => v.Puissance == double.Parse(searchKey, CultureInfo.InvariantCulture))
                        .ToList();
                    if (searchResultVoiture.Count == 0) Log("Pas de résulat!");

                    foreach (Voiture camion in searchResultVoiture)
                    {
                        Log(camion.ToString());
                    }
                    break;
            }
        }
        public static void SortVehicules(List<Vehicule> vehicules)
        {
            Log("Vous voulez trier les véhicules via quelle propriété ? Marque ? Modèle ? Poids ou Puissance ? ");

            string property = GetUserInput().ToLower().Trim();

            List<string> properties = new List<string>() {
                "marque", "modèle", "poids", "puissance"
            };

            while (!properties.Contains(property))
            {
                Log("Entré invalide! Veuillez choisir l'un des critères : Marque ? Modèle ? Poids ou Puissance : ");
                property = GetUserInput().ToLower().Trim();
            }

            switch (property)
            {
                case "numéro":
                    List<Vehicule> sortResult = vehicules.OrderBy(v => int.Parse(v.Numero))
                        .ToList();

                    foreach (Vehicule vehicule in sortResult)
                    {
                        Log(vehicule.ToString());
                    }
                    break;
                case "marque":
                    sortResult = vehicules.OrderBy(v => v.Marque).ToList();

                    foreach (Vehicule vehicule in sortResult)
                    {
                        Log(vehicule.ToString());
                    }
                    break;
                case "modèle":
                    sortResult = vehicules.OrderBy(v => v.Modele).ToList();
                    foreach (Vehicule vehicule in sortResult)
                    {
                        Log(vehicule.ToString());
                    }
                    break;

                case "poids":
                    List<Camion> sortResultCamion = vehicules.Where(v => v is Camion)
                        .Cast<Camion>()
                        .OrderBy(v => v.Poids).ToList();

                    foreach (Camion camion in sortResultCamion)
                    {
                        Log(camion.ToString());
                    }
                    break;
                case "puissance":
                    List<Voiture> sortResultVoiture = vehicules.Where(v => v is Voiture)
                        .Cast<Voiture>()
                        .OrderBy(v => v.Puissance).ToList();

                    foreach (Voiture camion in sortResultVoiture)
                    {
                        Log(camion.ToString());
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
