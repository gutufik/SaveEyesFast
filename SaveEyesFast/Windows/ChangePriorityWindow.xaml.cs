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
using System.Windows.Shapes;
using SaveEyesFast.Data;

namespace SaveEyesFast.Windows
{
    /// <summary>
    /// Interaction logic for ChangePriorityWindow.xaml
    /// </summary>
    public partial class ChangePriorityWindow : Window
    {
        public List<Agent> Agents { get; set; }
        public ChangePriorityWindow(List<Agent> agents)
        {
            InitializeComponent();
            Agents = agents;
            tbPriority.Text = Agents.Max(x => x.Priority).ToString();
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            var priority = 0;

            if (!int.TryParse(tbPriority.Text, out priority) || priority < 0 )
            {
                MessageBox.Show("", "Неверный приоритет");
                return;
            }

            foreach(Agent agent in Agents)
            {
                agent.Priority += priority;
                DataAccess.SaveAgent(agent);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
