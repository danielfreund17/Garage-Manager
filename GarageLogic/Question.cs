using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Question
    {
        private string m_QuestionToAsk;
        private List<string> m_Options;
        private string[] m_EnumNames;
        private int m_maxOption = 0;

        public string QuestionToAsk
        {
            get { return m_QuestionToAsk; }
            set { m_QuestionToAsk = value; }
        }

        public List<string> Options
        {
            get { return m_Options; }
            set { m_Options = value; }
        }

        public string[] EnumNames
        {
            get { return m_EnumNames; }
            set { m_EnumNames = value; }
        }

        public int MaxOption
        {
            get { return m_maxOption; }
            set { m_maxOption = value; }
        }
    }
}
