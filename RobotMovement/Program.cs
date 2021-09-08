using System;

/*** Test data found after all the code (at the bottom) ***/

namespace RobotMovement
{
    class Program
    {
        private readonly RobotPlacement roboPlacement;
        static void Main(string[] args)
        {
            var quit = false; //Flag to exit the application
            var directionList = new string[] { "NORTH", "EAST", "SOUTH", "WEST" };
            var movementList = new int[] { 1, 1, -1, -1 };
            var movementValue = 0; //This will change to be one of the values in movementList after robot is successfully placed at the start of the application
            var indexCounter = 0;
            var xAxis = 0;
            var yAxis = 0;
            bool moveAlongX = false;
            var command = "";
            MovementDetails currentDetails = new MovementDetails
            {
                xAxis = xAxis,
                yAxis = yAxis,
                indexCounter = indexCounter,
                movementValue = movementValue,
                moveAlongX = moveAlongX
            };

            Console.WriteLine("Hello! Please enter a PLACE command to place the robot and get started. You may also enter QUIT at anytime to stop the application");
            while (!quit)
            {
                while (movementValue == 0)
                {
                    Console.Write("\nPlease place the robot (PLACE command): ");
                    command = Console.ReadLine();
                    updateDetails(RobotPlacement.VerifyPlacement(command, directionList, movementList, currentDetails));
                }

                Console.WriteLine();
                Console.Write("Please enter a command: ");
                command = Console.ReadLine();
                switch (command)
                {
                    case string c when command.StartsWith("PLACE"): //Since PLACE command comes with parameters, just check for the word first
                        updateDetails(RobotPlacement.VerifyPlacement(command, directionList, movementList, currentDetails));
                        break;
                    case "MOVE":
                        if (moveAlongX)
                        {
                            if ((xAxis > 0 && movementValue < 0) || (xAxis < 4 && movementValue > 0))
                            {
                                xAxis += movementValue;
                            }
                        }
                        else
                        {
                            if ((yAxis > 0 && movementValue < 0) || (yAxis < 4 && movementValue > 0))
                            {
                                yAxis += movementValue;
                            }
                        }
                        break;
                    case "LEFT":
                        indexCounter = indexCounter == 0 ? directionList.Length-1 : indexCounter-1;
                        movementValue = movementList[indexCounter];
                        moveAlongX = !moveAlongX;
                        break;
                    case "RIGHT":
                        indexCounter = indexCounter == directionList.Length-1 ? 0 : indexCounter+1;
                        movementValue = movementList[indexCounter];
                        moveAlongX = !moveAlongX;
                        break;
                    case "REPORT":
                        Console.WriteLine($"{xAxis},{yAxis},{directionList[indexCounter]}");
                        break;
                    case "QUIT":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid command. Please enter (in uppercase) either MOVE, LEFT, RIGHT, REPORT, or PLACE with the appropriate parameters. You may also enter QUIT to stop the application.");
                        break;
                }
            }

            void updateDetails(MovementDetails details)
            {
                xAxis = details.xAxis;
                yAxis = details.yAxis;
                indexCounter = details.indexCounter;
                movementValue = details.movementValue;
                moveAlongX = details.moveAlongX;

                currentDetails.xAxis = details.xAxis;
                currentDetails.yAxis = details.yAxis;
                currentDetails.indexCounter = details.indexCounter;
                currentDetails.movementValue = details.movementValue;
                currentDetails.moveAlongX = details.moveAlongX;
            }
        }

    }
}

/***TEST DATA***
 * Note: QUIT can be entered at anytime to stop the application

  Inputs and Outputs:
  a)-------------------
  PLACE 0,0,NORTH
  MOVE
  REPORT

  Output: 0,1,NORTH

  b)-------------------
  PLACE 0,0,NORTH
  LEFT
  MOVE
  LEFT
  MOVE
  REPORT

  Output: 0,0,SOUTH

  c)-------------------
  PLACE 4,3,NORTH
  MOVE
  RIGHT
  MOVE
  MOVE
  REPORT

  Output: 4,4,EAST

  d)-------------------
  PLACE 3,1,SOUTH
  MOVE
  LEFT
  MOVE
  PLACE 0,3,NORTH
  REPORT

  Output: 0,3,NORTH

  e)-------------------
  PLACE 0,0,NORTH
  MOVE
  PLACE 2,2,WEST
  LEFT
  MOVE
  REPORT

  Output: 2,1,SOUTH
 
  f)-------------------
  PLACE 0,0,NORTHWEST

  Output: Custom error message will pop up and user will be prompted again to enter a valid
          command (PLACE command will be specified if this was the starting/first command)

  g)-------------------
  PLACE 0,0,NORTH
  RUN

  Output: Custom error message will pop up and user will be prompted again to enter a valid command,
          robot will stay in the position before invalid command was entered

  h)-------------------
  PLACE 0,0,NORTH
  RUN
  MOVE
  PLACE 10,-1,SOUTH
  REPORT

  Output: 0,1,NORTH
 
 */
