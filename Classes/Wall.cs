namespace SnakeGame
{
    using System;
    public class Wall
        {
            char symbol;

            public Wall(char smb)
            {
                symbol = smb;

            }
    
            public void drawWall()
            {

        
                //Верхняя панель
                Console.SetCursorPosition(SetupWindowSize.wight/2-5,1);
                Console.Write("******SNAKE*******"); 
            
                
                //верхняя и нижняя стена
                for (int x = 1; x<=SetupWindowSize.wight; x++)
                {
                    Console.SetCursorPosition(x,0);
                    Console.Write(symbol); 

                    Console.SetCursorPosition(x,2);
                    Console.Write(symbol); 

                    Console.SetCursorPosition(x,SetupWindowSize.height);
                    Console.Write(symbol); 
                }


                //горизонтальные стены

                for (int y = 0; y<=SetupWindowSize.height; y++)
                {

                    Console.SetCursorPosition(1,y);
                    Console.Write(symbol); 

                    Console.SetCursorPosition(SetupWindowSize.wight,y);
                    Console.Write(symbol); 
    
                }


            }


    }
}