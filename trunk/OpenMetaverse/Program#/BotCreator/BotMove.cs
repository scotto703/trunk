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
//
// Updates: Added turntoward method (Lawrence Miller)
//**************************************************************

using System.Threading;
using OpenMetaverse;

namespace BotGUI
{
    class BotMove
    {
        #region Attributes

        GridClient client;
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
                if (currentPos.ApproxEquals(vectorConvert(client.Self.SimPosition), TARGET_DISTANCE))
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
        /// Turns a bot to face target object
        /// </summary>
        /// <param name="target">vector3 object to turn towards</param>
        public void turnToward(Vector3 target)
        {
            client.Self.Movement.TurnToward(target);
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
             * and creates global coordinates. Updated it to return a Vector3 object instead
             * of calling autopilot() directly.
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