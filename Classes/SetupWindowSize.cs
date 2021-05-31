
namespace SnakeGame
{
    using System;
     
     //Класс определяет размеры игрового поля
     static public class SetupWindowSize 
    {   
       
        static int x = Console.LargestWindowWidth;
        static int y = Console.LargestWindowHeight;

        static public int wight
        {
           get{return x;}
           set{x = value;}     
        }

        static public int height 
        {
            get{return y;}
            set{y = value;}

        }        
        
   
         static public void SetupWindow()
         {
          
            Console.Clear();    
            Console.SetWindowSize(x, y); 
            Console.CursorVisible = false;
         }   
    }

}