//**************************************************************
// Class: BotLoadAIML
// 
// Author: Joel McClain
//
// Date: 7-15-10
//
// Description: This class controls all the AIML processing that 
// each avatar will have.               
//
//**************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIMLbot;
using AIMLbot.Utils;
using OpenMetaverse;

namespace BotGUI
{
    class BotLoadAIML
    {
        #region Attributes
        string m_AimlPath; 
        string m_SettingsPath; 
        Bot m_myBot = new Bot();
        AIMLLoader m_Loader; 
        User myUser;
        System.Timers.Timer radiusTimer = new System.Timers.Timer();
        System.Timers.Timer ListTimer = new System.Timers.Timer();
        string m_chatQuestion;
        Request m_chatRequest;
        Result m_chatResult;
        string m_imQuestion;
        Request m_imRequest;
        Result m_imResult;
        #endregion

        #region Properties
        public string chatQuestion{ get { return m_chatQuestion; } set{ m_chatQuestion = value; } }
        public Request chatRequest { get { return m_chatRequest; } set { m_chatRequest = value; } }
        public Result chatResult { get { return m_chatResult; } set { m_chatResult = value; } }
        public string imQuestion { get { return m_imQuestion; } set { m_imQuestion = value; } }
        public Request imRequest { get { return m_imRequest; } set { m_imRequest = value; } }
        public Result imResult { get { return m_imResult; } set { m_imResult = value; } }
        public string AimlPath { get { return m_AimlPath; } set { m_AimlPath = value; } }
        public string SettingsPath { get { return m_SettingsPath; } set { m_SettingsPath = value; } }
        public Bot myBot { get { return m_myBot; } set { m_myBot = value; } }
        public AIMLLoader Loader { get { return m_Loader; } set { m_Loader = value; } }
        #endregion

        #region Constructor
        public BotLoadAIML(string name)
        {
            Loader = new AIMLLoader(m_myBot);
            myUser = new User(name, m_myBot);
        }
        #endregion
    }
}
