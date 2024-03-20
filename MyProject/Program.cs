using static MyLibrary.Utils.UserActions;
using static MyProject.Utils.Processor;
using MyLibrary;
using MyLibrary.Vehicules;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Creation d'un repository de vehicules
        VehiculeRepository vehiculeRepository = new VehiculeRepository();
        
        // Récupérer la liste des véhicules déja enregistrés
        List<Vehicule>? loadedData = LoadAllVehicules();

        // Affecter les véhicules enregistrés au repository créé en amont
        if (loadedData is not null) vehiculeRepository.SetVehicules(loadedData);
        
        // Traitement
        await Process(vehiculeRepository);
    }
}