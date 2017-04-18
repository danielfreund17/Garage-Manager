using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : EnergySource
    {
        public ElectricEngine(float i_MaxOfBattary)
        : base(i_MaxOfBattary)
        {
        }

        public override string ToString()
        {
            string msg = string.Format(
@"There is {0} hours of battary left until shut down 
The current percentage of energy is {1}%",
                this.m_CurrentEnergy, 
                this.m_PercentageOfEnergy);
            return msg;        
        }
    }
}
