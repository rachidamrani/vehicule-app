using static System.Console;

namespace MyLibrary.Utils;

public static class Tools
{
    /// <summary>
    /// Récupérer une valeur entrée par l'utilisateur en supprimant les espaces au début et à la fin de la valeur
    /// </summary>
    /// <returns>string</returns>
    public static string GetUserInput() => ReadLine()!.Trim();
    public static void Log(string message = "")
    {
        if(string.IsNullOrEmpty(message))
        {
            WriteLine();
        }

        WriteLine(message);
    }
    
    /// <summary>
    /// Vérifier si la valeur entrée par l'utilisateur est un nombre ayant un nombre de chiffre compris 
    /// entre 4 et 6
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static bool ValidateVehiculeNumber(string number) => 
        number.All(char.IsDigit) && number.Length >= 4 && number.Length <= 6;
    
}
