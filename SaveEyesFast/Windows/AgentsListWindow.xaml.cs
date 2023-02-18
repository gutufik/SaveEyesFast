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

namespace SaveEyesFast.Windows
{
    /// <summary>
    /// Interaction logic for AgentsListWindow.xaml
    /// </summary>
    public partial class AgentsListWindow : Window
    {
        public List<Agent> Agents { get; set; }
        public List<AgentType> AgentTypes { get; set; }
        public List<Agent> AgentsForFilters { get; set; }

        private int _page = 0;
        private int _countOnPage = 10;

        public Dictionary<string, Func<Agent, object>> Sortings { get; set; }

        public AgentsListWindow()
        {
            InitializeComponent();
            Agents = DataAccess.GetAgents();
            AgentTypes = DataAccess.GetAgentTypes();
            AgentTypes.Insert(0, new AgentType
            {
                Title = "Все типы",
                Agents = Agents
            });

            Sortings = new Dictionary<string, Func<Agent, object>>
            {
                {"Наименование по убыванию", x=> x.Title },
                {"Наименование по возрастанию", x=> x.Title },
                {"Скидка по убыванию", x=> x.Discount },
                {"Скидка по возрастанию", x=> x.Discount },
                {"Приоритет по убыванию", x=> x.Priority },
                {"Приоритет по возрастанию", x=> x.Priority },
            };

            DataContext = this;
        }

        public void SetPageNumbers()
        {
            var pagesCount = AgentsForFilters.Count % _countOnPage == 0 ? AgentsForFilters.Count / _countOnPage: AgentsForFilters.Count / _countOnPage + 1;

            spPageNumbers.Children.Clear();
            spPageNumbers.Children.Add(new TextBlock { Text = $"<" });
            for (int i=0; i < pagesCount; i++)
            {
                spPageNumbers.Children.Add(new TextBlock { Text = $"{i + 1}" });
            }
            spPageNumbers.Children.Add(new TextBlock { Text = $">" });

            foreach (var child in spPageNumbers.Children)
            {
                (child as UIElement).MouseDown += AgentsListWindow_MouseDown;
            }
        }

        private void AgentsListWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var text = (sender as TextBlock).Text;
            var pagesCount = AgentsForFilters.Count % _countOnPage == 0 ? AgentsForFilters.Count / _countOnPage : AgentsForFilters.Count / _countOnPage + 1;

            if (text == "<")
            {
                if (_page > 0)
                    _page--;
            }
            else if (text == ">")
            {
                if (_page < pagesCount-1)
                    _page++;
            }
            else
            {
                _page = int.Parse(text) -1;
            }
            Filter(false);
        }

        public void Filter(bool filtersChanged)
        {
            if (filtersChanged)
                _page = 0;

            var agentType = cbFilter.SelectedItem as AgentType;
            var sort = cbSort.SelectedItem as string;
            var searchText = tbSearch.Text.ToLower();

            DataAccess.RefreshList += DataAccess_RefreshList;

            if (agentType != null && sort != null)
            {
                AgentsForFilters = agentType.Agents.Where(x => 
                    x.Title.ToLower().Contains(searchText) ||
                    x.Phone.ToLower().Contains(searchText) ||
                    x.Email.ToLower().Contains(searchText)).ToList();


                AgentsForFilters = AgentsForFilters.OrderBy(Sortings[sort]).ToList();
                if (sort.Contains("убыванию"))
                    AgentsForFilters.Reverse();

                lvAgents.ItemsSource = AgentsForFilters.Skip(_page * _countOnPage).Take(_countOnPage);
                lvAgents.Items.Refresh();
                SetPageNumbers();
            }
        }

        private void DataAccess_RefreshList()
        {
            Agents = DataAccess.GetAgents();
            Filter(true);
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter(true);
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter(false);
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter(true);
        }

        private void btnChangePriority_Click(object sender, RoutedEventArgs e)
        {
            var agents = lvAgents.SelectedItems.Cast<Agent>().ToList();
            (new Windows.ChangePriorityWindow(agents)).ShowDialog();
        }

        private void lvAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var agent = (lvAgents.SelectedItem as Agent);

            btnChangePriority.Visibility = agent != null? Visibility.Visible:Visibility.Hidden;
        }

        private void lvAgents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var agent = (lvAgents.SelectedItem as Agent);
            if (agent != null)
                new AgentWindow(agent).ShowDialog();
        }
    }
}
