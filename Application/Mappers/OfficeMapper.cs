using FinalProjectBakary.Domain.Dtos;
using FinalProjectBakary.Domain.Entities;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Persistence.Entities;

namespace FinalProjectBakary.Application.Mappers
{
    public static class OfficeMapper
    {
        public static OfficeDto ToDto (this Office office)
        {
            return new OfficeDto()
            {
                Name = office.Name,
                MaximumCapacity = office.GetAvailableCapacity(),
                Menu = office.Menu,
                OrderQueue = office.OrderQueue,
                FinishedOrderList = office.FinishedOrderList,
                AvailableCapacity = office.GetAvailableCapacity()
            };
        }
        public static void ShowCompleteListDetails(this Office office)
        {
            OfficeResume resume = office.GetOfficeTotalResume();

            Console.WriteLine($"------------------------- Bakery {office.Name} ---------------------------------");
            Console.WriteLine($"* Current capacity: {office.GetAvailableCapacity()}/{office.MaximumCapacity}");
            Console.WriteLine($"* Total profits: {resume.TotalEarnings}");
            Console.WriteLine($"* Total units sold: {resume.TotalUnits}");
            Console.Write("");

            Console.WriteLine($"-- Menu available in {office.Name}  --");
            foreach (string breadName in office.Menu.GetAvailableBreadsNames())
            {
                Console.WriteLine($"---- {breadName}");
            }
            Console.Write("");

            Console.WriteLine($"-- Pending orders to be prepared in {office.Name} --");
            ShowOrders(office, office.OrderQueue);
            Console.WriteLine($"-- Orders already prepared in {office.Name}  --");
            ShowOrders(office, office.FinishedOrderList);
            Console.WriteLine($"----------------------------------------------------------");
            Console.Write("");
        }
        public static void ShowOrders(this Office office, IEnumerable<Order> orders)
        {
            if( orders.Count()> 0)
            {
                foreach (Order order in orders)
                {
                    Console.WriteLine($"-- Order: {order.Id} - Created at: {order.Audit.CreatedAt}");
                    foreach (OrderBread product in order.OrderBreads)
                    {
                        Console.WriteLine($"----Bread: {product.Bread.Name} - Qy: {product.Quantity}");
                    }
                    Console.WriteLine($"----------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine($"---- There are no orders in the list ");
            }
        }  
        public static void ShowResumenEarningsAndQuantity(this Office office)
        {
            OfficeResume resume = office.GetOfficeTotalResume();
            Console.WriteLine($"-- Office resume for {resume.OfficeName}");
            Console.WriteLine($"---- Total profits: {resume.TotalEarnings}");
            Console.WriteLine($"---- Total units: {resume.TotalUnits}");
        }
    }
}