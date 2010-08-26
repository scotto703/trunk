//********************************************************************************
// Class:  AlisandraMovement
// Written by: Justin Hemker
// 
// Last Modifed:  March 31, 2009
//********************************************************************************

using OpenMetaverse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AIMLbot
{
    public class AlisandraMovement
    {
        private GridClient sLclient;
        private Vector3 targetPosition1;    //target positions are global coordinates (region corner + local coordinate)
        private Vector3 targetPosition2;
        private Vector3 targetPosition3;
        private Vector3 targetPosition4;
        private Vector3 targetPosition5;
        private Vector3 targetPosition6;
        private Vector3 targetPosition7;
        private Vector3 targetPositionHome; //global vector where bot will always return to
        private Vector3 currentPosition;
        
        private Vector3 teleportPosition1;
        private Vector3 teleportPosition2;
        
        private const double regionCornerX = 288768.00000;  //X global coordinate corner of island
        private const double regionCornerY = 294400.00000;  //Y global coordinate corner of island
        private const double TARGET_DISTANCE = .85;         //bot must be within this distance to move to next targetPosition
        private const int sleepTime = 300000;               //this is the amount of time bot will sit at one location
        
        public AlisandraMovement(GridClient client)
        {
            sLclient = client;
            WanderIsland(sLclient);//this calls the WanderIsland function as default movement when bot is logged on.
        }

        private void WanderIsland(GridClient client)
        {
            targetPosition1 = new Vector3(288909.8F, 294505.3F, 25F);
            targetPosition2 = new Vector3(288934.5F, 294510.6F, 28F);
            targetPosition3 = new Vector3(288898.0F, 294501.6F, 25F);
            targetPosition4 = new Vector3(288897.9F, 294476.0F, 29F);
            targetPosition5 = new Vector3(288880.8F, 294617.8F, 26F);
            targetPosition6 = new Vector3(288872.9F, 294610.0F, 25F);
            targetPosition7 = new Vector3(288866.8F, 294583.3F, 25F);
            targetPositionHome = new Vector3(147F, 146F, 26F);

            teleportPosition1 = new Vector3(125F, 58F, 58F);
            teleportPosition2 = new Vector3(113F, 56F, 25F);

            UUID fountainBench = new UUID("c2537f4b-1551-1a01-73ea-c46e102c1d1b");
            UUID admissionsLoveseat = new UUID("9cc94104-1260-a097-b2d9-ea1ecf7a18ab");
            UUID meditationBench = new UUID("61c4f29c-6fb9-9687-9a44-a7d88edd0296");
            UUID gaziboPoseball = new UUID("d7b6f951-c24d-eb1d-939d-6a3fa0f629cb");

            currentPosition = vectorConvert(client.Self.RelativePosition);

            #region event 1
            client.Self.RequestSit(fountainBench, Vector3.Zero);
            client.Self.Sit();
            Thread.Sleep(sleepTime);  //sleep thread to relax at bench before moving

            client.Self.Chat("OMG! How long was I daydreaming??? I need to get to the admissions office!!!", 0, ChatType.Normal);
            client.Self.Stand();  //stand up from bench
            Thread.Sleep(1000);  //must sleep thread for stand() to take place

            //head to admissions office
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y > targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);
                
                //when bot is within target distance, cancel auto pilot and exit loop
                if (currentPosition.Y <= targetPosition1.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }
            #endregion

            #region event 2
            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X < targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition2.X - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.Chat("Where is everybody?", 0, ChatType.Normal);
            client.Self.Chat("I guess I'll just have a seat and wait...", 0, ChatType.Normal);
            client.Self.RequestSit(admissionsLoveseat, Vector3.Zero);
            client.Self.Sit();
            Thread.Sleep(sleepTime);  //relax on admissions couch

            client.Self.Chat("I should go meditate for a while.", 0, ChatType.Normal);
            client.Self.Stand();
            Thread.Sleep(1000);
            #endregion

            #region event 3
            //head to library
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X > targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition3.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition4.X, (double)targetPosition4.Y, (double)targetPosition4.Z);
            while (currentPosition.Y > targetPosition4.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);
                if (currentPosition.Y <= targetPosition4.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }
            #endregion

            #region event 4
            //teleport to library roof
            client.Self.Teleport("Baker Island", teleportPosition1);

            client.Self.RequestSit(meditationBench, Vector3.Zero);
            client.Self.Sit();
            Thread.Sleep(sleepTime);  //meditate on library roof

            client.Self.Chat("I think I'll go hang out at the gazebo for a while.", 0, ChatType.Normal);
            client.Self.Stand();
            Thread.Sleep(1000);
            #endregion

            #region event 5
            //head to gabezo
            //teleport to ground level
            client.Self.Teleport("Baker Island", teleportPosition2);

            client.Self.AutoPilot((double)targetPosition5.X, (double)targetPosition5.Y, (double)targetPosition5.Z);
            while (currentPosition.Y < targetPosition5.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition5.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.RequestSit(gaziboPoseball, Vector3.Zero);
            client.Self.Sit();
            Thread.Sleep(sleepTime);  //relax at gazibo

            client.Self.Chat("I should go to the grocery store to pick up a few things.", 0, ChatType.Normal);
            client.Self.Stand();
            Thread.Sleep(1000);
            #endregion

            #region event 6
            //head to grocery store
            client.Self.AutoPilot((double)targetPosition6.X, (double)targetPosition6.Y, (double)targetPosition6.Z);
            while (currentPosition.Y > targetPosition6.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition6.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition7.X, (double)targetPosition7.Y, (double)targetPosition7.Z);
            while (currentPosition.Y > targetPosition7.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition7.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            //insert code to interact at baker grocery here
            Thread.Sleep(50000);
            #endregion

            #region event 7
            //teleport back to fountain bench to re-run loop
            client.Self.Teleport("Baker Island", targetPositionHome);
            WanderIsland(sLclient);
            #endregion
        }

        //Function to convert double local coordinates to float global coordinates and round to one decimal place
        static Vector3 vectorConvert(Vector3 localCoordinate)
        {
            float newX, newY, newZ;

            newX = (float)Math.Round((regionCornerX + localCoordinate.X), 1);
            newY = (float)Math.Round((regionCornerY + localCoordinate.Y), 1);
            newZ = localCoordinate.Z;

            return new Vector3(newX, newY, newZ);
        }
    }
}