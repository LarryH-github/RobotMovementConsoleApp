using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/*** Test data found after all the code (at the bottom) ***/

namespace RobotMovement
{
    class Program
    {
        static void Main(string[] args)
        {
            var quit = false; //Flag to exit the application
            var directionListLength = Enum.GetNames(typeof(Enums.DirectionList)).Length;
            var movementList = new int[] { 1, 1, -1, -1 };
            Robot robot = new Robot
            {
                XAxis = 0,
                YAxis = 0,
                IndexCounter = 0,
                MovementValue = 0, //This will change to be one of the values in movementList after robot is successfully placed at the start of the application
                MoveAlongX = false
            };

            Console.WriteLine("Hello! Please enter a PLACE command to place the robot and get started. You may also enter QUIT at anytime to stop the application");
            while (!quit)
            {
                string command;
                while (robot.MovementValue == 0)
                {
                    Console.Write("\nPlease place the robot (PLACE command): ");
                    command = Console.ReadLine();
                    updateDetails(RobotPlacement.VerifyPlacement(command, movementList, robot));
                }

                Console.WriteLine();
                Console.Write("Please enter a command: ");
                command = Console.ReadLine();
                switch (command)
                {
                    case string c when command.StartsWith("PLACE"): //Since PLACE command comes with parameters, just check for the word first
                        updateDetails(RobotPlacement.VerifyPlacement(command, movementList, robot));
                        break;
                    case "MOVE":
                        if (robot.MoveAlongX)
                        {
                            if ((robot.XAxis > 0 && robot.MovementValue < 0) || (robot.XAxis < 4 && robot.MovementValue > 0))
                            {
                                robot.XAxis += robot.MovementValue;
                            }
                        }
                        else
                        {
                            if ((robot.YAxis > 0 && robot.MovementValue < 0) || (robot.YAxis < 4 && robot.MovementValue > 0))
                            {
                                robot.YAxis += robot.MovementValue;
                            }
                        }
                        break;
                    case "LEFT":
                        robot.IndexCounter = robot.IndexCounter == 0 ? directionListLength-1 : robot.IndexCounter -1;
                        robot.MovementValue = movementList[robot.IndexCounter];
                        robot.MoveAlongX = !robot.MoveAlongX;
                        break;
                    case "RIGHT":
                        robot.IndexCounter = robot.IndexCounter == directionListLength-1 ? 0 : robot.IndexCounter +1;
                        robot.MovementValue = movementList[robot.IndexCounter];
                        robot.MoveAlongX = !robot.MoveAlongX;
                        break;
                    case "REPORT":
                        Console.WriteLine($"{robot.XAxis},{robot.YAxis},{(Enums.DirectionList)robot.IndexCounter}");
                        break;
                    case "QUIT":
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Error: Invalid command. Please enter (in uppercase) either MOVE, LEFT, RIGHT, REPORT, or PLACE with the appropriate parameters. You may also enter QUIT to stop the application.");
                        break;
                }
            }

            void updateDetails(Robot details)
            {
                robot.XAxis = details.XAxis;
                robot.YAxis = details.YAxis;
                robot.IndexCounter = details.IndexCounter;
                robot.MovementValue = details.MovementValue;
                robot.MoveAlongX = details.MoveAlongX;
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
