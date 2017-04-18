using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        /// <summary>
        /// mapping Licence of car to VehicleInside (vehicle in the garage).
        /// </summary>
        private Dictionary<string, VehicleInside> m_Customers;

        public Garage()
        {
            m_Customers = new Dictionary<string, VehicleInside>();
        }

        public void AddCustomer(VehicleInside i_CustomerToAdd)
        {
            m_Customers.Add(i_CustomerToAdd.Vehicle.LicensePlate, i_CustomerToAdd);
        }

        public bool IsVehicleExist(string i_LicensePlate)
        {
            bool vehicleExist = false;
            if (m_Customers.ContainsKey(i_LicensePlate))
            {
                vehicleExist = true;
                m_Customers[i_LicensePlate].VehicleStatus = eVehicleStatus.InRepair;       
            }

            return vehicleExist;
        }

        public List<string> GetAllLicences()
        {
            List<string> listOfAll = new List<string>();
            foreach (KeyValuePair<string, VehicleInside> customer in m_Customers)
            {
                listOfAll.Add(customer.Value.Vehicle.LicensePlate);
            }

            return listOfAll;
        }

        public List<string> GetLicensesByStatus(eVehicleStatus i_VehicleStatus)
        {
            List<string> listOfLicences = new List<string>();
            foreach (KeyValuePair<string, VehicleInside> customer in m_Customers)
            {
                if (i_VehicleStatus == customer.Value.VehicleStatus)
                {
                    listOfLicences.Add(customer.Value.Vehicle.LicensePlate);
                }
            }

            return listOfLicences;
        }

        public void ChangeCustomerStatus(string i_CarLicence, eVehicleStatus i_NewStatus)
        {
            VehicleInside vehicleToUpdate;
            if (!m_Customers.TryGetValue(i_CarLicence, out vehicleToUpdate))
            {
                throw new ArgumentException("Vehicle doesn't exsists");
            }
            else
            {
                if (i_NewStatus != vehicleToUpdate.VehicleStatus)
                {
                    vehicleToUpdate.VehicleStatus = i_NewStatus;
                }
                else
                {
                    string VehicleName = vehicleToUpdate.Vehicle.GetType().Name;
                    throw new ArgumentException(
                        string.Format(
                        "The {0}'s status is already {1}.",
                        VehicleName,
                        i_NewStatus.ToString()));
                }
            }
        }

        public void BlowWheelsToMaximum(string i_CarLicence)
        {
            VehicleInside vehicleToUpdate;
            if (m_Customers.TryGetValue(i_CarLicence, out vehicleToUpdate))
            {
                List<Wheel> wheelsToBlow;
                float maxAirInWheel = vehicleToUpdate.Vehicle.MaxAirInWheel;
                wheelsToBlow = vehicleToUpdate.Vehicle.Wheels;
                foreach (Wheel wheel in wheelsToBlow)
                {
                    wheel.CurrentAirPressure = maxAirInWheel;
                }
            }
            else
            {
                throw new ArgumentException("Vehicle does not exsist");
            }
        }

        public void RefuelGasEngine(string i_CarLicence, eFuelType i_FuelType, float i_LittersToAdd)
        {
            VehicleInside vehicleToUpdate;
            eFuelType fuelType;

            if (m_Customers.TryGetValue(i_CarLicence, out vehicleToUpdate))
            {
                string vehicleName = vehicleToUpdate.Vehicle.GetType().Name;
                try
                {
                    fuelType = vehicleToUpdate.Vehicle.Energy.FuelType; // to catch: ArgumentException(not a gas vehicle)
                }
                catch (ArgumentException i_ArgumentException)
                {
                    throw new ArgumentException(
                        string.Format(
                        "The {0} does not have gas engine", vehicleName),
                        i_ArgumentException);
                }

                if (fuelType != i_FuelType)
                {
                    throw new ArgumentException(
                        string.Format(
                        "The gas you wanted to add does not match with the {0}",
                        vehicleName));
                }
                else
                {
                    vehicleToUpdate.Vehicle.Energy.AddEnergy(i_LittersToAdd); // to catch: ArgumentException(overload tank)
                }
            }
            else
            {
                throw new ArgumentException("Vehicle doesn't exsists");
            }
        }

        public void RechargeElectricEngine(string i_CarLicence, float i_MinutesToAdd)
        {
            VehicleInside vehicleToUpdate;
            if (m_Customers.TryGetValue(i_CarLicence, out vehicleToUpdate))
            {
                if (vehicleToUpdate.Vehicle.Energy is ElectricEngine)
                {
                    vehicleToUpdate.Vehicle.Energy.AddEnergy(i_MinutesToAdd); // to catch: ArgumentException(overload adding)
                }
                else
                {
                    string vehicleName = vehicleToUpdate.Vehicle.GetType().Name;
                    throw new ArgumentException(
                        string.Format(
                        "The {0} does not have gas engine",
                        vehicleName));
                }
            }
            else
            {
                throw new ArgumentException("Vehicle doesn't exsists");
            }
        }

        public List<string> GetDetailsOfCustomer(string i_CarLicence)
        {
            VehicleInside customer;
            if (m_Customers.TryGetValue(i_CarLicence, out customer))
            {
                List<string> detailsList = new List<string>();
                getGeneralInfo(detailsList, customer);
                getWheelsInfo(detailsList, customer);
                getEnergyInfo(detailsList, customer);
                getUniqueInfo(detailsList, customer);

                return detailsList;
            }
            else
            {
                throw new ArgumentException("Vehicle does not exist");
            }            
        }

        private void getGeneralInfo(List<string> o_DetailsList, VehicleInside o_Customer)
        {
            string vehicleType = o_Customer.Vehicle.GetType().Name;
            o_DetailsList.Add("Owner name is: " + o_Customer.OwnerName);
            o_DetailsList.Add("Phone Number of owner is: " + o_Customer.OwnerPhoneNumber);
            o_DetailsList.Add("The type of the vehicle is: " + vehicleType);
            o_DetailsList.Add("The name of the model is: " + o_Customer.Vehicle.ModelName);
            o_DetailsList.Add(
                string.Format(
                "The {0} has {1} wheels ",
                vehicleType,
                o_Customer.Vehicle.NumberOfWheels));
            o_DetailsList.Add(string.Format(
                "The status of the {0} in the garage is: {1}",
                vehicleType,
                o_Customer.VehicleStatus));
        } 
        
        private void getWheelsInfo(List<string> o_DetailsList, VehicleInside o_Customer)
        {
            List<Wheel> listOfWheels = o_Customer.Vehicle.Wheels;
            int counter = 1;
            foreach (Wheel wheel in listOfWheels)
            {
                string msg;
                string manufacturer = wheel.ManufacturerName;
                float airInWheel = wheel.CurrentAirPressure;
                msg = string.Format(
                    "Wheel number {0} have {1} air pressure and was made by {2} company",
                    counter,
                    airInWheel,
                    manufacturer);
                o_DetailsList.Add(msg);
                counter++;
            }
        } 
        
        private void getEnergyInfo(List<string> o_DetailsList, VehicleInside o_Customer)
        {
            o_DetailsList.Add(string.Format(o_Customer.Vehicle.Energy.ToString())); // Energy Info polymorphic
        }

        private void getUniqueInfo(List<string> o_DetailsList, VehicleInside o_Customer)
        {
            List<string> uniqueInfoOfVehicle = o_Customer.Vehicle.GetUniqueInfo(); // Polimorphic info list
            foreach (string str in uniqueInfoOfVehicle)
            { // Get specific info of vehicle by it's type.
                o_DetailsList.Add(str);
            }
        }
    }
}
