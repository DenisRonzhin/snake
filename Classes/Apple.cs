using System;

namespace SnakeGame
{
    
    public class Apple:IDisposable
        {
            public struct appPointer
            {public int x,y;}

            public appPointer appPointer_ {get; set;} //свойство с координатами яблока

            appPointer appPoint = new appPointer();

            //Метод для создания яблока на поле
            public void createApple()
            {
            Random randomX = new Random();
            Random randomY = new Random();

            appPoint.x = randomX.Next(4,SetupWindowSize.wight-4);
            appPoint.y = randomY.Next(4,SetupWindowSize.height-4);

            CurrentValue.apple_x = appPoint.x;
            CurrentValue.apple_y = appPoint.y;
    
            }

            //Метод для отрисовки яблока на поле
            public void showApple()
            {   
    
            Console.SetCursorPosition(appPoint.x,appPoint.y);
            Console.Write('X');    
        
    
            }    
    
            public void hideApple()
            {   
    
            Console.SetCursorPosition(appPoint.x,appPoint.y);
            Console.Write(' ');    
        
            }

            public void Dispose()
            {
            }    
        }
}