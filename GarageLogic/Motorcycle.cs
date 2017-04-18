using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eLicenceType
    {
        A,
        A1,
        AB,
        B1
    }

    public class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const float k_MaxAirPressure = 31;
        private const float k_MaxElectricalEngine = 1.9f;
        private const float k_MaxGasEngine = 7.2f;
        private const float k_MaxEngineCapacity = 2500;
        private eLicenceType m_LicenceType;
        private int m_EngineSize;

        public Motorcycle(string i_ModelName, string i_LicenceNumber)
            : base(k_NumberOfWheels, i_ModelName, i_LicenceNumber)
        {
            setMotorcycleInfo();
        }          
        
        public void setMotorcycleInfo()
        {
            this.m_QustionsList = new List<Question>();

            Question Q1 = new Question();
            Q1.QuestionToAsk = "What kind of engine the motorcycle have?";
            Q1.EnumNames = Enum.GetNames(typeof(eEngineTypes));
            this.m_QustionsList.Add(Q1);

            Question Q2 = new Question();
            Q2.QuestionToAsk = "What is the license type of the motorcycle";
            Q2.EnumNames = Enum.GetNames(typeof(eLicenceType));
            this.m_QustionsList.Add(Q2);

            Question Q3 = new Question();
            Q3.QuestionToAsk = "What is the engine capacity? (in Cc)";
            this.m_QustionsList.Add(Q3);

            Question Q4 = new Question();
            Q4.QuestionToAsk = "What is the current amount of energy?";
            this.m_QustionsList.Add(Q4);

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
                    if(i_UserInput == "Gas")
                    {
                        this.m_EnergySource = new GasEngine(eFuelType.Octan95, k_MaxGasEngine);
                    }
                    else
                    {
                        this.m_EnergySource = new ElectricEngine(k_MaxElectricalEngine);
                    }

                    break;
                case 2:
                    m_LicenceType = (eLicenceType)Enum.Parse(typeof(eLicenceType), i_UserInput);
                    break;
                case 3:
                    EngineSize = int.Parse(i_UserInput);
                    break;
                case 4:
                    m_EnergySource.CurrentEnergy = float.Parse(i_UserInput);
                    break;
                default:
                    break;
            }
        }

        public override List<string> GetUniqueInfo()
        {
            List<string> MotorcycleInfoList = new List<string>();
            MotorcycleInfoList.Add(string.Format("The motorcycle's license is {0}.", m_LicenceType.ToString()));
            MotorcycleInfoList.Add(string.Format("The motorcycle's engine capacity is {0}", m_EngineSize.ToString()));
            return MotorcycleInfoList;
        }

        public eLicenceType LicenceType
        {
            get { return m_LicenceType; }
        }

        public int EngineSize
        {
            get { return m_EngineSize; }
            set
            {
                if (value <= 0 || value > k_MaxEngineCapacity)
                {
                    throw new ValueOutOfRangeException("The Value is out of range", 0, k_MaxEngineCapacity);
                }
                else
                {
                    m_EngineSize = value;
                }
            }
        }

        public override float MaxAirInWheel
        {
            get { return k_MaxAirPressure; }
        }

        public override int NumberOfWheels
        {
            get { return k_NumberOfWheels; }
        }
    }
}
