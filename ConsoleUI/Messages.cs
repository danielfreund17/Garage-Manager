using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.ConsoleUI
{
    public static class Messages
    {
        public static string s_AskForLicense = "Please write your license plate:";
        public static string s_AskForModel = "Please write the model: ";
        public static string s_AskForName = "Please write the name of the owner: ";
        public static string s_AskForPhoneNumebr = "Please write the phone number of the owner";
        public static string s_AskForWheelManufacturer = "Please write the manufacturer name of wheel number {0}";
        public static string s_AskForAllWheels = "Please write the manufacturer name of all wheels:";
        public static string s_VehicleExit = "The vehicle is already in the garage!";
        public static string s_VehicleNotExit = "The vehicle does not exsist in the garage!";
        public static string s_NotValidInput = "Please write a valid input:";
        public static string s_ValueOutOfRange = "Value is out of range!";
        public static string s_ExitMessage = "Thanks for using Garage Appliction!";
        public static string s_AskVehicleType = "Please choose the type of the vehicle:";
        public static string s_AddToGarage = "Your {0} has been added to the garage successfully!";
        public static string s_GetcurrentAirPressure = "Please insert the current air pressure of the wheel:";
        public static string s_GetAmountToAdd = "Please enter the amount to add:";
        public static string s_AddedGas = "Gas been successfully filled!";
        public static string s_ChooseBetweenTwoNumbers = "Please select between {0} to {1}";
        public static string s_AskForFuelType = "Please choose one of the following fuel types:";
        public static string s_AskByStatus = "Would you like to get licences by status?";
        public static string s_Yes = "Yes";
        public static string s_No = "No";
        public static string s_PressToContinue = "Press any key to continue...";
        public static string s_EmptyList = "Information list is empty!";
        public static string s_GetNewStatus = "What is the new status for your vehicle?";
        public static string s_StatusChanged = "The status has been change successfully!";
        public static string s_FilledWheels = "Wheels been successfully filled!";
        public static string s_AddedMinuts = "Minutes charged successfully!";
        public static string s_HowToSetWheel = "Does all the wheels the same?";
        public static string s_Maximum = "Current amount is at the possible maximum!";
        public static string s_ErrorMessage = string.Format(
@"=======================================
     {0}
=======================================",
Messages.s_NotValidInput);

        public static string s_MenuMsg = string.Format(@"
            Welcome to the Garage Manager!
                 Best garage in town
             ---made by Daniel and Idan---
       Please choose one of the following options

====================================================================
Menu:
1. Add new customer to the garage.
2. Get all the lincences inside the garage.
3. Change status of existing customer.
4. Blow the wheels of a specific veihcle to maximum.
5. Refuel vehicle with gas engine.
6. Recharge vehicle with electrical engine.
7. Get all information of specific vehicle by it's license number.
8. Exit application.
====================================================================");
    }
}
