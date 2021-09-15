using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace RobotMovement
{
    class Robot
    {
        [Required]
        public int XAxis { get; set; }
        [Required]
        public int YAxis { get; set; }
        [Required]
        public int IndexCounter { get; set; }
        [Required(ErrorMessage = "Requireddddd")]
        public int MovementValue { get; set; }
        [Required]
        public bool MoveAlongX { get; set; }
    }
}
