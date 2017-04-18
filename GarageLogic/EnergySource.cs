using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eEngineTypes
    {
        Gas = 1,
        Electrical
    }

    public abstract class EnergySource
    {
        private const float k_MinAirEnergy = 0;
        protected float m_CurrentEnergy = 0;
        protected float m_PercentageOfEnergy = 0;
        protected float m_MaxEnergy;

        public EnergySource(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
        }

        public float PercentageOfEnergy
        {
            get { return m_PercentageOfEnergy; }
        }

        public void AddEnergy(float i_EnergyToAdd)
        {
            if(i_EnergyToAdd + m_CurrentEnergy <= m_MaxEnergy)
            {
                m_CurrentEnergy += i_EnergyToAdd;
                m_PercentageOfEnergy = m_CurrentEnergy / m_MaxEnergy * 100;
            }
            else
            {
                string msg = "Overloading energy!";
                throw new ValueOutOfRangeException(msg, k_MinAirEnergy, m_MaxEnergy - m_CurrentEnergy);
            }
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }

            set
            { 
                if(value > m_MaxEnergy)
                {
                    string msg = "Overloading energy to vehicle!";
                    throw new ValueOutOfRangeException(msg, k_MinAirEnergy, m_MaxEnergy);
                }
                else
                {
                    m_CurrentEnergy = value;
                    m_PercentageOfEnergy = m_CurrentEnergy / m_MaxEnergy * 100;
                }
            }
        }

        public float MaxEnergy
        {
            get { return m_MaxEnergy; }
            set { m_MaxEnergy = value; }
        }
        
        public eFuelType FuelType
        {
            get
            {
                GasEngine tempGasEngine = this as GasEngine;
                if(tempGasEngine != null)
                {
                    return tempGasEngine.GasType;
                } 
                else
                {
                    string msg = "This vehicle has no gas engine!";
                    throw new ArgumentException(msg);
                }
            }
        }       
    }
}
