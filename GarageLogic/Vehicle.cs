using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_ModelName;
        protected readonly string r_LicensePlate;
        protected EnergySource m_EnergySource;
        protected List<Wheel> m_Wheels;
        protected List<Question> m_QustionsList;

        public Vehicle(int i_NumberOfWheels, string i_ModelName, string i_LicenceNumber)
        {
            m_Wheels = new List<Wheel>(i_NumberOfWheels);
            m_QustionsList = new List<Question>();
            r_ModelName = i_ModelName;
            r_LicensePlate = i_LicenceNumber;
        }

        public abstract float MaxAirInWheel
        {
            get;
        }

        public abstract int NumberOfWheels
        {
            get;
        }

        public ReadOnlyCollection<Question> GetQuestions
        {
            get { return m_QustionsList.AsReadOnly(); }
        }

        public abstract void InsertAnswer(string i_UserInput, int i_Index);

        public abstract List<string> GetUniqueInfo();

        public string LicensePlate
        {
            get { return r_LicensePlate; }
        }

        public string ModelName
        {
            get { return r_ModelName; }
        }

        public EnergySource Energy
        {
            get { return m_EnergySource; }
            set { m_EnergySource = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
        }

        protected void createQuestion(Question i_Question)
        {
            int indexer = 1;
            i_Question.Options = new List<string>();
            if (i_Question.EnumNames != null)
            {
                i_Question.MaxOption = i_Question.EnumNames.Length;
                foreach (string eName in i_Question.EnumNames)
                {
                    i_Question.Options.Add(string.Format("{0}. {1}", indexer++, eName.ToString()));
                }
            }
        }
    }
}
