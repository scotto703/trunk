//**************************************************************
// Class: BotAction
//
// Author: Alan Jesser, Phillip Brossia
//
// Description: Handle bot actions with various calls to metaverse
//              functions and libraries
//
// Edited by: Joel McClain
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
        //make grid a GridClient from OM library
        public GridClient grid;

        //declare item as an InventoryItem from OM library
        private InventoryItem item;

        //declare atchPoint as a Vector3 AttachmentPoint from OM library
        private AttachmentPoint atchPoint;

        //declare counter as a timer for timer events
        private System.Timers.Timer counter = new System.Timers.Timer();

        //declare targetUUID as UUID object from OM library
        private UUID targetUUID;

        //declare a Vector3 location for object or position
        private Vector3 objOrPosition;

        // integer to hold amount of time for thread to sleep
        private int time;

        // flag to determine if stopThread uses a random number
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
        //function to set the targetUUID for use by OM functions
        public void setUUID(UUID aUUID)
        {
            targetUUID = aUUID;
        }

        //function to set objOrPosition Vector3 variable for use by OM functions
        public void setVector3(Vector3 posObj)
        {
            objOrPosition = posObj;
        }

        //function to set the item UUID for use by OM functions
        public void setInvItem(InventoryItem aUUID)
        {
            item = aUUID;
        }

        //function to set the Vector3 atchPoint for use by OM functions
        public void setAtchPoint(AttachmentPoint aUUID)
        {
            atchPoint = aUUID;
        }

        // sets the time integer
        public void setTime(int lengthOfTime)
        {
            time = lengthOfTime;
        }

        // sets the random boolean flag
        public void setRandom(bool value)
        {
            random = value;
        }
        #endregion

        #region ActionMethods
        //function call to cause the bot to perform an action
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
            }
        }

        // Overloaded stopThread that uses a random amount of time between
        // 2 and 10 minutes
        //private void

        // stops the thread for a given amount of time (1000 = 1 sec)
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

                // Reset flag back to false
                this.random = false;
            }
            if (time > 0)
            {                
                Thread.Sleep(time);
            }

            // reset time back to zero
            this.setTime(0);         
        }

        //function to make the bot look in the direction specified
        private void lookAt(Vector3 pos)
        {
            grid.Self.Movement.TurnToward(pos);            
        }

        //function to start the animation specified, requires animation UUID
        //which is listed in the Animations library in OM, also needs a boolean
        //to say whether actions needs to be done or not based on communication
        //to the bot
        private void animationStart(UUID aUUID, Boolean b)
        {
            grid.Self.AnimationStart(aUUID, b);
        }

        //function to stop animation that the bot is performing, requires animation UUID
        //which is listed in the Animation library in OM, also needs a boolean 
        //to say whether communication needs to go to the bot or if it can fail
        private void animationStop(UUID aUUID, Boolean b)
        {
            grid.Self.AnimationStop(aUUID, b);
        }

        //function to tell the bot to sit on an object, requires UUID of object
        //and Vector3
        private void sit(UUID aUUID, Vector3 obj)
        {
            grid.Self.RequestSit(aUUID, obj);
            grid.Self.Sit();
        }

        //function to tell the bot to stand
        private void stand()
        {
            grid.Self.Stand();
        }

        //function to tell the bot to attach an boject to self
        //will use the variables item and atchPoint to do this 
        private void attachTo()
        {
            grid.Appearance.Attach(item, atchPoint);
        }       
        #endregion

        #region EventHandlerMethods
        private void OnTimer(object source, ElapsedEventArgs e)
        {
            animationStop(targetUUID, true);
        }
        #endregion       
    }
}