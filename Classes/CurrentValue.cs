namespace SnakeGame
{
    using System;
    
     static public class CurrentValue
    {
      
        static public int apple_x {get;set;}
        static public int apple_y {get;set;}
        
        static public int appleCount {get; set;}

        static public int speedSnake {get; set;}

        static public int tail_x{get; set;}
        static public int tail_y{get; set;}

        static public void showIndicators()
        {
              Console.SetCursorPosition(SetupWindowSize.wight-15,1);
              Console.Write($"Apple: {appleCount}"); 

              Console.SetCursorPosition(SetupWindowSize.wight-30,1);
              Console.Write($"Speed: {speedSnake}"); 

        }


    }

}