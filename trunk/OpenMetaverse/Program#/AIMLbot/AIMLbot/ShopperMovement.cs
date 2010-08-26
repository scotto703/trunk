//********************************************************************************
// Class:  ShopperMovment
// Written by: Brian Wetherell
// Edited by: 
// Last Modifed:  June 9, 2009  
//comments:
//This code file provides movement based on the state of the Shopper bot.
//
//A 'CheckOut' is triggered by in-world dialogue with the clerk. The shopper bot default movement
//has the bot browsing the store's items.
//
//********************************************************************************

using OpenMetaverse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AIMLbot
{
    public class ShopperMovement
    {
        //Movement variables used to shop
        private GridClient sLclient;
        private Vector3 targetPosition1;    //target positions are global coordinates (region corner + local coordinate)
        private Vector3 targetPosition2;
        private Vector3 targetPosition3;
        private Vector3 targetPosition4;
        private Vector3 targetPosition5;
        private Vector3 targetPosition6;

        //Movement variables used to walk the 'check out' path
        private Vector3 CheckOutPosition1;    //target positions are global coordinates (region corner + local coordinate)
        private Vector3 CheckOutPosition2;
        private Vector3 CheckOutPosition3;
        private Vector3 CheckOutPosition4;
        private Vector3 CheckOutPosition5;
        private Vector3 Teleport1;


        //needed generic variables
        private Vector3 currentPosition;
        private Vector3 lookAtTarget;       //local vector of where to look when at destination
        private Vector3 lookAtTarget2;       //local vector of where to look when at destination
        private const double regionCornerX = 288768.00000;  //X global coordinate of island
        private const double regionCornerY = 294400.00000;  //Y global coordinate of island
        private const double TARGET_DISTANCE = .65;  //bot must be within this distance to move to next targetPosition

        //Constructor
        public ShopperMovement(GridClient client, int Check)
        {
            sLclient = client;
            DefaultMovement(sLclient);
            CheckOut = Check;
        }

        //checkout property of the class
        public int CheckOut
        {
            get
            {
                return CheckOut;
            }
            set
            {
                CheckOut = value;
            }
        }

        public ShopperMovement state
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        //This function automates the shopper bot in the store.
        private void DefaultMovement(GridClient sLclient)
        {
            //set target positions for pathing.
            targetPosition1 = new Vector3(288869.50F, 294591.20F, 24.99F);
            targetPosition2 = new Vector3(288867.80F, 294585.90F, 24.98F);
            lookAtTarget = new Vector3(97.9F, 186.2F, 26.6F);
            targetPosition3 = new Vector3(288867.70F, 294581F, 24.99F);
            targetPosition4 = new Vector3(288856.70F, 294581.30F, 24.99F);
            targetPosition5 = new Vector3(288857F, 294586.40F, 24.99F);
            lookAtTarget2 = new Vector3(90.7F, 187.9F, 25.5F);
            targetPosition6 = new Vector3(288856.60F, 294591.20F, 24.99F);
            currentPosition = vectorConvert(sLclient.Self.RelativePosition);


            //Shopper walks around the store in a loop, stopping occasionally to browse.
            while (CheckOut == 0)
            {
                //I set this sleep so that Nimon has a few seconds to actually render in-world before he moves.
                Thread.Sleep(500);



                //walks to targetposition #2
                sLclient.Self.AutoPilot((double)targetPosition2.X, (double)targetPosition2.Y, (double)targetPosition2.Z);
                while (currentPosition.Y > targetPosition2.Y)
                {
                    Thread.Sleep(0);
                    currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                    //when bot is within target distance, cancel auto pilot and exit loop
                    if (currentPosition.Y <= targetPosition2.Y + TARGET_DISTANCE)
                    {
                        sLclient.Self.AutoPilotCancel();
                        break;
                    }
                }

                //First stop, looks at top shelf.
                sLclient.Self.Movement.TurnToward(lookAtTarget);
                Thread.Sleep(10000);

                //continues shopping loop - target position #3
                sLclient.Self.AutoPilot((double)targetPosition3.X, (double)targetPosition3.Y, (double)targetPosition3.Z);
                while (currentPosition.Y > targetPosition3.Y)
                {
                    Thread.Sleep(0);
                    currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                    //when bot is within target distance, cancel auto pilot and exit loop
                    if (currentPosition.Y <= targetPosition3.Y + TARGET_DISTANCE)
                    {
                        sLclient.Self.AutoPilotCancel();
                        break;
                    }
                }

                //target position #4
                sLclient.Self.AutoPilot((double)targetPosition4.X, (double)targetPosition4.Y, (double)targetPosition4.Z);
                while (currentPosition.X > targetPosition4.X)
                {
                    Thread.Sleep(0);
                    currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                    //when bot is within target distance, cancel auto pilot and exit loop
                    if (currentPosition.X <= targetPosition4.X + TARGET_DISTANCE)
                    {
                        sLclient.Self.AutoPilotCancel();
                        break;
                    }
                }

                // target position #5
                sLclient.Self.AutoPilot((double)targetPosition5.X, (double)targetPosition5.Y, (double)targetPosition5.Z);
                while (currentPosition.Y < targetPosition5.Y)
                {
                    Thread.Sleep(0);
                    currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                    //when bot is within target distance, cancel auto pilot and exit loop
                    if (currentPosition.Y >= targetPosition5.Y + TARGET_DISTANCE)
                    {
                        sLclient.Self.AutoPilotCancel();
                        break;
                    }
                }

                // Stop #2 - bot looks at shelf
                sLclient.Self.Movement.TurnToward(lookAtTarget2);
                Thread.Sleep(5000);

                // target position #6
                sLclient.Self.AutoPilot((double)targetPosition6.X, (double)targetPosition6.Y, (double)targetPosition6.Z);
                while (currentPosition.Y < targetPosition6.Y)
                {
                    Thread.Sleep(0);
                    currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                    //when bot is within target distance, cancel auto pilot and exit loop
                    if (currentPosition.Y >= targetPosition6.Y + TARGET_DISTANCE)
                    {
                        sLclient.Self.AutoPilotCancel();
                        break;
                    }
                }
                //Walks to target position #1
                sLclient.Self.AutoPilot((double)targetPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
                while (currentPosition.X < targetPosition1.X)
                {
                    Thread.Sleep(0);
                    currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                    //when bot is within target distance, cancel auto pilot and exit loop
                    if (currentPosition.X >= targetPosition1.X + TARGET_DISTANCE)
                    {
                        sLclient.Self.AutoPilotCancel();
                        break;
                    }
                }
                if (CheckOut > 1)
                {
                    NimonCheckOut(sLclient, CheckOut);
                    break;
                }
            }
        }


        //Function that determines the movement path of Nimon for the 'checkout' process
        public int NimonCheckOut(GridClient client, int Status)
        {
            CheckOutPosition1 = new Vector3(288864.20F, 294591.80F, 25F);
            CheckOutPosition2 = new Vector3(288869.50F, 294591.20F, 25F);
            CheckOutPosition3 = new Vector3(288869.80F, 294604.30F, 25F);
            CheckOutPosition4 = new Vector3(288849.6F, 294604.30F, 25F);
            CheckOutPosition5 = new Vector3(288841.70F, 294595.20F, 25);
            Teleport1 = new Vector3(288869.50F, 294591.20F, 25F);
            lookAtTarget = new Vector3(95.70F, 192.90F, 26F);

                Status = CheckOut;

            switch (CheckOut)
            {
                case 1:

                    //Walks to CheckoutPosition #1
                    sLclient.Self.AutoPilot((double)CheckOutPosition1.X, (double)targetPosition1.Y, (double)targetPosition1.Z);
                    while (currentPosition.X < CheckOutPosition1.X)
                    {
                        Thread.Sleep(0);
                        currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                        //when bot is within target distance, cancel auto pilot and exit loop
                        if (currentPosition.X >= CheckOutPosition1.X + TARGET_DISTANCE)
                        {
                            sLclient.Self.AutoPilotCancel();
                            break;
                        }
                    }

                    //Checkout counter, looks at Oriana.
                    sLclient.Self.Movement.TurnToward(lookAtTarget);

                    return CheckOut;

                case 2:

                   sLclient.Self.Chat("Yes, thank you.",0,ChatType.Normal);

                    return CheckOut;

                case 3:

                    sLclient.Self.Chat("Here you go, $10.",0,ChatType.Normal);
                    //animation?

                    return CheckOut;

                case 4:

                    sLclient.Self.Chat("Thank you! You too!",0,ChatType.Normal);

                    //attach grocery bag & walk out
                    //Walks to checkout position #2 /
                    sLclient.Self.AutoPilot((double)CheckOutPosition2.X, (double)CheckOutPosition2.Y, (double)CheckOutPosition2.Z);
                    while (currentPosition.X < CheckOutPosition2.X)
                    {
                        Thread.Sleep(0);
                        currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                        //when bot is within target distance, cancel auto pilot and exit loop
                        if (currentPosition.X >= CheckOutPosition2.X + TARGET_DISTANCE)
                        {
                            sLclient.Self.AutoPilotCancel();
                            break;
                        }
                    }

                    //Walks to checkout position #3
                    sLclient.Self.AutoPilot((double)CheckOutPosition3.X, (double)CheckOutPosition3.Y, (double)CheckOutPosition3.Z);
                    while (currentPosition.X < CheckOutPosition3.X)
                    {
                        Thread.Sleep(0);
                        currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                        //when bot is within target distance, cancel auto pilot and exit loop
                        if (currentPosition.X >= CheckOutPosition3.X + TARGET_DISTANCE)
                        {
                            sLclient.Self.AutoPilotCancel();
                            break;
                        }
                    }

                    //Walks to checkout position #4
                    sLclient.Self.AutoPilot((double)CheckOutPosition4.X, (double)CheckOutPosition4.Y, (double)CheckOutPosition4.Z);
                    while (currentPosition.X < CheckOutPosition4.X)
                    {
                        Thread.Sleep(0);
                        currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                        //when bot is within target distance, cancel auto pilot and exit loop
                        if (currentPosition.X >= CheckOutPosition4.X + TARGET_DISTANCE)
                        {
                            sLclient.Self.AutoPilotCancel();
                            break;
                        }
                    }

                    //Walks to checkout position #5
                    sLclient.Self.AutoPilot((double)CheckOutPosition5.X, (double)CheckOutPosition5.Y, (double)CheckOutPosition5.Z);
                    while (currentPosition.X < CheckOutPosition5.X)
                    {
                        Thread.Sleep(0);
                        currentPosition = vectorConvert(sLclient.Self.RelativePosition);

                        //when bot is within target distance, cancel auto pilot and exit loop
                        if (currentPosition.X >= CheckOutPosition5.X + TARGET_DISTANCE)
                        {
                            sLclient.Self.AutoPilotCancel();
                            break;
                        }
                    }

                    CheckOut = 0;
                    return CheckOut;

                default:

                    sLclient.Self.Chat("Code fell through to default", 0, ChatType.Normal);
                    CheckOut = 0;
                    return CheckOut;
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

           /* UUID GroceryBag = New UUID("51b731db-5626-73c3-0f4c-d333e77ae268");
            //At this point, the checkout animation and dialogue will take place*/