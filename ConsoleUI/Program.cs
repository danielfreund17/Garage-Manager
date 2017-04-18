using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            Garage garage = new Garage();
            ConsoleManagerUI garageManager = new ConsoleManagerUI(garage);
            garageManager.RunGarrage();
        }
    }
}
