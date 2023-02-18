using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveEyesFast.Data
{
    public class DataAccess
    {
        public delegate void RefreshListDelegate();
        public static event RefreshListDelegate RefreshList;

        public static List<Product> GetProducts() => SaveEyes2Entities.GetContext().Products.ToList();

        public static List<ProductType> GetProductTypes() => SaveEyes2Entities.GetContext().ProductTypes.ToList();

        public static List<Agent> GetAgents() => SaveEyes2Entities.GetContext().Agents.ToList();

        public static List<AgentType> GetAgentTypes() => SaveEyes2Entities.GetContext().AgentTypes.ToList();

        public static void SaveProduct(Product product)
        {
            if (product.ID == 0)
                SaveEyes2Entities.GetContext().Products.Add(product);

            SaveEyes2Entities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }

        public static void SaveAgent(Agent agent)
        {
            if (agent.ID == 0)
                SaveEyes2Entities.GetContext().Agents.Add(agent);

            SaveEyes2Entities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }

        
    }
}
