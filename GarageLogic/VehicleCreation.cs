using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Ex03.GarageLogic
{
    public enum eVehicleTypes
    {
        Car = 1,
        Motorcycle,
        Truck
    }

    public class VehicleCreation
    {
        private string[] m_AviableTypes;

        public VehicleCreation()
        {
            m_AviableTypes = Enum.GetNames(typeof(eVehicleTypes));

            // possible to add new vehicles to the system
        }

        public ReadOnlyCollection<string> TypesOfVehicles
        {
            get { return Array.AsReadOnly<string>(m_AviableTypes); }
        }

        public Vehicle CreatVehicle(eVehicleTypes i_TypeOfVehicle, string i_ModelName, string i_LicencePlate)
        {
            Vehicle vehicleToCreat = null;
            switch (i_TypeOfVehicle)
            {
                case eVehicleTypes.Car:
                    vehicleToCreat = new Car(i_ModelName, i_LicencePlate);
                    break;
                case eVehicleTypes.Motorcycle:
                    vehicleToCreat = new Motorcycle(i_ModelName, i_LicencePlate);
                    break;
                case eVehicleTypes.Truck:
                    vehicleToCreat = new Truck(i_ModelName, i_LicencePlate);
                    break;
            }

            return vehicleToCreat;
        }
    }
}
