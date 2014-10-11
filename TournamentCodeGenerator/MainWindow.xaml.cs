﻿using System;
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
            string name = Name.Text;
            string pass = Pass.Password;
            if (string.IsNullOrEmpty(name))
                name = RandomGen(10);
            if (string.IsNullOrEmpty(pass))
                pass = RandomGen(10);
            //*/

            if (!name.ToLower().Contains("'s game"))
                name += "'s game";

            Result.Text = "pvpnet://lol/customgame/joinorcreate";
            Result.Text += "/map" + (Map.SelectedItem as ComboboxItem).Value.ToString();
            Result.Text += "/pick" + (Type.SelectedItem as ComboboxItem).Value.ToString();
            Result.Text += "/team" + (Map.SelectedItem as ComboboxItem).Players.ToString();
            if ((bool)Spec.IsChecked)
                Result.Text += "/spec" + "ALL";
            else
                Result.Text += "/spec" + "NONE";

            string json = "";
                json = "{ \"name\": \"" + name + "\", \"password\": \"" + pass + "\" }";

            //Also "report" to get riot to send a report back, and "extra" to have data sent so you can identify it (passbackDataPacket)

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(json);
            Result.Text += "/" + System.Convert.ToBase64String(plainTextBytes);
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