namespace SnakeGame
{

     using System;
     using System.Collections.Generic;
     

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
        public Snake(int x, int y, int lengh, char smb, pointer_ pointt, int speed):base(x,y,smb)
        {
            lenghtSnake = lengh;

            pointer = pointt;

            speed_ = speed;
        }
        
        public struct speedSnk //структура для хранения скорости змейки
        { public int spd_index, spd_value;}

        public speedSnk speedSnake = new speedSnk(); 
        
        public speedSnk[] listSpeed = new speedSnk[10]; //массив доступных скоростей 

        int speed_; //текущая скорость

        public int currentSpeed // свойство текущая скорость
        {
            get{return speed_;}

            set{speed_ = value;}

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

            // List<Point> point = new List<Point>();

            // point.Add(new Point(1,2));

            // point[0].drawpoint();
        
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
            CurrentValue.speedSnake = speed_;
     
            if (pointer == pointer_.left || pointer == pointer_.right || pointer == pointer_.down || pointer == pointer_.up) 
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

            //заполним массив лоступных скоростей
            int maxDelay = 200;
            for (int i = 0; i < 10; i++)
            {
                speedSnake.spd_index = i+1;
                speedSnake.spd_value = maxDelay;
                maxDelay =  maxDelay - 20;
                listSpeed[i] = speedSnake;
            }     

    
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

                //Console.Beep();

            }

            CurrentValue.showIndicators();        

            return findApple;
        }

       

        //Увеличить скорость
        public void increeaseSpeed()
        {
             if (speed_ < 9 ) speed_++; 

        }

        //Уменьшить скорость
        public void reduceSpeed()
        {
            if (speed_>1) speed_--;

        }

      

        public bool checkGameOver()
        {
            bool gameOver = false;
            
            //столкновение со стенками по y
            if (snk[0].x == 1 || snk[0].x == SetupWindowSize.wight)
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

    
}