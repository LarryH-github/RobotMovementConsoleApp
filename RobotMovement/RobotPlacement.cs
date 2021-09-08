using System;
using System.Collections.Generic;
using System.Text;

namespace RobotMovement
{
    class RobotPlacement
    {
        public static MovementDetails VerifyPlacement(string input, string[] directionList, int[] movementList, MovementDetails oldDetails)
        {
            var inputStrings = input.Split(' ');
            if (inputStrings.Length == 2 && inputStrings[0] == "PLACE")
            {
                var options = inputStrings[1].Split(',');
                if (!(options.Length == 3))
                {
                    Console.WriteLine("Error placing robot: Invalid 'PLACE' command. Please use all upper case, have a space between the word 'PLACE' and the parameters, and have no spaces in the parameters. Example: PLACE 0,0,NORTH");
                    return oldDetails;
                }

                if (!(Int32.TryParse(options[0], out int x) && Int32.TryParse(options[1], out int y)))
                {
                    Console.WriteLine("Error placing robot: Please use whole numbers for the coordinate values.");
                    return oldDetails;
                }

                if (!(x >= 0 && x <= 4 && y >= 0 && y <= 4))
                {
                    Console.WriteLine("Error placing robot: Please provide coordinate values between 0 and 4.");
                    return oldDetails;
                }

                if (!(Array.Exists(directionList, d => d == options[2])))
                {
                    Console.WriteLine("Error placing robot: Please ensure orientation is one of NORTH, EAST, SOUTH, WEST (all upper case).");
                    return oldDetails;
                }

                //xAxis = x;
                //yAxis = y;
                var counter = Array.IndexOf(directionList, options[2]);
                //movementValue = movementList[indexCounter];
                //moveAlongX = indexCounter % 2 == 0 ? false : true;
                MovementDetails newDetails = new MovementDetails
                {
                    xAxis = x,
                    yAxis = y,
                    indexCounter = counter,
                    movementValue = movementList[counter],
                    moveAlongX = counter % 2 == 0
                };
                Console.WriteLine("Robot has been placed!");

                return newDetails;
            }
            Console.WriteLine("Error placing robot: Invalid 'PLACE' command. Please use all upper case and have a space between the word 'PLACE' and the parameters, but have no spaces between the parameters themselves. Example: PLACE 0,0,NORTH");
            return oldDetails;
        }
    }

    public class MovementDetails
    {
        public int xAxis { get; set; }
        public int yAxis { get; set; }
        public int indexCounter { get; set; }
        public int movementValue { get; set; }
        public bool moveAlongX { get; set; }
    }
}
