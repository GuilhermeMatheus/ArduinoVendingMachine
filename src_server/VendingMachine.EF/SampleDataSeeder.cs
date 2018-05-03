using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;

namespace VendingMachine.EF
{
    public class SampleDataSeeder
    {
        public async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = (VendingMachineDbContext)serviceProvider.GetService(typeof(VendingMachineDbContext));
            await Initialize(context);
        }

        public async Task Initialize(VendingMachineDbContext context)
        {
            await PopulateClientCardsAsync(context);

            await PopulateProductsAsync(context);
            await context.SaveChangesAsync();

            await PopulateMachinesAsync(context);
            await context.SaveChangesAsync();

            await PopulateProductRailAsync(context);
            await context.SaveChangesAsync();
        }

        private static async Task PopulateClientCardsAsync(VendingMachineDbContext context)
        {
            if (context.ClientCards.Any())
                return;

            var clientCard = new ClientCard { Rfid = 69801812, Alias = "Guilherme Matheus Costa", Credit = 100 };
            await context.ClientCards.AddAsync(clientCard);
        }

        private static async Task PopulateProductsAsync(VendingMachineDbContext context)
        {
            if (context.Products.Any())
                return;

            var id = 0;
            await context.Products.AddRangeAsync(
                new Product {Id = id++, DisplayName = "Bolacha Passa-tempo", Price = 4.55m },
                new Product {Id = id++, DisplayName = "Bolacha Negresco", Price = 4.50m },
                new Product {Id = id++, DisplayName = "Doritos Frito/Assado", Price = 6m },
                new Product {Id = id++, DisplayName = "Twix cobertura dupla", Price = 3.50m },
                new Product {Id = id++, DisplayName = "Barra de cereal diet", Price = 1.50m },
                new Product {Id = id++, DisplayName = "Snicker", Price = 2.50m },
                new Product {Id = id++, DisplayName = "Bolo floresta negra", Price = 7m },
                new Product {Id = id++, DisplayName = "M&Ms", Price = 3m },
                new Product {Id = id++, DisplayName = "Refrigerante Pepsi", Price = 5m },
                new Product {Id = id++, DisplayName = "Coca-cola zero", Price = 5m },
                new Product {Id = id++, DisplayName = "Suco de uva DelValle", Price = 4.50m },
                new Product {Id = id++, DisplayName = "Suco de laranja Ades", Price = 4.50m },
                new Product {Id = id++, DisplayName = "Lanche natural vegan", Price = 6.50m },
                new Product {Id = id++, DisplayName = "Lanche peito de peru", Price = 6.50m },
                new Product {Id = id++, DisplayName = "Hamburguer X-Tudo", Price = 6.50m },
                new Product {Id = id++, DisplayName = "Bolacha Negresco", Price = 4.50m }
            );
        }

        private static async Task PopulateMachinesAsync(VendingMachineDbContext context)
        {
            if (context.Machines.Any())
                return;

            var machines = new List<Machine>
            {
                new Machine
                {
                     Alias = "Protótipo",
                     Address ="R. Delfim Moreira, 102",
                     IsActivated = true
                }
            };

            foreach (var item in machines)
                await context.Machines.AddAsync(item);
        }

        private static async Task PopulateProductRailAsync(VendingMachineDbContext context)
        {
            if (context.ProductRails.Any())
                return;

            var machine = context.Machines.Find(1);
            var products = context.Products.ToList();

            for (int i = 1; i <= 16; i++)
            {
                var productRail = new ProductRail
                {
                    Machine = machine,
                    Helix = i,
                    Count = 3,
                    Product = products[i-1]
                };

                await context.ProductRails.AddAsync(productRail);
            }
        }
    }
}
