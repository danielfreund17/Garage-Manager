using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public enum eMenuOptions
    {
        AddCustomer = 1,
        getLicenses,
        ChangeCustomerStatus,
        BlowWheelsToMaximum,
        RefuelGasEngine,
        RechargeElectricEngine,
        GetDetailsOfCustomer,
        Exit
    }

    public class ConsoleManagerUI
    {
        private const int k_Empty = 0;
        private const int k_SleepTime = 2000;
        private const int k_NumberOfMenuOptions = 8;
        private const int k_MinOptionAviable = 1;
        private readonly int r_NumberOfTypesAvailable;
        private Garage m_Garage;
        private VehicleCreation m_VehicleCreator;
        private ReadOnlyCollection<string> m_AvailableVehicleTypes;

        public ConsoleManagerUI(Garage i_Garage)
        {
            m_Garage = i_Garage;
            m_VehicleCreator = new VehicleCreation();
            m_AvailableVehicleTypes = m_VehicleCreator.TypesOfVehicles;
            r_NumberOfTypesAvailable = m_AvailableVehicleTypes.Count;
        }

        public void RunGarrage()
        {
            bool exitProgram = false;
            while (!exitProgram)
            {
                exitProgram = getUserOrder();
            }

            exitApplication();
        }

        public bool getUserOrder()
        {
            eMenuOptions userChoice = getUserMenuChoice();
            bool userWantsToQuit = false;
            switch (userChoice)
            {
                case eMenuOptions.AddCustomer:
                    Console.Clear();
                    addCustomerToGarage();
                    break;
                case eMenuOptions.BlowWheelsToMaximum:
                    Console.Clear();
                    blowWheelsToMaximum();
                    break;
                case eMenuOptions.ChangeCustomerStatus:
                    Console.Clear();
                    changeCustomerStatus();
                    break;
                case eMenuOptions.getLicenses:
                    Console.Clear();
                    getLicenses();
                    break;
                case eMenuOptions.GetDetailsOfCustomer:
                    Console.Clear();
                    getDetailsOfCustomer();
                    break;
                case eMenuOptions.RechargeElectricEngine:
                    Console.Clear();
                    refuelEngine(eEngineTypes.Electrical);
                    break;
                case eMenuOptions.RefuelGasEngine:
                    Console.Clear();
                    refuelEngine(eEngineTypes.Gas);
                    break;
                case eMenuOptions.Exit:
                    userWantsToQuit = true;
                    break;
            }

            return userWantsToQuit;
        }

        private eMenuOptions getUserMenuChoice()
        {
            string userInput;
            bool inputValidation = false;
            int ChoiceOfUser = 0;
            Console.Clear();
            while (!inputValidation)
            {
                displayMenu();
                userInput = Console.ReadLine();
                inputValidation = checkInput(userInput, k_MinOptionAviable, k_NumberOfMenuOptions, out ChoiceOfUser);
            }

            return (eMenuOptions)ChoiceOfUser;
        }

        private void displayMenu()
        {
            Console.Clear();
            Console.WriteLine(Messages.s_MenuMsg);
            Console.WriteLine(Messages.s_ChooseBetweenTwoNumbers, k_MinOptionAviable, k_NumberOfMenuOptions);
        }

        private void validationError()
        {
            Console.Clear();
            Console.WriteLine(Messages.s_ErrorMessage);
            Thread.Sleep(k_SleepTime);
            Console.Clear();
        }

        private bool checkInput(string i_userInput, int k_MinOption, int k_MaxOption, out int i_ChoiceOfUser)
        {
            bool inputValidation = true;
            i_ChoiceOfUser = k_MinOption;
            try
            {
                i_ChoiceOfUser = int.Parse(i_userInput);
                if (!(i_ChoiceOfUser >= k_MinOption && i_ChoiceOfUser <= k_MaxOption))
                {
                    inputValidation = false;
                }
            }
            catch (FormatException)
            {
                inputValidation = false;
            }

            if(!inputValidation)
            {
                validationError();
            }

            return inputValidation;
        }

        private void exitApplication()
        {
            Console.Clear();
            Console.Write("Thanks for using Garage Appliction!" + Environment.NewLine);
            Thread.Sleep(k_SleepTime);
            Environment.Exit(0);
        }

        private void addCustomerToGarage()
        {
            Vehicle newVehicleToAdd;
            eVehicleTypes vehicleType;
            VehicleInside NewCustomerToAdd;
            string choiceOfUser;
            choiceOfUser = chooseOption(typeof(eVehicleTypes), Messages.s_AskVehicleType);
            vehicleType = (eVehicleTypes)Enum.Parse(typeof(eVehicleTypes), choiceOfUser);
            newVehicleToAdd = setGeneralInfo(vehicleType); // get general info- each vehicle must have.
            if (newVehicleToAdd != null)
            {
                setInfoByType(newVehicleToAdd); // get specific information by type. (GetQuestions)
                setOwnerInfo(newVehicleToAdd, out NewCustomerToAdd);
                m_Garage.AddCustomer(NewCustomerToAdd);
                Console.Clear();
                Console.WriteLine(Messages.s_AddToGarage, newVehicleToAdd.GetType().Name);
                Thread.Sleep(k_SleepTime);
            }
        }

        private bool getGeneralAnswers(string i_Question, out string i_Answer, bool i_isLicenseQuestion)
        {
            bool isExist = false;
            Console.Clear();
            Console.WriteLine(i_Question);
            i_Answer = Console.ReadLine();
            while (i_Answer == string.Empty)
            {
                Console.WriteLine(Messages.s_NotValidInput);
                i_Answer = Console.ReadLine();
            }

            if (i_isLicenseQuestion && m_Garage.IsVehicleExist(i_Answer))
            {
                Console.WriteLine(Messages.s_VehicleExit);
                isExist = true;
                Thread.Sleep(k_SleepTime);
            }

            return isExist;
        }

        private Vehicle setGeneralInfo(eVehicleTypes i_VehicleType)
        {
            Console.Clear();
            Vehicle newVehicleToCreat = null;
            bool isExist;
            bool isLicenseQuestion = true;
            string ModelName, LicensePlate;
            isExist = getGeneralAnswers(Messages.s_AskForLicense, out LicensePlate, isLicenseQuestion);
            if (!isExist)
            {
                isLicenseQuestion = false;
                getGeneralAnswers(Messages.s_AskForModel, out ModelName, isLicenseQuestion);
                Console.Clear();
                newVehicleToCreat = m_VehicleCreator.CreatVehicle(i_VehicleType, ModelName, LicensePlate);
                setWheelsInfo(newVehicleToCreat);
            }

            Console.Clear();
            return newVehicleToCreat;
        }
       
        private void setWheelsInfo(Vehicle io_Vehicle)
        {
            const int setEachWheel = 1;
            int userChoise = yesNoQuestions(Messages.s_HowToSetWheel);
            if(userChoise == setEachWheel)
            {
                setEveryWheels(io_Vehicle);
            }
            else
            {
                setAllWheels(io_Vehicle);
            }
        }

        private void setEveryWheels(Vehicle io_Vehicle)
        {
            Wheel newWheelToAdd;
            string manufacturer;
            for (int i = 1; i <= io_Vehicle.NumberOfWheels; i++)
            {
                Console.WriteLine(Messages.s_AskForWheelManufacturer, i);
                manufacturer = Console.ReadLine();
                newWheelToAdd = new Wheel(manufacturer, io_Vehicle.MaxAirInWheel);
                setWheel(newWheelToAdd);
                io_Vehicle.Wheels.Add(newWheelToAdd);
            }
        }

        private void setAllWheels(Vehicle io_Vehicle)
        {
            Wheel newWheelToAdd;
            Wheel newWheel;
            Console.WriteLine(Messages.s_AskForAllWheels);
            string manufacturer = Console.ReadLine();
            newWheelToAdd = new Wheel(manufacturer, io_Vehicle.MaxAirInWheel);
            setWheel(newWheelToAdd);
            for (int i = 1; i <= io_Vehicle.NumberOfWheels; i++)
            {
                newWheel = newWheelToAdd.ShalowClone();
                io_Vehicle.Wheels.Add(newWheel);
            }
        }
       
        private void setWheel(Wheel i_NewWheel)
        {
            bool validAirPressure = false;
            float currentAirPressure = k_Empty;
            string userInput;
            while (!validAirPressure)
            {
                try
                {
                    Console.WriteLine(Messages.s_GetcurrentAirPressure);
                    userInput = Console.ReadLine();
                    currentAirPressure = float.Parse(userInput);
                    if (currentAirPressure < k_Empty)
                    {
                        validAirPressure = false;
                    }
                    else
                    {
                        i_NewWheel.CurrentAirPressure = currentAirPressure;
                        validAirPressure = true;
                        Console.Clear();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine(Messages.s_NotValidInput);
                }
                catch (ValueOutOfRangeException i_Voore)
                {
                    Console.WriteLine(i_Voore.Message);
                    if(i_Voore.MaxValue == k_Empty)
                    {
                        Console.WriteLine(Messages.s_Maximum);
                    }
                    else
                    {
                        Console.WriteLine(Messages.s_ChooseBetweenTwoNumbers, i_Voore.MinValue, i_Voore.MaxValue);
                    }                    
                }
            }
        }

        private void setOwnerInfo(Vehicle io_Vehicle, out VehicleInside io_VehicleInside)
        {
            bool isLicenseQuestion = false;
            string ownerName;
            string ownerPhoneNumber;
            getGeneralAnswers(Messages.s_AskForName, out ownerName, isLicenseQuestion);
            getGeneralAnswers(Messages.s_AskForPhoneNumebr, out ownerPhoneNumber, isLicenseQuestion);
            io_VehicleInside = new VehicleInside(ownerName, ownerPhoneNumber, io_Vehicle);
        }

        private void setInfoByType(Vehicle io_Vehicle)
        {         
            int questionIndex = 1;
            ReadOnlyCollection<Question> questionsToAsk;
            questionsToAsk = io_Vehicle.GetQuestions;
            foreach (Question currentQuestion in questionsToAsk)
            {
                setUniqueInfoByQuestion(currentQuestion, questionIndex, io_Vehicle);
                questionIndex++;
            }
        }

        private void setUniqueInfoByQuestion(Question i_Question, int i_QuestionIndex, Vehicle io_Vehicle)
        {
            string userAnswer;
            string answerToHandle;
            bool badAnswer = true;
            while (badAnswer)
            {
                try
                {
                    Console.WriteLine(i_Question.QuestionToAsk);
                    if (i_Question.Options != null)
                    {
                        foreach (string option in i_Question.Options)
                        {
                            Console.WriteLine(option);
                        }
                    }

                    userAnswer = Console.ReadLine();
                    answerToHandle = getUserAnswer(i_Question, userAnswer);
                    io_Vehicle.InsertAnswer(answerToHandle, i_QuestionIndex);
                    badAnswer = false;
                }
                catch (ValueOutOfRangeException i_Voore)
                {
                    Console.WriteLine(i_Voore.Message);
                    if (i_Voore.MaxValue == k_Empty)
                    {
                        Console.WriteLine(Messages.s_Maximum);
                    }
                    else
                    {
                        Console.WriteLine(Messages.s_ChooseBetweenTwoNumbers, i_Voore.MinValue, i_Voore.MaxValue);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine(Messages.s_NotValidInput);
                }
            }
        }

        private string getUserAnswer(Question i_Question, string i_Answer)
        {
            int userChoise = 0;
            bool GoodChoise = false;
            string choiceOfUser = string.Empty;
            bool res = int.TryParse(i_Answer, out userChoise);
            while (!GoodChoise)
            {
                if (i_Question.MaxOption == k_Empty)
                {
                    choiceOfUser = i_Answer;
                    break;
                }
                else if (res && userChoise >= 1 && userChoise <= i_Question.MaxOption)
                {
                    choiceOfUser = i_Question.EnumNames[userChoise - 1];
                    break;
                }

                Console.WriteLine(Messages.s_NotValidInput);
                i_Answer = Console.ReadLine();
                res = int.TryParse(i_Answer, out userChoise);
            }

            return choiceOfUser;
        }

        private void refuelEngine(eEngineTypes i_EngineType)
        {
            bool isInputValid = false;
            string licensePlate;
            string userInput;
            float amountToAdd = 0;
            Console.WriteLine(Messages.s_AskForLicense);
            licensePlate = Console.ReadLine();
            Console.WriteLine(Messages.s_GetAmountToAdd);
            userInput = Console.ReadLine();
            isInputValid = float.TryParse(userInput, out amountToAdd);
            while (!isInputValid || amountToAdd <= k_Empty)
            {
                Console.WriteLine(Messages.s_NotValidInput);
                userInput = Console.ReadLine();
                isInputValid = float.TryParse(userInput, out amountToAdd);
            }

            refuelEngineAUX(i_EngineType, licensePlate, amountToAdd);     
        }

        private void refuelEngineAUX(eEngineTypes i_EngineType, string i_LicensePlate, float i_AmountToAdd)
        {
            if(i_EngineType == eEngineTypes.Electrical)
            {
                rechargeElectricEngine(i_LicensePlate, i_AmountToAdd);
            }
            else
            {
                refuelGasEngine(i_LicensePlate, i_AmountToAdd);
            }
        }

        private void refuelGasEngine(string i_LicenseNumber, float i_LittersToAdd)
        {
            eFuelType chosenFuel;
            string userChoice;    
            userChoice = chooseOption(typeof(eFuelType), Messages.s_AskForFuelType);
            chosenFuel = (eFuelType)Enum.Parse(typeof(eFuelType), userChoice);
            try
            {
                m_Garage.RefuelGasEngine(i_LicenseNumber, chosenFuel, i_LittersToAdd);
                Console.WriteLine(Messages.s_AddedGas);
            }
            catch (ArgumentException i_Ae)
            {
                Console.WriteLine(i_Ae.Message);
            }
            catch (ValueOutOfRangeException i_Voore)
            {
                Console.WriteLine(i_Voore.Message);
                if (i_Voore.MaxValue == k_Empty)
                {
                    Console.WriteLine(Messages.s_Maximum);
                }
                else
                {
                    Console.WriteLine(Messages.s_ChooseBetweenTwoNumbers, i_Voore.MinValue, i_Voore.MaxValue);
                }
            }
            finally
            {
                Thread.Sleep(k_SleepTime);
            }
        }

        private void rechargeElectricEngine(string i_LicenseNumber, float i_MinutesToAdd)
        {
            try
            {
                m_Garage.RechargeElectricEngine(i_LicenseNumber, i_MinutesToAdd);
                Console.WriteLine(Messages.s_AddedMinuts);
            }
            catch(ArgumentException i_Ae)
            {
                Console.WriteLine(i_Ae.Message);
            }
            catch(ValueOutOfRangeException i_Voore)
            {
                Console.WriteLine(i_Voore.Message);
                if (i_Voore.MaxValue == k_Empty)
                {
                    Console.WriteLine(Messages.s_Maximum);
                }
                else
                {
                    Console.WriteLine(Messages.s_ChooseBetweenTwoNumbers, i_Voore.MinValue, i_Voore.MaxValue);
                }
            }
            finally
            {
                Thread.Sleep(k_SleepTime);
            }
        }

        private void getLicenses()
        {
            const int allLicenses = 1;
            int choiceOfUser = yesNoQuestions(Messages.s_AskByStatus);
            List<string> listOfLicences = null;
            if (choiceOfUser == allLicenses)
            {
                listOfLicences = m_Garage.GetAllLicences();
            }
            else
            {
                listOfLicences = getLicensesByStatus();
            }

            printInformation(listOfLicences);
            Console.WriteLine(Messages.s_PressToContinue);
            Console.ReadKey();
        }

        private List<string> getLicensesByStatus()
        {
            string userChoice;
            userChoice = chooseOption(typeof(eVehicleStatus), Messages.s_GetNewStatus);
            eVehicleStatus vehicleStatusChosen;
            vehicleStatusChosen = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), userChoice);
            return m_Garage.GetLicensesByStatus(vehicleStatusChosen);
        }

        private void getDetailsOfCustomer()
        {
            string licensePlate;
            List<string> detailsOfCustomer;
            Console.WriteLine(Messages.s_AskForLicense);
            licensePlate = Console.ReadLine();
            try
            {
                detailsOfCustomer = m_Garage.GetDetailsOfCustomer(licensePlate);
                printInformation(detailsOfCustomer);        
            }
            catch(ArgumentException i_Ae)
            {
                Console.WriteLine(i_Ae.Message);
            }
            finally
            {
                Console.WriteLine(Messages.s_PressToContinue);
                Console.ReadKey();
            }
        }
           
        private void changeCustomerStatus()
        {
            string choiceOfUser;
            eVehicleStatus ChosenStatus;
            Console.WriteLine(Messages.s_AskForLicense);
            string userLicense = Console.ReadLine();
            choiceOfUser = chooseOption(typeof(eVehicleStatus), Messages.s_GetNewStatus);
            ChosenStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), choiceOfUser);            

            try
            {
                m_Garage.ChangeCustomerStatus(userLicense, ChosenStatus);
                Console.WriteLine(Messages.s_StatusChanged);
            }
            catch(ArgumentException i_Ae)
            {
                Console.WriteLine(i_Ae.Message);
            }
            finally
            {
                Thread.Sleep(k_SleepTime);
            }
        }

        private void blowWheelsToMaximum()
        {
            Console.Clear();
            Console.WriteLine(Messages.s_AskForLicense);
            string userLicensePlate = Console.ReadLine();
            try
            {
                m_Garage.BlowWheelsToMaximum(userLicensePlate);
                Console.WriteLine(Messages.s_FilledWheels);
            }
            catch(ArgumentException i_Ae)
            {
                Console.WriteLine(i_Ae.Message);
            }
            finally
            {
                Thread.Sleep(k_SleepTime);
            }          
        }

        private int yesNoQuestions(string i_Msg)
        {
            const int yes = 2, no = 1;
            int choiceOfUser = 0;
            string userInput;
            bool inputValidation = false;
            while (!inputValidation)
            {
                Console.WriteLine(i_Msg);
                Console.WriteLine("1. " + Messages.s_No);
                Console.WriteLine("2. " + Messages.s_Yes);
                userInput = Console.ReadLine();
                inputValidation = checkInput(userInput, no, yes, out choiceOfUser);
            }

            return choiceOfUser;
        }

        private void printInformation(List<string> i_ListOfDetails)
        {
            Console.Clear();
            if (i_ListOfDetails.Count == k_Empty)
            {
                Console.WriteLine(Messages.s_EmptyList);
            }
            else
            {
                foreach (string str in i_ListOfDetails)
                {
                    Console.WriteLine(str);
                }
            }
        }

        private string chooseOption(Type i_EnumType, string i_Msg)
        {
            int UserChoice = 0;
            int indexer = 1;
            string userInput;
            bool inputValidation = false;
            string[] possibleOptions = Enum.GetNames(i_EnumType);
            int numOfOptions = possibleOptions.Length;

            while (!inputValidation)
            {
                Console.WriteLine(i_Msg);
                foreach (string status in possibleOptions)
                {
                    Console.WriteLine("{0}. {1}", indexer++, status);
                }

                userInput = Console.ReadLine();
                inputValidation = checkInput(userInput, k_MinOptionAviable, numOfOptions, out UserChoice);
                indexer = 1;
            }

            return possibleOptions[UserChoice - 1];
        }
    }
}
