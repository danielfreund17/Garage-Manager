using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
        InRepair = 1,
        Repaired,
        Paid,
    }

    public class VehicleInside
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_VehicleStatus;

        public VehicleInside(string i_OwnerName, string i_PhoneNumber, Vehicle i_Vehicle)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_PhoneNumber;
            m_Vehicle = i_Vehicle;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }
        
        public string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }
    }
}
