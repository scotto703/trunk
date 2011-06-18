//**************************************************************
// Class: BotAction
//
// Author: Alan Jesser, Phillip Brossia
//
// Description: Handle bot actions with various calls to metaverse
//              functions and libraries
//
// Major Revision by: Joel McClain
// Date: 12-7-09
//**************************************************************

using System;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
using System.Timers;
using System.Threading;

namespace BotGUI
{
    public class BotAction
    {
        #region Attributes
        /// <summary>
        ///  make grid a GridClient from OM library
        /// </summary>
        private GridClient grid;

        /// <summary>
        /// declare item as an InventoryItem from OM library
        /// </summary>
        private InventoryItem item;

        /// <summary>
        /// declare atchPoint as a Vector3 AttachmentPoint from OM library
        /// </summary>
        private AttachmentPoint atchPoint;

        /// <summary>
        /// declare counter as a timer for timer events
        /// </summary>
        private System.Timers.Timer counter = new System.Timers.Timer();              

        /// <summary>
        /// declare targetUUID as UUID object from OM library
        /// </summary>
        private UUID targetUUID;

        /// <summary>
        /// declare targetLocalID as an unsigned int that represent the local id of the object
        /// </summary>
        private uint targetLocalID;

        /// <summary>
        /// declare a Vector3 location for object or position
        /// </summary>
        private Vector3 objOrPosition;

        /// <summary>
        /// integer to hold amount of time for thread to sleep
        /// </summary>
        private int time;

        /// <summary>
        ///  flag to determine if stopThread uses a random number
        /// </summary>
        private bool random = false;
        #endregion

        #region Constructor
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="client">Sets which bot to perform an action on</param>
        public BotAction(GridClient client)
        {
            grid = client;
        }
        #endregion

        #region setMethods
        /// <summary>
        /// function to set the targetUUID for use by OM functions
        /// </summary>
        /// <param name="aUUID">Unique ID</param>
        public void setUUID(UUID aUUID)
        {
            targetUUID = aUUID;
        }

        /// <summary>
        /// function to set the targetLocalID for use by OM functions
        /// </summary>
        /// <param name="aUUID">Unique ID</param>
        public void setLocalID(uint lID)
        {
            targetLocalID = lID;
        }

        /// <summary>
        /// function to set objOrPosition Vector3 variable for use by OM functions
        /// </summary>
        /// <param name="posObj">Vector3 object position</param>
        public void setVector3(Vector3 posObj)
        {
            objOrPosition = posObj;
        }

        /// <summary>
        /// function to set the item UUID for use by OM functions
        /// </summary>
        /// <param name="aUUID">Unique ID</param>
        public void setInvItem(InventoryItem aUUID)
        {
            item = aUUID;
        }

        /// <summary>
        /// function to set the Vector3 atchPoint for use by OM functions
        /// </summary>
        /// <param name="aUUID">Unique ID</param>
        public void setAtchPoint(AttachmentPoint aUUID)
        {
            atchPoint = aUUID;
        }

        /// <summary>
        /// sets the time integer
        /// </summary>
        /// <param name="lengthOfTime">Integer in millimeters</param>
        public void setTime(int lengthOfTime)
        {
            time = lengthOfTime;
        }

        /// <summary>
        /// sets the random boolean flag
        /// </summary>
        /// <param name="value">Boolean value</param>
        public void setRandom(bool value)
        {
            random = value;
        }
        #endregion

        #region ActionMethods
        /// <summary>
        /// function call to cause the bot to perform an action
        /// </summary>
        /// <param name="ActionType">Integer that denotes the action to be performed</param>
        /// <param name="time">Integer length of time in milliseconds that is used for the animation event interval</param>
        public void createAction(int ActionType, int time)
        {
            //switch statement to decide which action to perform
            switch (ActionType)
            {
                //case 1 functions require timer events since animations
                //in OM library do not have a stop built in
                //requires the UUID of the action to perform and length to perform it
                //time is in clock ticks, 1000 ticks is roughly 1 second
                case 1:
                    animationStart(targetUUID, true);
                    counter.Elapsed += new ElapsedEventHandler(OnTimer);
                    counter.Interval = time;
                    counter.Start();
                    counter.AutoReset = false;
                    break;
                //case 2 is just a sit actions, requires the UUID of the object
                //to sit on as well as the Vector3 position
                case 2:
                    sit(targetUUID, objOrPosition);
                    break;
                //case 3 will turn the bot in the direction of the Vector3 position
                case 3:
                    lookAt(objOrPosition);
                    break;
                //case 4 is to make the bot stand if not already doing so
                case 4:
                    stand();
                    break;
                //case 5 is used to call the attachTo function which will attach
                //an item to a location on the bot
                case 5:                    
                    attachTo();
                    break;
                // case 6 is used to stop a thread for a given amount of time
                case 6:
                    stopThread();
                    break;
                // case 7 is used to make the bot fly, or to land if it is already flying
                case 7:
                    toggleFly();
                    break;
                // case 8 is used to make the bot click on an object
                case 8:
                    clickObject(targetLocalID);
                    break;
            }
        }

        /// <summary>
        /// Overloaded stopThread that uses a random amount of time between
        /// 2 and 10 minutes
        /// stops the thread for a given amount of time (1000 = 1 sec)
        /// </summary>
        private void stopThread()
        {
            // generate random wait time if selected
            if (random == true)
            {
                // Generate random number between min and max time 
                // variables and store the value
                Random generator = new Random();
                int min = 120000;  // 2 mintues
                int max = 600000;  // 10 mintues
                time = generator.Next(min, max);                
            }
            if (time > 0)
            {
                Thread.Sleep(time);
            }
            // reset time back to zero
            this.setTime(0);         
        }       

        /// <summary>
        /// function to make the bot look in the direction specified
        /// </summary>
        /// <param name="pos">Vector3 position of the location to look at</param>
        private void lookAt(Vector3 pos)
        {
            grid.Self.Movement.TurnToward(pos);            
        }

        /// <summary>
        /// function to start the animation specified, requires animation UUID
        /// which is listed in the Animations library in OM, also needs a boolean
        /// to say whether actions needs to be done or not based on communication
        /// to the bot
        /// </summary>
        /// <param name="aUUID">Unique ID of the action to be performed</param>
        /// <param name="b">Boolean stating to ensure if this works or not</param>
        private void animationStart(UUID aUUID, Boolean b)
        {
            grid.Self.AnimationStart(aUUID, b);
        }

        
        /// <summary>
        /// function to stop animation that the bot is performing, requires animation UUID
        /// which is listed in the Animation library in OM, also needs a boolean 
        /// to say whether communication needs to go to the bot or if it can fail
        /// </summary>
        /// <param name="aUUID">Unique ID of the action to be performed</param>
        /// <param name="b">Boolean stating to ensure if this works or not</param>
        private void animationStop(UUID aUUID, Boolean b)
        {
            grid.Self.AnimationStop(aUUID, b);
        }

        
        /// <summary>
        /// function to tell the bot to sit on an object, requires UUID of object
        /// and Vector3
        /// </summary>
        /// <param name="aUUID">Unique ID of the object to sit on</param>
        /// <param name="obj">Vector3 location of the object</param>
        private void sit(UUID aUUID, Vector3 obj)
        {
            grid.Self.RequestSit(aUUID, obj);
            grid.Self.Sit();
        }

        
        /// <summary>
        /// function to tell the bot to stand
        /// </summary>
        private void stand()
        {
            grid.Self.Stand();
        }

        /// <summary>
        /// function to tell the bot to attach an oject to self
        /// will use the variables item and atchPoint to do this
        /// </summary>
        private void attachTo()
        {
            grid.Appearance.Attach(item, atchPoint);
        }

        /// <summary>
        /// Bot flies and moves to a high altitude
        /// </summary>        
        private void toggleFly()
        {
            int height = 60;//minimum height to fly to
            if (grid.Self.Movement.Fly == false)
            {
                grid.Self.Movement.Fly = true;
                while (grid.Self.SimPosition.Z < height)
                {
                    grid.Self.Movement.UpPos = true;
                    Thread.Sleep(100);
                    grid.Self.Movement.UpPos = false;
                    Thread.Sleep(200);
                }
            }
            else if (grid.Self.Movement.Fly == true)
            {
                grid.Self.Movement.Fly = false;
            }
        }

        /// <summary>
        /// Bot clicks on an object
        /// </summary>        
        private void clickObject(uint localID)
        {
            grid.Objects.ClickObject(grid.Network.CurrentSim, localID);
        }
        #endregion

        #region EventHandlerMethods
        /// <summary>
        /// Handler function for the animate event, stops the animation
        /// </summary> 
        private void OnTimer(object source, ElapsedEventArgs e)
        {
            animationStop(targetUUID, true);
        }        
        #endregion
    }
}