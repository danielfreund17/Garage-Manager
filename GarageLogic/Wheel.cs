using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private const float k_MinAirPressure = 0;
        private readonly float r_MaxAirPressure;
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public Wheel ShalowClone()
        {
            Wheel newWheel = new Wheel(this.ManufacturerName, this.r_MaxAirPressure);
            newWheel.m_CurrentAirPressure = this.m_CurrentAirPressure;
            return newWheel;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set
            {
                if(value > r_MaxAirPressure)
                {
                    string msg = "Overloading air to wheel!";
                    throw new ValueOutOfRangeException(msg, k_MinAirPressure, r_MaxAirPressure);
                }

                m_CurrentAirPressure = value;
            }
        }

        public string ManufacturerName
        {
            get { return m_ManufacturerName; }
            set { m_ManufacturerName = value; }
        }

        public void BlowWheel(float i_AirToAdd)
        {
            if (i_AirToAdd + m_CurrentAirPressure <= r_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirToAdd;
            }
            else
            {
                string msg = "Pressure added to the wheels is out of range!";
                throw new ValueOutOfRangeException(msg, k_MinAirPressure, r_MaxAirPressure);
            }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }
    }
}
