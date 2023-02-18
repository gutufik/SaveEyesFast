using System;
using System.IO;
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
using System.Windows.Shapes;
using SaveEyesFast.Data;
using Microsoft.Win32;

namespace SaveEyesFast.Windows
{
    /// <summary>
    /// Interaction logic for AgentWindow.xaml
    /// </summary>
    public partial class AgentWindow : Window
    {
        public Agent Agent { get; set; }
        public List<Product> Products { get; set; }
        public List<AgentType> AgentTypes { get; set; }
        public AgentWindow(Agent agent)
        {
            InitializeComponent();
            Agent = agent;
            Products = DataAccess.GetProducts();
            AgentTypes = DataAccess.GetAgentTypes();

            DataContext = this;
        }

        private void btnChangePhoto_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Agent.Logo = File.ReadAllBytes(openFileDialog.FileName);
                AgentImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void cbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
