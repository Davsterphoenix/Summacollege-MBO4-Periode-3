using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System;

namespace Start_Periode_Opdracht;
public partial class MainWindow : Window
{
    public int Geselecteerd = 1;
    public List<Gebruiker> Gebruikers = new List<Gebruiker>();
    public class Gebruiker
    {
        public string Voornaam;
        public string Achternaam;
        public int Leeftijd;
        public float Lengte;
        public float Gewicht;
    }
    public MainWindow()
    {
        
        InitializeComponent();
        DummyPeople();
        AddAllToListbox();
    }

    public void DummyPeople()
    {
        //Maakt 3 Dummy Gebruikers aan volgens de instructie
        Gebruikers.Add(new Gebruiker() { Voornaam = "Jan", Achternaam = "Jansen", Leeftijd = 25, Lengte = 1.75f, Gewicht = 75.5f});
        Gebruikers.Add(new Gebruiker() { Voornaam = "Emma", Achternaam = "de Vries" , Leeftijd = 22, Lengte = 1.65f, Gewicht = 65.5f});
        Gebruikers.Add(new Gebruiker() { Voornaam = "Pieter", Achternaam = "de Boer", Leeftijd = 23, Lengte = 1.80f, Gewicht = 80.5f });
    }
    public void AddAllToListbox()
    {
        //Verwijdert eerst alle items uit de listbox
        ListBoxUserView.Items.Clear();
        //Kijkt voor alle gebruiker objecten in de lijst en voegt de naam toe aan de lijstbox
        foreach (var gebruiker in Gebruikers)
        {
            ListBoxUserView.Items.Add(gebruiker.Voornaam + " " + gebruiker.Achternaam);
        }
    }
    
    private void ListBoxUserView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        
        //Neemt het nummer uit de lijst en vergelijkt dit
        var selectedIndex = ListBoxUserView.SelectedIndex;
        int LT = Gebruikers[selectedIndex].Leeftijd;
        float LE = Gebruikers[selectedIndex].Lengte;
        float GE = Gebruikers[selectedIndex].Gewicht;
        
        Console.WriteLine($"Double clicked {ListBoxUserView.SelectedItem}");
        Gebruiker_Gegevens Gebruiker_Gegevens_Object = new Gebruiker_Gegevens(ListBoxUserView.SelectedItem.ToString(),LT,LE,GE );
        
        
        this.Hide();
        Gebruiker_Gegevens_Object.ShowDialog();
        this.Show();
    }
    private void Selected_Tile(object sender, MouseButtonEventArgs e)
{
    if (ListBoxUserView.SelectedIndex >= 0) 
    {
        Geselecteerd = ListBoxUserView.SelectedIndex;
    }
}

private void Delete_Button_Click(object sender, RoutedEventArgs e)
{
    DeleteProcess();
}

public void DeleteProcess()
{
    try
    {
        if (Geselecteerd >= 0 && Geselecteerd < ListBoxUserView.Items.Count)
        {
            Gebruikers.RemoveAt(Geselecteerd);
            AddAllToListbox();
        }
        else
        {
            MessageBox.Show("Selecteer een geldige gebruiker om te verwijderen.");
        }
    }
    catch (Exception exception)
    {
        MessageBox.Show(exception.Message);
    }
}

private void NieuweGebruiker_Button(object sender, RoutedEventArgs e)
{
    try
    {
        Gebruikers.Add(new Gebruiker() { Voornaam = NieuweVoornaam.Text, Achternaam = NieuweAchternaam.Text, Leeftijd = Convert.ToInt32(NieuweLeeftijd.Text), Lengte = Convert.ToInt32(NieuweLengte.Text), Gewicht = Convert.ToInt32(NieuweGewicht.Text)});
        AddAllToListbox();
    }
    catch (Exception exception)
    {
        MessageBox.Show(exception.Message);
    }
}

private void SaveToFileButton_OnClick(object sender, RoutedEventArgs e)
{
    OpslaanNaarBestand();
}
private void Load_OnClick(object sender, RoutedEventArgs e)
{
    LoadFromFile();
}



private void LoadFromFile()
{
    try
    {
        
OpenFileDialog openFileDialog = new OpenFileDialog
{
    Filter = "User Files|*.users",
    Title = "Select a User File"
};

if (openFileDialog.ShowDialog() == true)
{
    using (var reader = new StreamReader(openFileDialog.FileName))
    {
        string line;
        Gebruikers.Clear();
        while ((line = reader.ReadLine()) != null)
        {
            var userData = line.Split(',');
            if (userData.Length == 5)
            {
                Gebruikers.Add(new Gebruiker
                {
                    Voornaam = userData[0],
                    Achternaam = userData[1],
                    Leeftijd = int.Parse(userData[2]),
                    Lengte = float.Parse(userData[3]),
                    Gewicht = float.Parse(userData[4])
                });
                AddAllToListbox();
            }
        }
    }
    AddAllToListbox();
}
    }
    catch (Exception e)
    {
        MessageBox.Show(e.Message);
        throw;
    }
}
private void OpslaanNaarBestand()
{
    var dialog = new SaveFileDialog
{
    Filter = "User Files|*.users",
    FileName = "userData.users"
};
if (dialog.ShowDialog() == true)
    using (var writer = new StreamWriter(dialog.FileName))
    {
        foreach (var gebruiker in Gebruikers)
            writer.WriteLine($"{gebruiker.Voornaam},{gebruiker.Achternaam},{gebruiker.Leeftijd},{gebruiker.Lengte},{gebruiker.Gewicht}");
    }
}
}