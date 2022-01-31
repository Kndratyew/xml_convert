using convertorVl.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using convertorVl.Data;
using Newtonsoft.Json;

namespace convertorVl
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Model.Valute> valutes;
        public MainWindow()
        {
            InitializeComponent();
            HttpClient client = new HttpClient();
            var respose =
                client.GetAsync("https://www.cbr.ru/scripts/XML_daily.asp")
                    .GetAwaiter().GetResult();

            var text = respose.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            valutes = Data.ValuteLoader.LoadValutes(text);
            valutes.Insert(0, new Model.Valute { Name = "Российский Рубль", Value = 1, CharCode = "RUB" });
            // удалили DisplayMemberPath
            FromComboBox.ItemsSource = valutes;
            ToComboBox.ItemsSource = valutes;

        }
        private void FilterText(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int x))
            {
                e.Handled = true;
            }
        }

        private void TextBox1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Model.Valute inValute = FromComboBox.SelectedItem as Valute;
            Valute outValute = ToComboBox.SelectedItem as Valute;
            if (inValute == null || outValute == null) return;
            bool succ = int.TryParse(TextBox1.Text, out int value);
            if (!succ) return;
            double rubles = value * inValute.Value;
            double result = rubles / outValute.Value;
            RezutTextBox.Text = Math.Round(result, 2).ToString();
        }
    }

}
