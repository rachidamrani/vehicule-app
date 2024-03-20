using static MyLibrary.Utils.UserActions;
using static MyLibrary.Utils.Tools;
using MyLibrary;
using MyLibrary.Vehicules;

namespace MyProject.Utils;

public static class Processor
{
    /// <summary>
    /// Lancement du traitement des actions effectu�s par l'utilisatuer
    /// </summary>
    /// <param name="vehiculeRepository"></param>
    /// <returns></returns>
    public static async Task Process(VehiculeRepository vehiculeRepository)
    {
        bool quit = false;

        do
        {
            // Afficher les actions que l'utilisateur peut effectuer
            LoadActions();
            // R�cup�rer le nombre de l'action :
            int actionNumber = GetActionNumber();

            switch (actionNumber)
            {
                case 1:
                    // R�cup�rer la marque du v�hicule :
                    string marque = GetVehiculeMarque();
                    // R�cup�rer le modele du v�hicule :
                    string modele = GetVehiculeModel();
                    // R�cup�rer le numero du v�hicule :
                    string number = GetVehiculeNumber();
                    // R�cup�rer le type du v�hicule ainsi que sa puissance si et seulement s'il est de type voiture:
                    string type = GetVehiculeType();

                    if (type == "c")
                    {
                        // R�cup�rer le poids du camion
                        double weight = GetVehiculeWeight();
                        Camion camion = new Camion(marque, modele, number, weight);
                        vehiculeRepository.AddNewVehicule(camion);
                    }
                    else
                    {
                        // R�cup�rer la puissance de la voiture 
                        int puissance = GetVehiculePuissance();
                        Voiture voiture = new Voiture(marque, modele, number, puissance);
                        vehiculeRepository.AddNewVehicule(voiture);
                    }

                    break;
                case 2:
                    vehiculeRepository.ViewAllVehicules();
                    break;
                case 3:
                    vehiculeRepository.ViewVehiculeByNum();
                    break;
                case 4:
                    vehiculeRepository.UpdateVehiculeByNum();
                    break;
                case 5:
                    vehiculeRepository.DeleteVehiculeByNum();
                    break;
                case 6:
                    FilterVehicules(vehiculeRepository.GetAllVehicules());
                    break;
                case 7:
                    SortVehicules(vehiculeRepository.GetAllVehicules());
                    break;
                case 8:
                    await SaveAllVehicules(vehiculeRepository.GetAllVehicules());
                    break;
                case 0:
                    Log("Voulez-vous quitter l'application ? [O]/[N] : ");
                    if (GetUserInput().ToLower() == "o") { Log("Au revoir!"); quit = true; }
                    break;
            }

        } while (!quit);
    }
   
}