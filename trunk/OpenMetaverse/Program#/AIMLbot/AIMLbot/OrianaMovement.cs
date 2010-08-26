//********************************************************************************
// Class:  BotMovment
// Written by: Allan Blackford
// Edited by: Justin Hemker
// Last Modifed:  February 9, 2009  
//********************************************************************************

using OpenMetaverse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AIMLbot
{
    public class OrianaMovement
    {
        private GridClient sLclient;
        private Vector3 targetPosition1;    //target positions are global coordinates (region corner + local coordinate)
        private Vector3 targetPosition2;
        private Vector3 targetPosition3;
        private Vector3 targetPosition4;
        private Vector3 currentPosition;
        private Vector3 lookAtTarget;       //local vector of where to look when at destination
        private Vector3 targetPositionHome; //global vector where bot will always return to
        private Vector3 lookAtHome;         //local vector of where bot will look when at PositionHome
        private const double regionCornerX = 288768.00000;  //X global coordinate of island
        private const double regionCornerY = 294400.00000;  //Y global coordinate of island
        private const double TARGET_DISTANCE = .67;  //bot must be within this distance to move to next targetPosition
        private bool movementExecuted;
        private ShopperMovement simulation;

        public OrianaMovement(string question, GridClient client)
        {
            sLclient = client;
            FindQuestion(question);
            lookAtHome = new Vector3(96.3F, 195.0F, 26.3F);
        }
        private void FindQuestion(string question)
        {
            //determine if question is one that will result in bot movement
            switch (question)
            {
                case "Would you like me to take you to the freezer section?":
                    MoveToFreezer(sLclient);
                    movementExecuted = true;
                    break;
                case "Would you like me to take you to the dairy section?":
                    MoveToDairy(sLclient);
                    movementExecuted = true;
                    break;
                case "Would you like me to take you to the produce section?":
                    MoveToProduce(sLclient);
                    movementExecuted = true;
                    break;
                case "Would you like me to take you to aisle 4?":
                    MoveToAisle4(sLclient);
                    movementExecuted = true;
                    break;
                case "Would you like me to take you to aisle 2?":
                    MoveToAisle2(sLclient);
                    movementExecuted = true;
                    break;
                case "Would you like me to take you to aisle 1?":
                    MoveToAisle1(sLclient);
                    movementExecuted = true;
                    break;
                case "Mr. Wrigglesworth is in his office. Would you like me to take you to him?":
                    MoveToManager(sLclient);
                    movementExecuted = true;
                    break;
                case "Would you like to see the check out simulation?":
                    OrianaCheckOut(sLclient);
                    movementExecuted = true;
                    break;
                default:
                    movementExecuted = false;
                    break;
            }
        }
        public bool getMovementExecuted()
        {
            return movementExecuted;
        }
        private void MoveToFreezer(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.8F, 25F);
            targetPosition2 = new Vector3(288855.5F, 294591.1F, 25F);
            targetPosition3 = new Vector3(288867.2F, 294591.0F, 25F);
            targetPosition4 = new Vector3(288866.3F, 294581.4F, 25F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(102.8F, 180.0F, 25.4F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to freezer section
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                //when bot is within target distance, cancel auto pilot and exit loop
                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.Y > targetPosition2.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X < targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition3.X - TARGET_DISTANCE)
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
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("This is where we keep our frozen items like ice cream and frozen pizza.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y < targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition3.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X > targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition2.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }
        private void MoveToDairy(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.8F, 25F);
            targetPosition2 = new Vector3(288855.5F, 294591.1F, 25F);
            targetPosition3 = new Vector3(288867.2F, 294591.0F, 25F);
            targetPosition4 = new Vector3(288867.8F, 294581.3F, 25F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(101.8F, 180.4F, 25.4F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to dairy section
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.Y > targetPosition2.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X < targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition3.X - TARGET_DISTANCE)
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
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("This is where we keep our dairy items, juice and cold beer.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y < targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition3.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X > targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition2.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }
        private void MoveToProduce(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.8F, 25F);
            targetPosition2 = new Vector3(288855.5F, 294591.1F, 25F);
            targetPosition3 = new Vector3(288860.0F, 294590.7F, 25F);
            targetPosition4 = new Vector3(288861.3F, 294584.5F, 25F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(93.9F, 188.5F, 25.4F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to produce section
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.Y > targetPosition2.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X < targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition3.X - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.Chat("Right this way, please...", 0, ChatType.Normal);
            Thread.Sleep(1000);

            client.Self.AutoPilot((double)targetPosition4.X, (double)targetPosition4.Y, (double)targetPosition4.Z);
            while (currentPosition.Y > targetPosition4.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);
                if (currentPosition.Y <= targetPosition4.Y + TARGET_DISTANCE)
                {
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("This is where we keep our fresh and bagged veggies, and fruit.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y < targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition3.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X > targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition2.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }
        private void MoveToAisle4(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.7F, 25F);
            targetPosition3 = new Vector3(288857.3F, 294582.9F, 25F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(90.4F, 186.2F, 25.4F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to aisle 4 (popcorn)
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y > targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);
                if (currentPosition.Y <= targetPosition3.Y + TARGET_DISTANCE)
                {
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("As you can see, we have quite a selection of popcorn.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }
        private void MoveToAisle2(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.8F, 25F);
            targetPosition2 = new Vector3(288855.5F, 294591.1F, 25F);
            targetPosition3 = new Vector3(288863.8F, 294590.7F, 25F);
            targetPosition4 = new Vector3(288864.6F, 294584.0F, 25F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(97.1F, 187.1F, 25.4F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to Aisle2 (condiments section)
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.Y > targetPosition2.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X < targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition3.X - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.Chat("Right this way, please...", 0, ChatType.Normal);
            Thread.Sleep(1000);

            client.Self.AutoPilot((double)targetPosition4.X, (double)targetPosition4.Y, (double)targetPosition4.Z);
            while (currentPosition.Y > targetPosition4.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);
                if (currentPosition.Y <= targetPosition4.Y + TARGET_DISTANCE)
                {
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("This is where we keep salad dressings, ketchup, mustard and other condiments.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y < targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition3.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X > targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition2.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }
        private void MoveToAisle1(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.8F, 25F);
            targetPosition2 = new Vector3(288855.5F, 294591.1F, 25F);
            targetPosition3 = new Vector3(288867.2F, 294591.0F, 25F);
            targetPosition4 = new Vector3(288866.8F, 294583.8F, 25F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(96.7F, 187.9F, 25.4F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to aisle1
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.Y > targetPosition2.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X < targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition3.X - TARGET_DISTANCE)
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
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("This is where we keep general pharmacy items, baking goods, snacks, soda and beer.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y < targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition3.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X > targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition2.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }
        private void MoveToManager(GridClient client)
        {
            targetPosition1 = new Vector3(288855.7F, 294595.8F, 25F);
            targetPosition2 = new Vector3(288858.7F, 294574.0F, 25F);
            targetPosition3 = new Vector3(288868.5F, 294574.1F, 30F);
            targetPosition4 = new Vector3(288860.6F, 294590.3F, 30F);
            targetPositionHome = new Vector3(288864.9F, 294595.4F, 25F);
            lookAtTarget = new Vector3(94.3F, 193.7F, 30.3F);
            currentPosition = vectorConvert(client.Self.RelativePosition);

            //move to manager desk
            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            client.Self.Chat("Follow me please.", 0, ChatType.Normal);
            while (currentPosition.X > targetPosition1.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition1.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.Y > targetPosition2.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.X < targetPosition3.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPosition3.X - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition4.X, (double)targetPosition4.Y, (double)targetPosition4.Z);
            while (currentPosition.Y < targetPosition4.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);
                if (currentPosition.Y >= targetPosition4.Y - TARGET_DISTANCE)
                {
                    currentPosition = targetPosition4;
                    client.Self.Movement.TurnToward(lookAtTarget);
                    Thread.Sleep(2500);
                    client.Self.Chat("This is Mr. Wrigglesworth.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                    client.Self.Chat("If you need anything else I'll be at the counter.", 0, ChatType.Normal);
                    Thread.Sleep(2500);
                }
            }

            //move back to counter
            client.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
            while (currentPosition.Y > targetPosition3.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y <= targetPosition3.Y + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
            while (currentPosition.X > targetPosition2.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X <= targetPosition2.X + TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
            while (currentPosition.Y < targetPosition1.Y)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.Y >= targetPosition1.Y - TARGET_DISTANCE)
                {
                    client.Self.AutoPilotCancel();
                    break;
                }
            }

            client.Self.AutoPilot((double)targetPositionHome.X, (double)targetPositionHome.Y, (double)targetPositionHome.Z);
            while (currentPosition.X < targetPositionHome.X)
            {
                Thread.Sleep(0);
                currentPosition = vectorConvert(client.Self.RelativePosition);

                if (currentPosition.X >= targetPositionHome.X - TARGET_DISTANCE)
                {
                    break;
                }
            }

            client.Self.Movement.TurnToward(lookAtHome);
        }

        //CheckOut procedure - Orianna's side
        public void OrianaCheckOut(GridClient client)
        {
            simulation = simulation.state;

            movementExecuted = false;
            simulation.CheckOut = 1;

            while (simulation.CheckOut > 0)
            {

                switch (simulation.CheckOut)
                {
                    case 1:
                        sLclient.Self.Chat("Are you ready to check out?", 0, ChatType.Normal);
                        simulation.CheckOut = 2;
                        break;

                    case 2:
                        sLclient.Self.Chat("That will be $7.49 please.", 0, ChatType.Normal);
                        simulation.CheckOut = 3;
                        break;
                    case 3:
                        sLclient.Self.Chat("And here's your change!", 0, ChatType.Normal);
                        //animation?
                        simulation.CheckOut = 4;
                        movementExecuted = true;
                        break;
                }
            }
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
