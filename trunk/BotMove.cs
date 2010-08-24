//**************************************************************
// Class: BotMove
// 
// Author: Joel McClain
// Date: 12-2-09
//
// Global Coordinates and vectorConvert method by: Allan Blackford 
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

namespace BotGUI
{
    class BotMove
    {
        #region Attributes
        GridClient client;
        const double regionCornerX = 288768.00000;  //X global coordinate of island
        const double regionCornerY = 294400.00000;  //Y global coordinate of island
        const float TARGET_DISTANCE = 1.25F;
        string regionName;    // for use with teleport
        
        #endregion 

        #region Constructor
        public BotMove(GridClient client)
        {
            this.client = client;
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
            Vector3 currentPos = vectorConvert(destination);

            client.Self.AutoPilot((double)currentPos.X, (double)currentPos.Y, (double)currentPos.Z);            
            while (!arrived)
            {
                Thread.Sleep(0);                
                if (currentPos.ApproxEquals(vectorConvert(client.Self.RelativePosition), TARGET_DISTANCE))
                {
                    client.Self.AutoPilotCancel();
                    arrived = true;
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
             * This is how the program TestClient (included in the openmetaverse libray)
             * gets the region corners from the current simulator that it is connected to
             * and creates global coordinates. 
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
