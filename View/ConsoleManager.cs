using FinalProjectBakary.Application.Mappers;
using FinalProjectBakary.Domain.Common;
using FinalProjectBakary.Domain.Dtos;
using FinalProjectBakary.Domain.Entities;
using FinalProjectBakary.Domain.Entities.Breads;
using FinalProjectBakary.Domain.Entities.Enums;
using FinalProjectBakary.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinalProjectBakary.View
{
    public class ConsoleManager
    {
        private OfficeManager _officeManager ;
        public ConsoleManager(OfficeManager officeManager)
        {
            _officeManager = officeManager;
        }
        public void Init()
        {
            _officeManager.CreateMockData();
            MainMenu();
        }

        // Menus
        #region
        private void MainMenu()
        {
            bool flag = true;
            Console.WriteLine(" --------- WELCOME --------- ");
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("- 1: Office menu");
                Console.WriteLine("- 2: Show historic resume");
                Console.WriteLine("- 9: EXIT");
                Console.Write("");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowOfficeMenu();
                        break;
                    case "2":
                        ShowHistoricResumeForAllOffices();
                        break;
                    case "9":
                        flag = false;
                        break;
                }
            }
            while (flag);
            Console.WriteLine("-- Good bye --");
            Console.Write("");
        }
        public void CurrentOfficceMenu()
        {
            bool flag = true;
            Office currentOffice = GetCurrentOffice();
            do
            {
                Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()}  --------- ");
                ShowCurrentOfficeHeader();
                Console.WriteLine("Choose an option:");
                Console.WriteLine("- 1: Create order");
                Console.WriteLine("- 2: Prepare all orders");
                Console.WriteLine("- 3: Show pending orders");
                Console.WriteLine("- 4: Show finished orders");
                Console.WriteLine("- 5: Show capacity avaiable");
                Console.WriteLine("- 6: Show earnings and total orders");
                Console.WriteLine("- 7: **** PENDIENTE **** Edit office's Menu");
                Console.WriteLine("- 9: Go back");
                Console.Write("");
                string? option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CreateNewOrderMenu();
                        break;
                    case "2":
                        PrepareAllOrders();
                        break;
                    case "3":
                        currentOffice.ShowOrders(currentOffice.OrderQueue);
                        break;
                    case "4":
                        currentOffice.ShowOrders(currentOffice.FinishedOrderList);
                        break;
                    case "5":
                        ShowCapacity();
                        break;
                    case "6":
                        currentOffice.ShowResumenEarningsAndQuantity();
                        break;
                    case "7":
                        // EditOfficesMenu();
                        break;
                    case "9":
                        flag = false;
                        break;
                }
            }
            while (flag);
            Console.Write("");
        }
        public void ShowOfficeMenu()
        {
            bool flag = true;
            do
            {
                Console.WriteLine(" --------- WELCOME > Office List --------- ");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("- 1: Show office list");
                Console.WriteLine("- 2: Create office");
                Console.WriteLine("- 3: Current office");
                Console.WriteLine("- 9: Go back");
                Console.Write("");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        ShowOfficeListWithIndex();
                        break;
                    case "2":
                        CreateOfficceMenu();
                        break;
                    case "3":
                        ChooseOfficceMenu();
                        break;
                    case "9":
                        flag = false;
                        break;
                }
            }
            while (flag);
            Console.Write("");
        }
        #endregion

        // helpers with app
        #region 
        public Office GetCurrentOffice()
        {
            return _officeManager.currentOffice ?? throw new Exception("Error: not office selected");
        }
        public string GetCurrentOfficeName()
        {
            return GetCurrentOffice().Name;
        }
        public void CancelReservedUnits(Dictionary<string, int> breadsToAdd)
        {
            int reservedUnits = 0;
            foreach (KeyValuePair<string, int> bread in breadsToAdd)
            {
                reservedUnits += bread.Value;
            }
            GetCurrentOffice().CancelReservedUnits(reservedUnits);
        }
        #endregion

        // Consola only show methods
        #region
        public void ShowCurrentOfficeHeader()
        {
            Console.WriteLine("--------- Current Office ---------");
            Console.WriteLine("     " + GetCurrentOfficeName().ToUpper());
            Console.WriteLine("- - - - - - - - - - - - - - - - - -");
            Console.Write("");
        }
        public void ShowHistoricResumeForAllOffices()
        {
            foreach (Office office in _officeManager.OfficeList)
            {
                office.ShowCompleteListDetails();
            }
            Console.WriteLine(" ************** TOTALS **************");
            (int totalOrders, double totalEarnings) = _officeManager.CalculateTotals().Result;
            Console.WriteLine($"There was a total of {totalOrders} orders with an earned money of ${totalEarnings} us");
            Console.WriteLine();
        }
        public void ShowOfficeListWithIndex()
        {
            IEnumerable<Office> offices = _officeManager.OfficeList;
            Console.WriteLine("-- Office list: ");

            foreach (Office office in offices)
            {
                office.ShowCompleteListDetails();
            }
            Console.Write("");
        }
        public void ShowOptionsOfficeList()
        {
            IEnumerable<OfficeDto> offices = _officeManager.OfficeList.Select(office => office.ToDto());
            Console.WriteLine("-- Office list: ");

            int opt = 1;
            foreach (OfficeDto office in offices)
            {
                Console.WriteLine($"- Option: {opt} - {office.Name}");
                opt++;
            }
            Console.WriteLine("");
        }
        public void ShowListOfBreadsInMenuWithIndex()
        {
            Office office = GetCurrentOffice();
            Console.WriteLine("-- Breads in menu list: ");
            List<string> names = office.Menu.GetAvailableBreadsNames();
            int opt = 1;
            foreach (string name in names)
            {
                Console.WriteLine($"- Option: {opt} - {name}");
                opt++;
            }
            Console.WriteLine("");
        }
        public void ShowListDetailsBreadsToAdd(Dictionary<string, int> breadsToAdd)
        {
            Console.WriteLine($" --------- CURRENT ORDER PRODUCTS --------- ");
            foreach (KeyValuePair<string, int> bread in breadsToAdd)
            {
                Console.WriteLine($"** Bread: {bread.Key}, Quantity: {bread.Value}");
            }
        }
        public void PrepareAllOrders()
        {
            if (GetCurrentOffice().OrderQueue.Count() > 0)
            {
                Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()} > Preparing orders.... --------- ");
                GetCurrentOffice().PrepareAllOrders();
                Console.WriteLine("------------------------------");
                Console.WriteLine("-- All orders are prepared! --");
                Console.WriteLine("------------------------------");
            }
            else
            {
                Console.WriteLine($" There are no orders to prepare at the moment!");
            }
        }
        public void ShowCapacity()
        {
            Console.WriteLine($"***** At the moment we can only receive orders of up to {GetCurrentOffice().GetAvailableCapacity()} units ***** ");
        }
        #endregion

        // Consola menus with user input data
        #region
        public string MenuGetANameForANewOffice()
        {
            string name;
            bool flag = true;
            do
            {
                Console.Write("-- Write down a name for new office: ");
                name = Console.ReadLine();
                if (name != string.Empty)
                {
                    if (!_officeManager.ExistOfficeByName(name))
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.Write("--ERROR: There is already an office with that name! ");
                    }
                }
                else
                {
                    Console.Write("--ERROR: You can't put empty name! ");
                }
            } while (flag);
            return name;
        }
        public int MenuGetAMaximumCapacity()
        {
            int capacity;
            bool flag = true;
            do
            {
                Console.Write("--  Enter the maximum capacity of the office: ");
                string capacityInput = Console.ReadLine();

                if (!int.TryParse(capacityInput, out capacity) || capacity < 1)
                {
                    Console.Write("--ERROR:  You must enter a numeric value  greater than zero! ");
                }
                else
                {
                    flag = false;
                }
            } while (flag);
            return capacity;
        }
        public void CreateOfficceMenu()
        {
            Console.WriteLine("-- Lets create a office --");
            string name = MenuGetANameForANewOffice();
            int maxCapacity = MenuGetAMaximumCapacity();
            Office officeCreated = _officeManager.CreateEmptyOffice(name, maxCapacity).Result;

            if (officeCreated != null)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("-- Congrats, office created --");
                Console.WriteLine("------------------------------");
                officeCreated.ShowCompleteListDetails();
            }
            Console.Write("");
        }
        public void ChooseOfficceMenu()
        {
            OfficeDto? officeDto = SetCurrentOfficceMenu();
            if (officeDto != null)
            {
                CurrentOfficceMenu();
            }
        }
        public OfficeDto? SetCurrentOfficceMenu()
        {
            bool flag = true;
            OfficeDto officeDto = null;
            do
            {
                Console.WriteLine(" --------- WELCOME > Office List > choose an Office  --------- ");
                Console.WriteLine("Choose an office by option number , or write 'exit' to go back ");
                ShowOptionsOfficeList();
                Console.Write("Write down office number: ");

                string choosenOffice = Console.ReadLine();

                if (choosenOffice?.ToLower() == "exit") break;

                bool validNumber = int.TryParse(choosenOffice, out int choosenOfficeInt);

                if (!validNumber
                    || _officeManager.OfficeList.Count() < choosenOfficeInt
                    || 1 > choosenOfficeInt)
                {
                    Console.WriteLine("--ERROR: Invalid value!");
                }
                else
                {
                    officeDto = _officeManager.GetOfficeByIndex(choosenOfficeInt - 1).ToDto();
                    _officeManager.SetCurrentOfficeByName(officeDto.Name);
                    Console.WriteLine("Choosen office: " + officeDto.Name);
                    Console.WriteLine();
                    flag = false;
                }
            }
            while (flag);
            return officeDto;

        }
        public string ShowChooseBreadMenu()
        {
            bool flag = true;
            string breadName = null;
            do
            {
                Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()} > Create new order menu > Add new product to order > CHOOSE A BREAD --------- ");
                Console.WriteLine("Choose a bread by option number , or write 'exit' to go back ");
                ShowListOfBreadsInMenuWithIndex();
                Console.Write("Write down bread number: ");

                string choosenBread = Console.ReadLine();

                if (choosenBread?.ToLower() == "exit") break;

                bool validNumber = int.TryParse(choosenBread, out int choosenBreadInt);
                List<string> availableBreadsNames = GetCurrentOffice().Menu.GetAvailableBreadsNames();

                if (!validNumber
                    || availableBreadsNames.Count < choosenBreadInt
                    || 1 > choosenBreadInt)
                {
                    Console.WriteLine("--ERROR: Invalid value!");
                }
                else
                {
                    breadName = availableBreadsNames[choosenBreadInt - 1];
                    Console.WriteLine("Choosen bread: " + breadName);
                    flag = false;
                }
            }
            while (flag);
            return breadName;
        }
        public int ShowChooseQuantityMenu()
        {
            bool flag = true;
            int quantity = 0;
            int availablecapacity = GetCurrentOffice().GetAvailableCapacity();
            do
            {
                Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()} >  Create new order menu > Add new product to order > CHOOSE A QUANTITY --------- ");
                ShowCapacity();
                Console.Write("Write down bread number , or 'exit' to go back ");

                string choosenQuantity = Console.ReadLine();

                if (choosenQuantity?.ToLower() == "exit") break;

                bool validNumber = int.TryParse(choosenQuantity, out int choosenQuantityInt);

                if (!validNumber
                    || choosenQuantityInt > availablecapacity
                    || 1 > choosenQuantityInt)
                {
                    Console.WriteLine("--ERROR: Invalid value!");
                }
                else
                {
                    quantity = choosenQuantityInt;
                    GetCurrentOffice().AddReservedUnits(quantity);
                    Console.WriteLine("Choosen quantity: " + quantity);
                    flag = false;
                }
            }
            while (flag);
            return quantity;
        }
        public (string, int) SelectNewProductMenu()
        {
            if (GetCurrentOffice().GetAvailableCapacity() < 1)
            {
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine("--We do not have the capacity to receive more orders at this time. ! --");
                Console.WriteLine("-----------------------------------------------------------------------");
                return ("", 0);
            }

            Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()} > Create new order menu > Add new product to order --------- ");
            string breadName = ShowChooseBreadMenu();
            int breadQuantity = ShowChooseQuantityMenu();
            return (breadName, breadQuantity);
        }
        public bool ConfirmOrderMenu(Dictionary<string, int> breadsToAdd)
        {
            if (breadsToAdd.Count > 0)
            {
                bool flag = true;
                bool confirm = false;
                do
                {
                    Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()} > Create new order menu > Order confirmation --------- ");
                    ShowListDetailsBreadsToAdd(breadsToAdd);
                    Console.Write("---- Do you confirm the order? [Y - N] ");

                    string userResponse = Console.ReadLine();
                    if (userResponse.ToUpper().Equals("Y"))
                    {
                        flag = false;
                        confirm = true;
                        CancelReservedUnits(breadsToAdd);
                    }
                    else if (userResponse.ToUpper().Equals("N"))
                    {
                        flag = false;
                        CancelReservedUnits(breadsToAdd);
                    }
                    else
                    {
                        Console.WriteLine("--ERROR: Invalid input ");
                    }
                } while (flag);
                return confirm;

            }
            return false;
        }

        #endregion

        public void CreateNewOrderMenu()
        {
            Dictionary<string, int> breadsToAdd = new Dictionary<string, int>();

            bool flag = true;
            do
            {
                Console.WriteLine($" --------- WELCOME > Office List > Current Office: {GetCurrentOfficeName()} > Create new order menu --------- ");
                Console.Write("Write down option number , or 'exit' to go back ");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("- 1: Add new product");
                Console.WriteLine("- 2: **** PENDIENTE **** Update product quantity");
                Console.WriteLine("- 3: **** PENDIENTE **** Delete product");
                Console.WriteLine("- 4: Confirm new order");

                string option = Console.ReadLine();
                if (option?.ToLower() == "exit") break;

                (string, int) breadToAdd = ("", 0);
                bool confirmedOrder = false;

                switch (option)
                {
                    case "1":
                        breadToAdd = SelectNewProductMenu();
                        break;
                    case "4":
                        confirmedOrder = ConfirmOrderMenu(breadsToAdd);
                        break;
                }

                if (breadToAdd.Item1 != "")
                {
                    if (breadsToAdd.ContainsKey(breadToAdd.Item1))
                    {
                        breadsToAdd.TryGetValue(breadToAdd.Item1, out int value);
                        breadsToAdd[breadToAdd.Item1] = value + breadToAdd.Item2;
                    }
                    else
                    {
                        breadsToAdd.Add(breadToAdd.Item1, breadToAdd.Item2);
                    }
                }
                if (confirmedOrder && breadsToAdd.Count > 0)
                {
                    bool status = _officeManager.CreateNewOrder(breadsToAdd);
                    if (status)
                    {
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("-- Congrats, new order created ! --");
                        Console.WriteLine("-----------------------------------");
                        flag = false;
                    }
                }
            }
            while (flag);
        }
    }
}
