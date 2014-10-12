using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TournamentCodeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            ComboboxItem item = new ComboboxItem();
            item.Text = "Summoners Rift";
            item.Value = 1;
            item.Players = 5;
            Map.Items.Add(item);
            Map.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "The Crystal Scar";
            item.Value = 8;
            item.Players = 5;
            Map.Items.Add(item);
            Map.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "The Howling Abyss";
            item.Value = 12;
            item.Players = 5;
            Map.Items.Add(item);
            Map.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "The Twisted Treeline";
            item.Value = 10;
            item.Players = 3;
            Map.Items.Add(item);
            Map.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Blind Pick";
            item.Value = 1;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "No Ban Draft";
            item.Value = 3;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "All Random";
            item.Value = 4;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Open Pick";
            item.Value = 5;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Blind Draft";
            item.Value = 7;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Blind Pick";
            item.Value = 10;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Blind Draft";
            item.Value = 7;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Infinite Time Blind Pick";
            item.Value = 11;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Captain Pick";
            item.Value = 10;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Tournament Draft";
            item.Value = 6;
            Type.Items.Add(item);
            Type.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "No Spectators";
            item.Value = "NONE";
            SpectateMode.Items.Add(item);
            SpectateMode.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Lobby Only";
            item.Value = "LOBBYONLY";
            SpectateMode.Items.Add(item);
            SpectateMode.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "Friends List Only";
            item.Value = "DROPINONLY";
            SpectateMode.Items.Add(item);
            SpectateMode.SelectedIndex = 0;

            item = new ComboboxItem();
            item.Text = "All";
            item.Value = "ALL";
            SpectateMode.Items.Add(item);
            SpectateMode.SelectedIndex = 0;
        }
        public static string RandomGen(int length)
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.WaterTextbox.Text;
            string pass = Pass.WaterPasswordbox.Password;
            if (string.IsNullOrEmpty(name))
                name = RandomGen(10);
            if (string.IsNullOrEmpty(pass))
                pass = RandomGen(10);
            //*/
            if (name.StartsWith("\"") && name.EndsWith("\""))
            {
                name = name.Substring(1);
                name = name.Remove(name.Length - 1);
            }
            else if (!name.ToLower().Contains("'s game"))
                name += "'s game";

            Result.Text = "pvpnet://lol/customgame/joinorcreate";
            Result.Text += "/map" + (Map.SelectedItem as ComboboxItem).Value.ToString();
            Result.Text += "/pick" + (Type.SelectedItem as ComboboxItem).Value.ToString();
            Result.Text += "/team" + (Map.SelectedItem as ComboboxItem).Players.ToString();
            Result.Text += "/spec" + (SpectateMode.SelectedItem as ComboboxItem).Value.ToString();
            string json = "";

            if (String.IsNullOrEmpty(Report.WaterTextbox.Text))
            {                
                json = "{ \"name\": \"" + name + "\", \"password\": \"" + pass + "\" }";
            }
            else if (!String.IsNullOrEmpty(Extra.WaterTextbox.Text))
            {
                json = "{ \"name\": \"" + name + "\",\"extra\":\"" + Extra.WaterTextbox.Text + "\" \"password\": \"" + pass + "\" \"report\":\"" + Report.WaterTextbox.Text + "\"}";
            }
            else
            {
                json = "{ \"name\": \"" + name + "\",\"extra\":\"\" \"password\": \"" + pass + "\" \"report\":\"" + Report.WaterTextbox.Text + "\"}";
            }

            //Also "report" to get riot to send a report back, and "extra" to have data sent so you can identify it (passbackDataPacket)

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(json);
            Result.Text += "/" + System.Convert.ToBase64String(plainTextBytes);
        }
        private void Extra_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }
    }
    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public object Players { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
