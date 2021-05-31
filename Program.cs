using System;
using System.Collections.Generic;
using System.Threading; 
using System.Media;
using System.Linq;

namespace PR_4
{
   
    //Класс определяет размеры игрового поля
    
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
              Console.Write($"Speed: {speedSnake/100}"); 

        }


    }
    
    
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

    public class Point
    {


        public Point(int x,int y,Char smb)
        {
            xx = x;
            yy = y;
            symb = smb;

        }    


        int xx; 
        public int x
        {
            get {return xx;}
            set {xx = value;}
        } // координаты точки по x
        
        int  yy;
        public int y
        {
           get{return yy;}
           set{yy = value;}

        } // координаты точки по y
         

        char symb; 
                public char symbol 
        {
            get{return symb;} 
            set{symb = value;}
        } // символ отрисовки точки
               
        public void drawpoint() //отобразить точку
        {
            {
                Console.SetCursorPosition(xx,yy);
                Console.Write(symb);    
            }   

        }    

        public void clearpoint() //стереть точку
        {
            {
                Console.SetCursorPosition(xx,yy);
                Console.Write(' ');    
            }   

        }    
            
    }


    public class Snake : Point 
    {
        public Snake(int x, int y, int lengh, char smb, pointer_ pointt):base(x,y,smb)
        {
            lenghtSnake = lengh;

            pointer = pointt;
        }
        
        struct position  //структура для хранения координат точки
        {public int x, y;}    

        position posit = new position();
       
        List<position> snk = new List<position>(); //Список для хранения змейки
                
        int lenghtSnake; //По умолчанию длина змейки 5
        
        
        public int length  
        { 
            get
            {
                return lenghtSnake;
            }
          
            set
            {
               lenghtSnake = value;
               
            }

        } //Свойство длина змейки
          
        
        public enum pointer_ //указатель направления движения змейки
        {
            left,right,up,down,stop
        } 

        public pointer_ pointer {get;set;} 
        
        // Метод используется для отрисовки и стрирание змейки
        // Параметр showpoint определяет варинат отрисовки. Возможны два варинта: вывод на экран змейки, стирание с экрана змейки
        public void showsnake(bool showpoint) 
        {

            for (int i = 0; i<snk.Count; i++)
            {
                x = snk[i].x;
                y = snk[i].y;

                if (showpoint) drawpoint(); else clearpoint();    
                
            }
        
            Console.CursorVisible = false;
            
        }

        //Метод добавляет точки к змейке
        public void addPoint()
        {
            lenghtSnake++;

            posit.x = CurrentValue.tail_x;
            posit.y = CurrentValue.tail_y;
            
            snk.Add(posit);

        }

        // Метод сдвигает координаты точек змейки
        public void movesnake()
        {
            //зафиксируем координаты хвоста
            CurrentValue.tail_x = snk[snk.Count-1].x;
            CurrentValue.tail_y = snk[snk.Count-1].y;
     
            if (pointer != pointer_.stop) 
            {
                //сдвигаем координаты точек змейки на один элемент, кроме головы
                for (int l=snk.Count-1; l>0; l--)
                {
                    posit.x = snk[l-1].x;
                    posit.y = snk[l-1].y;
                    snk[l] = posit; 
                }     
            }

            //сдвигаем координаты головы в зависимости от направления движения
            switch(pointer)
            {
                case pointer_.up : 
                {
                    posit.y = snk[0].y-1;
                    posit.x = snk[0].x;
                    snk[0] = posit;
                    break;
                }   

                case pointer_.down : 
                {
                    posit.y = snk[0].y+1;
                    posit.x = snk[0].x;
                    snk[0] = posit;
                    break;
                }   

                case pointer_.left : 
                {   
                    posit.y = snk[0].y;
                    posit.x = snk[0].x-1;
                    snk[0] = posit;
                    break;
                }   
                case pointer_.right : 
                {
                    posit.y = snk[0].y;
                    posit.x = snk[0].x+1;    
                    snk[0] = posit;
                    break;
                }   
            }
 
        }
     
        //Метод первоначальной инициализации змейки.
        public void init()
        {
       
            for (int i = 0; i<lenghtSnake; i++)
            {
                posit.x = x+i;
                posit.y = y;
                snk.Add(posit);
                
            }

             snk.Reverse();

            CurrentValue.showIndicators();  

        }

        
        public bool checkApple()
        {
            bool findApple = false;
            
            if (snk[0].x == CurrentValue.apple_x && snk[0].y == CurrentValue.apple_y)
            {

                //lenghtSnake++;
                CurrentValue.appleCount++;

                findApple = true;

                Console.Beep();

            }

            CurrentValue.showIndicators();        

            return findApple;
        }

        public bool checkGameOver()
        {
            bool gameOver = false;
            
            //столкновение со стенками по y
            if (snk[0].x == 0 || snk[0].x == SetupWindowSize.wight)
            {
               gameOver =  true;
            }

            //столкноввение со стенками по x
            if (snk[0].y == 2 || snk[0].y == SetupWindowSize.height)
            {
                gameOver =  true;
            }

            //закольцовка змейки

            posit.x = snk[0].x;
            posit.y = snk[0].y;    

            for (int i = 1; i<=lenghtSnake-1; i++)
            {

                if (posit.x == snk[i].x && posit.y == snk[i].y) 
                {
                    gameOver = true;     
                }

            }          

            // Func<position, bool> pred = 
            // (x) => 
            //     {
            //         return x.x == snk[0].x && x.y == snk[0].y;
            //     };

            // snk.FindAll(
            //     pred
            // );

            // snk.FindAll(
            //     (x) => 
            //     {
            //         return x.x == snk[0].x && x.y == snk[0].y;
            //     }
            // );

            if (gameOver)

            {
                Console.SetCursorPosition(SetupWindowSize.wight/2,SetupWindowSize.height/2);
                Console.Write("******** GAME OVER *********");   
  


            }

            return gameOver;
        }

    }


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
    
        //хуй
        //хуй        
        }

        public void Dispose()
        {
        }    
    }

    class Program
    {

        static void Main(string[] args)
        {


            //Основной цикл программы
            while (true)
                {
                    //Определим размеры игрового поля
                    SetupWindowSize.height = 30;
                    SetupWindowSize.wight = 100;
                    SetupWindowSize.SetupWindow();        

                    CurrentValue.appleCount = 0;
                    CurrentValue.speedSnake = 1;    


                    Snake mysnake = new Snake(30,10,3,'@',Snake.pointer_.stop);
                
                    Wall mywall = new Wall('#');

                    mywall.drawWall();    
            
                    Apple app = new Apple();

                    //Инициализация змейки.
                    mysnake.init();
                    mysnake.showsnake(true);

                    app.createApple();
                    app.showApple();

                    while (mysnake.checkGameOver() == false) 
                    {

                        mysnake.showsnake(false);

                        app.showApple();
        
                        if (Console.KeyAvailable)
                        {
                        
                            ConsoleKeyInfo ConsoleKeyInf = Console.ReadKey();

                            switch(ConsoleKeyInf.Key)
                            { 
                                case ConsoleKey.UpArrow:    {if (mysnake.pointer != Snake.pointer_.down) {mysnake.pointer = Snake.pointer_.up;} break;}
                                case ConsoleKey.DownArrow:  {if (mysnake.pointer != Snake.pointer_.up) {mysnake.pointer = Snake.pointer_.down;} break;}
                                case ConsoleKey.LeftArrow:  {if (mysnake.pointer != Snake.pointer_.right) {mysnake.pointer = Snake.pointer_.left;} break;}
                                case ConsoleKey.RightArrow: {if (mysnake.pointer != Snake.pointer_.left) {mysnake.pointer = Snake.pointer_.right;} break;}
                        
                            }    

                        }

                        mysnake.movesnake();
                        if (mysnake.checkApple()) {app.hideApple(); mysnake.addPoint(); app.createApple();}  
                        mysnake.showsnake(true);
                        Thread.Sleep(100);

                    }

                    Console.ReadKey();

                    app.Dispose();

                    
                }
        }
    }
}
