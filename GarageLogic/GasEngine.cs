using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Octan95,
        Octan96,
        Octan98,
        Soler,
    }

    public class GasEngine : EnergySource
    {
        private const int k_MinFuel = 0;
        private readonly eFuelType r_FuelType;

        public GasEngine(eFuelType i_FuelType, float i_MaxOfTank)
            : base(i_MaxOfTank)
        {
            r_FuelType = i_FuelType;
        }
           
        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();
            string msg2 = string.Format(
@"Current gas is {0} litters
The current percentage of energy is {1}%",
                this.m_CurrentEnergy, 
                this.m_PercentageOfEnergy);
            msg.Append(msg2);
            msg.Append(Environment.NewLine);
            msg.Append("The type of the gas is: " + this.FuelType);
            return msg.ToString();
        }

        public eFuelType GasType
        {
            get { return r_FuelType; }
        }
    }
}
