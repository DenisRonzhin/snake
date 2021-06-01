using System;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Program
    {

        static void Main(string[] args)
        {


            ConsoleKeyInfo ConsoleKeyInf = new ConsoleKeyInfo();    
         
            //Основной цикл программы
            while (true)
                {
                    //Определим размеры игрового поля
                    SetupWindowSize.height = 30;
                    SetupWindowSize.wight = 100;
                    SetupWindowSize.SetupWindow();        

                    CurrentValue.appleCount = 0;
                    CurrentValue.speedSnake = 1;    


                    Snake mysnake = new Snake(30,10,3,'@',Snake.pointer_.stop,1);
                
                    Wall mywall = new Wall('#');

                    mywall.drawWall();    
            
                    Apple app = new Apple();

                    //Инициализация змейки.
                    mysnake.init();
                    mysnake.showsnake(true);

                    app.createApple();
                    app.showApple();

                    Task delayTimer = Task.CompletedTask;
                   

                  
                    while (true) 
                    {



                        app.showApple();
        
                        if (Console.KeyAvailable)
                        {
                      
                           ConsoleKeyInf = Console.ReadKey();


                        }

         
                        if (!delayTimer.IsCompleted) continue;

        
                            switch(ConsoleKeyInf.Key)
        
                            { 
                                case ConsoleKey.UpArrow:    {if (mysnake.pointer != Snake.pointer_.down) {mysnake.pointer = Snake.pointer_.up;} break;}
                                case ConsoleKey.DownArrow:  {if (mysnake.pointer != Snake.pointer_.up) {mysnake.pointer = Snake.pointer_.down;} break;}
                                case ConsoleKey.LeftArrow:  {if (mysnake.pointer != Snake.pointer_.right) {mysnake.pointer = Snake.pointer_.left;} break;}
                                case ConsoleKey.RightArrow: {if (mysnake.pointer != Snake.pointer_.left) {mysnake.pointer = Snake.pointer_.right;} break;}
                                case ConsoleKey.PageUp:     {mysnake.increeaseSpeed(); break;}
                                case ConsoleKey.PageDown:   {mysnake.reduceSpeed(); break;}
                        

                            }    



                        delayTimer  = delayGame(mysnake.listSpeed[mysnake.currentSpeed].spd_value);                        

                        mysnake.showsnake(false); 
                        mysnake.movesnake();
                        mysnake.showsnake(true);
                        if (mysnake.checkGameOver()) break;
                        if (mysnake.checkApple()) {app.hideApple(); mysnake.addPoint(); app.createApple();}  

                        //Thread.Sleep(mysnake.listSpeed[mysnake.currentSpeed].spd_value);


                    }

                    Console.ReadKey();

                    app.Dispose();

                    
                }
        }

         static async Task delayGame(int spd_value)
        {
            await Task.Delay(spd_value);


        }
    }
}
