using System;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    class Program
    {

        static void Main(string[] args)
        {

            //Основной цикл программы
            while (true)
                {

                    //Фиксируем время нажатия на клавишу    
                    long keyPressTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                    //Фиксируем текущее время                       
                    long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();



                    ConsoleKeyInfo ConsoleKeyInf = new ConsoleKeyInfo();    
                    ConsoleKeyInfo LastKeyInf = new ConsoleKeyInfo();    
    
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
                 
                            keyPressTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                 
                            switch(ConsoleKeyInf.Key)
    
                            { 
                                case ConsoleKey.UpArrow:    
                                        {
                                            if (LastKeyInf.Key == ConsoleKeyInf.Key && mysnake.pointer == Snake.pointer_.up) {mysnake.increeaseSpeed(); }
                                            //if (mysnake.pointer == Snake.pointer_.down) {mysnake.reduceSpeed();}
                                            break;
                                        }
                                case ConsoleKey.DownArrow:
                                        {
                                            if (LastKeyInf.Key == ConsoleKeyInf.Key && mysnake.pointer == Snake.pointer_.down) {mysnake.increeaseSpeed();}
                                            //if (mysnake.pointer == Snake.pointer_.up) {mysnake.reduceSpeed();}
                                            break;
                                        }
                                case ConsoleKey.LeftArrow:
                                        {    
                                            if (LastKeyInf.Key == ConsoleKeyInf.Key && mysnake.pointer == Snake.pointer_.left) {mysnake.increeaseSpeed();}
                                            //if (mysnake.pointer == Snake.pointer_.right) {mysnake.reduceSpeed();}
                                            break;
                                        }    
                                case ConsoleKey.RightArrow: 
                                        {
                                             
                                            if (LastKeyInf.Key == ConsoleKeyInf.Key && mysnake.pointer == Snake.pointer_.right) {mysnake.increeaseSpeed();}
                                            //if (mysnake.pointer == Snake.pointer_.left) {mysnake.reduceSpeed();}
                                            break;
                                        }
    
                              
                                            
                            } 

                            LastKeyInf = ConsoleKeyInf; 

                        }; 

                         
                        currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                        //Если текущее время отличается от времени нажатия на кнопку более чем на 70 мсек, значит кнопку отпустили

                        if (currentTime - keyPressTime >  70) mysnake.reduceSpeed();
                    
                        if (!delayTimer.IsCompleted) continue; // если таймер не сработал, продолжаем цикл

    
                        switch(ConsoleKeyInf.Key)
    
                        { 
                            case ConsoleKey.UpArrow:    
                                    {
                                        if (mysnake.pointer != Snake.pointer_.down) {mysnake.pointer = Snake.pointer_.up;} 
                                        break;
                                    }
                            case ConsoleKey.DownArrow:
                                    {
                                        if (mysnake.pointer != Snake.pointer_.up) {mysnake.pointer = Snake.pointer_.down;} 
                                        break;
                                    }
                            case ConsoleKey.LeftArrow:
                                    {
                                        if (mysnake.pointer != Snake.pointer_.right) {mysnake.pointer = Snake.pointer_.left;} 
                                        break;
                                    }
                            case ConsoleKey.RightArrow: 
                                    {
                                        if (mysnake.pointer != Snake.pointer_.left) {mysnake.pointer = Snake.pointer_.right;} 
                                        break;
                                    }

                        

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
