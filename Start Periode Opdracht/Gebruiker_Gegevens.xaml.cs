using System.Windows;

namespace Start_Periode_Opdracht;

public partial class Gebruiker_Gegevens : Window
{
    public int Leeftijd;
    public float Lengte;
    public float Gewicht;
    public Gebruiker_Gegevens(string Naam, int Leeftijd = 0, float Lengte = 0, float Gewicht = 0)
    {
        InitializeComponent();
        Gebruikersnaam_Label.Content = $"Gebruikersnaam: {Naam}";
        Leeftijd_Label.Content = $"Leeftijd: {Leeftijd}";
        Lengte_Label.Content = $"Lengte: {Lengte}";
    }
}