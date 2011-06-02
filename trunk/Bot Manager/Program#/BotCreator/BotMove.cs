﻿//**************************************************************
// Class: BotMove
// 
// Author: Joel McClain
// Date: 12-2-09
//
//
// Description: This class handles the movement code for a bot to 
//              walk from one spot to another. 
//
// Udpates: This class can be updated to handle other movements
//          such as flying and running
//**************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenMetaverse;
using System.IO;

namespace BotGUI
{
    class BotMove
    {
        #region Attributes
        /// <summary>
        /// Client of the bot to manipulate
        /// </summary>
        private GridClient client; 
        /// <summary>
        /// A constant that is used by vector's approximate equals
        /// Determines if the bot is close enough to the destination
        /// </summary>
        private const float TARGET_DISTANCE = 1.2F;
        /// <summary>
        /// Teleports require a region
        /// </summary>
        private string regionName;          
        /// <summary>
        /// Will pause non-flying movement when true
        /// Flying movement does not pause
        /// </summary>
        private bool movePaused = false;
        /// <summary>
        /// Will exit and block non-flying movement when true
        /// Flying movement ignores this
        /// Unused and untested
        /// </summary>
        private bool moveExited = false;
        public bool pauseMove
        {
            get
            {
                return movePaused;
            }
            set
            {
                movePaused = value;
                if (value == true)
                {
                    client.Self.AutoPilotCancel();
                }
            }
        }
        public bool exitMove
        {
            get
            {
                return moveExited;
            }
            set
            {
                moveExited = value;
            }
        } 

        #endregion 

        #region Constructor

        public BotMove(GridClient client)
        {
            this.client = client;
            //Default region is current region. 
            //Bot must be logged in to find current region
            //Ensured by instantiating reader at login time
            regionName = client.Network.CurrentSim.Name;
        }       

        #endregion

        #region Set Method

        public void setRegionName(string name)
        {
            regionName = name;
        }       

        #endregion

        #region Methods
        /// <summary>
        /// Moves a bot from one position to another
        /// </summary>
        /// <param name="destination">Position to move bot to</param>
        public void moveTo(Vector3 destination)
        {
            bool arrived = false;
            Vector3 endingPos = vectorConvert(destination);
            //Flying adds to autopilot's problems with z axis
            if (client.Self.Movement.Fly == true)
                endingPos.Z = client.Self.SimPosition.Z;

            int idleCntr = 0;
            Vector3 jumpPos = client.Self.SimPosition;
            float botX = 0;
            float botY = 0;
            Vector3 idleValue;
            idleValue.X = 0;
            idleValue.Y = 0;
            idleValue.Z = 0;
            //insurance that a move action does not run for more than 5 minutes
            int failSafeExit = 0;
            int sleepTime = 500;
            int fiveMinutes = 300 * 1000 / sleepTime;
            
            if(movePaused == false && moveExited == false)
                client.Self.AutoPilot((double)endingPos.X, (double)endingPos.Y, (double)endingPos.Z);
            idleCntr = -8;//Used to avoid teleport just after bot begins moving. 
            if (client.Self.Movement.Fly != true)
            {
                while (!arrived && !moveExited)
                {
                    Thread.Sleep(sleepTime);
                    if (movePaused == true)
                    {
                        client.Self.AutoPilotCancel();
                        while (movePaused == true)
                        {
                            Thread.Sleep(1000);
                        }
                        client.Self.AutoPilot((double)endingPos.X, (double)endingPos.Y, (double)endingPos.Z);
                    }
                    else if (endingPos.ApproxEquals(vectorConvert(client.Self.SimPosition), TARGET_DISTANCE) ||
                                destination.ApproxEquals(client.Self.SimPosition, TARGET_DISTANCE))
                    {
                        client.Self.AutoPilotCancel();
                        arrived = true;
                    }
                    else
                    {

                        failSafeExit++;
                        if (failSafeExit > fiveMinutes)
                        {
                            client.Self.Teleport(client.Network.CurrentSim.Handle, destination);
                            continue;
                        }
                        //if idle for a few loops then reposition bot
                        if (client.Self.Velocity.Equals(idleValue))
                        {
                            idleCntr++;
                        }
                        else
                        {
                            idleCntr = 0;
                        }

                        if (idleCntr > 4)
                        {
                            idleCntr = 0;
                            botX = destination.X - client.Self.SimPosition.X;
                            botY = destination.Y - client.Self.SimPosition.Y;
                            jumpPos = client.Self.SimPosition;

                            if (botX > 0)
                                jumpPos.X = jumpPos.X + 1;
                            else if (botX < 0)
                                jumpPos.X = jumpPos.X - 1;

                            if (botY > 0)
                                jumpPos.Y = jumpPos.Y + 1;
                            else if (botY < 0)
                                jumpPos.Y = jumpPos.Y - 1;

                            jumpPos.Z = jumpPos.Z + 1;

                            //teleport a short jump and continue moving
                            client.Self.Teleport(client.Network.CurrentSim.Handle, jumpPos);
                            client.Self.AutoPilot((double)endingPos.X, (double)endingPos.Y, (double)endingPos.Z);
                            idleCntr = -8;//delays the next jump without using a thread sleep
                        }

                        //Fixes problem that autopilot has with the z axis
                        //Does not correct anything until it is close to the destination
                        float x = destination.X - client.Self.SimPosition.X;
                        float y = destination.Y - client.Self.SimPosition.Y;
                        float x2 = x * x;
                        float y2 = y * y;

                        if (x2 < 25 & y2 < 25)
                        {
                            if ((int)endingPos.Z > (int)client.Self.SimPosition.Z)
                            {
                                endingPos.Z = endingPos.Z - (int)((endingPos.Z - client.Self.SimPosition.Z) * 0.9) - 1;
                                client.Self.AutoPilot(endingPos.X, endingPos.Y, endingPos.Z);
                            }
                            else if ((int)endingPos.Z < (int)client.Self.SimPosition.Z)
                            {
                                endingPos.Z = endingPos.Z + (int)((client.Self.SimPosition.Z - endingPos.Z) * 0.9) + 1;
                                client.Self.AutoPilot(endingPos.X, endingPos.Y, endingPos.Z);
                            }
                        }
                    }
                }
            }
            else if (client.Self.Movement.Fly == true)
            {
                while (!arrived && !moveExited)
                {
                    Thread.Sleep(sleepTime);
                    if (endingPos.ApproxEquals(vectorConvert(client.Self.SimPosition), TARGET_DISTANCE * 10))
                    {
                        client.Self.AutoPilotCancel();
                        client.Self.Teleport(regionName, destination);
                        arrived = true;
                    }
                    else if (client.Self.Velocity.Equals(idleValue))
                    {
                        client.Self.AutoPilot((double)endingPos.X, (double)endingPos.Y, (double)endingPos.Z);
                    }
                    //Pausing the bot's movement while in the air is rare. It will happen when the situation is
                    //made intentionally or during a rare timing bug when the pause is tripped while the move
                    //call is still processing in the event reader. It is simpler to catch it here and resolve
                    //it through a teleport. 
                    if (movePaused) 
                    {
                        client.Self.AutoPilotCancel();
                        while (movePaused == true)
                        {
                            Thread.Sleep(1000);
                        }
                        client.Self.Movement.Fly = false;
                        client.Self.Teleport(client.Network.CurrentSim.Handle, destination);
                    }
                }
            }
        }
        /// <summary>
        /// Teleports a bot from one position to another
        /// </summary>        
        public void teleportTo(Vector3 destination)
        {
            client.Self.Teleport(regionName, destination);
            Thread.Sleep(2000);            
        }

        /// <summary>
        /// This method will convert local coordinates into global coordinates
        /// </summary>
        /// <param name="localCoordinate">Vector3 object that has local coordinates</param>
        /// <returns></returns>
        private Vector3 vectorConvert(Vector3 localCoordinate)
        {            
            /***************************************************************************
             * This is how the program TestClient (included in the OpenMetaverse libray)
             * gets the region corners from the current simulator that it is connected to
             * and creates global coordinates. The method has been modified to return 
             * the Vector3 object instead of calling AutoPilot() directly.
             * *************************************************************************/
             
            uint regionX, regionY;
            Utils.LongToUInts(client.Network.CurrentSim.Handle, out regionX, out regionY);
            
            double x, y, z;
            x = (double)localCoordinate.X;
            y = (double)localCoordinate.Y;
            z = (double)localCoordinate.Z;

            x += (double)regionX;
            y += (double)regionY;

            return new Vector3((float)x, (float)y, (float)z);           
        }
        #endregion
    }
}
