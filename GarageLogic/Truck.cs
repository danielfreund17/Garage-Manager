using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eDangerousMatirials
    {
        Dangerous = 1,
        NotDangerous
    }
       
    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const float k_MaxAirPressure = 28;
        private const float k_MaxTankCapacity = 135;
        private bool m_CarriesDangerousMetirials;
        private float m_MaxCarryWeight;

        public Truck(string i_ModelName, string i_LicenceNumber)
            : base(k_NumberOfWheels, i_ModelName, i_LicenceNumber)
        {
            this.m_EnergySource = new GasEngine(eFuelType.Soler, k_MaxTankCapacity);
            setTruckInfo();
        }

        private void setTruckInfo()
        {
            this.m_QustionsList = new List<Question>();

            Question Q1 = new Question();
            Q1.QuestionToAsk = "Does the truck carries dangerous matirials";
            Q1.EnumNames = Enum.GetNames(typeof(eDangerousMatirials));
            this.m_QustionsList.Add(Q1);

            Question Q2 = new Question();
            Q2.QuestionToAsk = "What is the maximum carry weight of the truck";
            this.m_QustionsList.Add(Q2);

            Question Q3 = new Question();
            Q3.QuestionToAsk = "What is the current amount of litters of the engine?";
            this.m_QustionsList.Add(Q3);

            foreach (Question question in this.m_QustionsList)
            {
                createQuestion(question);
            } 
        }

        public override void InsertAnswer(string i_UserInput, int i_Index)
        {
            switch (i_Index)
            {
                case 1:
                    if(i_UserInput == eDangerousMatirials.Dangerous.ToString())
                    {
                        m_CarriesDangerousMetirials = true;
                    }
                    else
                    {
                        m_CarriesDangerousMetirials = false;
                    }

                    break;
                case 2:
                    m_MaxCarryWeight = float.Parse(i_UserInput);
                    break;
                case 3:
                    m_EnergySource.CurrentEnergy = float.Parse(i_UserInput);
                    break;
                default:
                    break;
            }
        }

        public override List<string> GetUniqueInfo()
        {
            List<string> TruckInfoList = new List<string>();
            string carriesMatirials = m_CarriesDangerousMetirials ? string.Empty : " not";
            TruckInfoList.Add(string.Format("The truck do{0} carries dangerous matterials.", carriesMatirials));
            TruckInfoList.Add(string.Format("The truck's maximum carry weight is {0}", m_MaxCarryWeight.ToString()));
            return TruckInfoList;
        }

        public bool IsCarriesDangerousMatirials
        {
            get { return m_CarriesDangerousMetirials; }
            set { m_CarriesDangerousMetirials = value; }  
        }
        
        public float MaxCarryWeight
        {
            get { return m_MaxCarryWeight; }
            set { m_MaxCarryWeight = value; }
        }

        public int MaxNumberOfWheels
        {
            get { return k_NumberOfWheels; }
        }

        public override float MaxAirInWheel
        {
            get { return k_MaxAirPressure; }
        }

        public float MaxTankCapacity
        {
            get { return k_MaxTankCapacity; }
        }

        public override int NumberOfWheels
        {
            get { return k_NumberOfWheels; }
        }
    }
}
