using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eNumberOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
    }

    public enum eCarColor
    {
        Red = 1,
        Yellow,
        Black,
        White,
    }

    public class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaxAirPressure = 30;
        private const float k_MaxElectricalEngine = 3.3f;
        private const float k_MaxGasEngine = 38;
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumberOfDoors;

        public Car(string i_ModelName, string i_LicenseNumber)
            : base(k_NumberOfWheels, i_ModelName, i_LicenseNumber)
        {
            setCarInfo();
        }

        private void setCarInfo()
        {
            this.m_QustionsList = new List<Question>();

            Question Q1 = new Question();
            Q1.QuestionToAsk = "What kind of engine the car have?";
            Q1.EnumNames = Enum.GetNames(typeof(eEngineTypes));
            this.m_QustionsList.Add(Q1);

            Question Q2 = new Question();
            Q2.QuestionToAsk = "How many doors the car have?";
            Q2.EnumNames = Enum.GetNames(typeof(eNumberOfDoors));
            this.m_QustionsList.Add(Q2);

            Question Q3 = new Question();
            Q3.QuestionToAsk = "Please choose the color of the car:";
            Q3.EnumNames = Enum.GetNames(typeof(eCarColor));
            this.m_QustionsList.Add(Q3);

            Question Q4 = new Question();
            Q4.QuestionToAsk = "What is the current amount of energy?";
            this.m_QustionsList.Add(Q4);

            foreach(Question question in this.m_QustionsList)
            {
                createQuestion(question);
            }       
        }

        public override void InsertAnswer(string i_UserInput, int i_Index)
        {
            switch (i_Index)
            {
                case 1:
                    if (i_UserInput == eEngineTypes.Gas.ToString())
                    {
                        m_EnergySource = new GasEngine(eFuelType.Octan98, k_MaxGasEngine);
                    }
                    else
                    {
                        m_EnergySource = new ElectricEngine(k_MaxElectricalEngine);
                    }

                    break;
                case 2:
                    m_NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), i_UserInput);
                    break;
                case 3:
                    m_CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_UserInput);
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
            List<string> carInfoList = new List<string>();
            carInfoList.Add(string.Format("The car has {0} doors.", m_NumberOfDoors.ToString()));
            carInfoList.Add(string.Format("The car color is {0}", m_CarColor.ToString()));
            return carInfoList;
        }

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get { return m_NumberOfDoors; }
            set { m_NumberOfDoors = value; }
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
