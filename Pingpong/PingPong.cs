using System;
using System.Threading;

class PingPong
{

    static int firstPadsize = 4;
    static int secondPadsize = 4;

    static int ballPosX = 0;
    static int ballPosY = 0;

    static bool ballDirectionUp = true;
    static bool ballDirectionRight = true;

    static int firstPlayerY = 0;
    static int secondPlayerY = 0;

    static int firstPlayerPoints = 0;
    static int secondPlayerPoints = 0;

    static int speed = 60;
    static int tmp = 0;

    static void RemoveScrollbars() {
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
        Console.ForegroundColor = ConsoleColor.White;
    }

    static void SetInitialPos() 
    {
        firstPlayerY = Console.WindowHeight / 2 - firstPadsize / 2;
        secondPlayerY = Console.WindowHeight / 2 - secondPadsize / 2;
        ballPosX = Console.WindowWidth / 2;
        ballPosY = Console.WindowHeight / 2;
    }

    static void PrintAtPosition(int x, int y, char c)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(c);
    }

    static void DrawFirstPlayer()
    {
        for (int y = firstPlayerY; y < firstPadsize + firstPlayerY; y++)
        {
            PrintAtPosition(0, y, '|');
            PrintAtPosition(1, y, '|');
        }
    }

    static void DrawSecontPlayer()
    {
        for (int y = secondPlayerY; y < secondPadsize + secondPlayerY; y++)
        {
            PrintAtPosition(Console.WindowWidth - 1, y, '|');
            PrintAtPosition(Console.WindowWidth - 2, y, '|');
        }
    }

    static void DrawBall()
    {
        PrintAtPosition(ballPosX, ballPosY, '*');
    }

    static void PrintResult()
    {
        Console.SetCursorPosition(Console.WindowWidth / 2 - 1, 0);
        Console.Write("{0}-{1}", firstPlayerPoints, secondPlayerPoints);
    }

    static void MoveFistPlayerUp()
    {
        if (firstPlayerY > 0)
        {
            firstPlayerY--;
        }
    }

    static void MoveFirstPlayerDown()
    {
        if (firstPlayerY + firstPadsize < Console.WindowHeight)
        {
            firstPlayerY++;
        }
    }

    static void MoveSecondPlayerUp()
    {
        if (secondPlayerY > 0)
        {
            secondPlayerY--;
        }
    }

    static void MoveSecondPlayerDown()
    {
        if (secondPlayerY + secondPadsize < Console.WindowHeight)
        {
            secondPlayerY++;
        }
    }

    static void MoveBall()
    {
        if (ballPosY == 0)
        {
            ballDirectionUp = false;
        }

        if (ballPosY == Console.WindowHeight - 1)
        {
            ballDirectionUp = true;
        }

        if (ballPosX == Console.WindowWidth - 1)
        {
            SetInitialPos();
            ballDirectionRight = false;
            ballDirectionUp = true;
            firstPlayerPoints++;
            tmp = 0;
            speed = 60;
            Console.ReadKey();
        }

        if (ballPosX == 0)
        {
            SetInitialPos();
            ballDirectionRight = true;
            ballDirectionUp = false;
            secondPlayerPoints++;
            tmp = 0;
            speed = 60;
            Console.ReadKey();
        }

        if (ballPosX <= 3)
        {
            if (ballPosY >= firstPlayerY && ballPosY < firstPlayerY + firstPadsize)
            { 
                ballDirectionRight = true;
            }
        }

        if (ballPosX >= Console.WindowWidth - 3 - 1)
        {
            if (ballPosY >= secondPlayerY && ballPosY < secondPlayerY + secondPadsize)
            { 
                ballDirectionRight = false;
            }
        }

        if (ballDirectionUp)
        {
            ballPosY--;
        }
        else
        {
            ballPosY++;
        }

        if (ballDirectionRight)
        {
            ballPosX++;
        }
        else
        {
            ballPosX--;
        }
    }

    static void IncreaseSpeed()
    {
        if (tmp > 50)
        {
            tmp = 0;
            speed--;
        }
    }

    static void Main()  
    {
        RemoveScrollbars();

        SetInitialPos();

        while (true)
        {

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();

                while (Console.KeyAvailable) Console.ReadKey(true);

                if(pressedKey.Key == ConsoleKey.W)
                {
                    MoveFistPlayerUp();
                }
                else if (pressedKey.Key == ConsoleKey.S)
                {
                    MoveFirstPlayerDown();
                }

                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    MoveSecondPlayerUp();
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    MoveSecondPlayerDown();
                }

                //while (true) Console.ReadKey(true);
            }


            Console.Clear();

            DrawFirstPlayer();

            DrawSecontPlayer();

            PrintResult();

            MoveBall();

            DrawBall();

            IncreaseSpeed();

            Thread.Sleep(speed);

            tmp++;
        }
    }
}
