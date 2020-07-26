using static System.Console;
using System;
using System.Linq;
using static System.ConsoleKey;
using System.Drawing;

enum Current
{
    Front,
    Last
}
class Runner
{
    private Current Room = Current.Front;
    private Random random;
    private Color[,,] plane;
    private int x, y, z;
    private int score = 0;
    public int obsticleRange = 10;

    private static readonly int[] cColors =
            {
                0x000000, 0x000080, 0x008000, 0x008080, 0x800000, 0x800080, 0x808000, 0xC0C0C0,
                0x808080, 0x0000FF, 0x00FF00, 0x00FFFF,
                0xFF0000, 0xFF00FF, 0xFFFF00, 0xFFFFFF
            };
    public Runner()
    {

        y = 30;
        x = 30;
        z = 30;
        plane = new Color[x, y, z];


        for (int i = 0; i < y; i++)
            for (int j = 0; j < x; j++)
                for (int k = 0; k < z; k++)
                    if (k == 0)
                        plane[i, j, k] = Color.Green;
                    else if (k == z - 1)
                        plane[i, j, k] = Color.Red;

        Play();

    }
    public void Play()
    {

        var color = Color.Blue;
        int playerAtX = 0;
        int playerAtY = 0;
        int playerAtZ = 0;
        random = new Random();

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                WritePixel(plane[i, j, 0]);
            }
            WriteLine();
        }

        ResetColor();

        while (true)
        {

            switch (ReadKey().Key)
            {

                case F:
                    Room = Current.Front;
                    SwipFront();
                    if (playerAtZ == 0)
                    {
                        var getPrevColor = plane[playerAtX, playerAtY, playerAtZ];
                        WriteAt(playerAtX, playerAtY, getPrevColor);
                        WriteAt(playerAtX, playerAtY, color);
                    }
                    break;


                case S:
                    Room = Current.Last;
                    SwipLast();
                    if (playerAtZ == z - 1)
                    {
                        var getPrevColor = plane[playerAtX, playerAtY, playerAtZ];
                        WriteAt(playerAtX, playerAtY, getPrevColor);
                        WriteAt(playerAtX, playerAtY, color);
                    }
                    break;
                case Enter:
                    if (playerAtZ == (z - 1))//wall
                    {
                        Kill();
                        continue;
                    }
                    else
                    {
                        if (playerAtZ == 0)
                        {
                            var getPrevColor = plane[playerAtX, playerAtY, playerAtZ];
                            WriteAt(playerAtX, playerAtY, getPrevColor);
                        }

                        playerAtZ++;
                    }
                    break;
                case Home:
                    if (playerAtZ == 0)//wall
                    {
                        Kill();
                        continue;
                    }
                    else
                    {
                        if (playerAtZ == 1)
                        {
                            var getPrevColor = plane[playerAtX, playerAtY, playerAtZ];
                            WriteAt(playerAtX, playerAtY, getPrevColor);
                            WriteAt(playerAtX, playerAtY, color);
                        }
                        playerAtZ--;
                    }

                    break;
                case RightArrow:
                    if (playerAtX == (x - 1))//wall
                    {
                        Kill();
                        continue;
                    }
                    else
                    {
                        playerAtX++;

                        if (playerAtZ == 0)
                        {
                            var getPrevColor = plane[playerAtX - 1, playerAtY, 0];
                            WriteAt(playerAtX - 1, playerAtY, getPrevColor);
                            WriteAt(playerAtX, playerAtY, color);
                        }

                        if (Room == Current.Last)
                        {
                            if (playerAtZ == z - 1)
                            {
                                var getPrevColor = plane[playerAtX - 1, playerAtY, z - 1];
                                WriteAt(playerAtX - 1, playerAtY, getPrevColor);
                                WriteAt(playerAtX, playerAtY, color);
                            }
                        }


                    }
                    break;

                case LeftArrow:
                    if (playerAtX == 0)//wall
                    {
                        Kill();
                        continue;
                    }
                    else
                    {
                        playerAtX--;

                        if (playerAtZ == 0)
                        {
                            var getPrevColor = plane[playerAtX + 1, playerAtY, 0];
                            WriteAt(playerAtX + 1, playerAtY, getPrevColor);
                            WriteAt(playerAtX, playerAtY, color);
                        }
                        if (Room == Current.Last)
                        {
                            if (playerAtZ == z - 1)
                            {
                                var getPrevColor = plane[playerAtX + 1, playerAtY, z - 1];
                                WriteAt(playerAtX + 1, playerAtY, getPrevColor);
                                WriteAt(playerAtX, playerAtY, color);
                            }
                        }

                    }

                    break;

                case DownArrow:
                    if (playerAtY == (y - 1))//wall
                    {
                        Kill();
                        continue;
                    }
                    else
                    {
                        playerAtY++;
                        if (playerAtZ == 0)
                        {
                            var getPrevColor = plane[playerAtX, playerAtY - 1, 0];
                            WriteAt(playerAtX, playerAtY - 1, getPrevColor);
                            WriteAt(playerAtX, playerAtY, color);
                        }

                        if (Room == Current.Last)
                        {
                            if (playerAtZ == z - 1)
                            {
                                var getPrevColor = plane[playerAtX, playerAtY - 1, z - 1];
                                WriteAt(playerAtX, playerAtY - 1, getPrevColor);
                                WriteAt(playerAtX, playerAtY, color);
                            }
                        }

                    }
                    break;

                case UpArrow:
                    if (playerAtY == 0)//wall
                    {
                        Kill();
                        continue;
                    }
                    else
                    {
                        playerAtY--;
                        if (playerAtZ == 0)
                        {
                            var getPrevColor = plane[playerAtX, playerAtY + 1, 0];
                            WriteAt(playerAtX, playerAtY + 1, getPrevColor);
                            WriteAt(playerAtX, playerAtY, color);
                        }

                        if (Room == Current.Last)
                        {
                            if (playerAtZ == z - 1)
                            {
                                var getPrevColor = plane[playerAtX, playerAtY + 1, z - 1];
                                WriteAt(playerAtX, playerAtY + 1, getPrevColor);
                                WriteAt(playerAtX, playerAtY, color);
                            }
                        }


                    }
                    break;
            }


            Console.WriteLine(playerAtX + " " + playerAtY + " " + playerAtZ);
        }
    }


    private void SwipLast()
    {
        Clear();

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                WritePixel(plane[i, j, z - 1]);
            }
            WriteLine();
        }

    }

    private void SwipFront()
    {
        Clear();

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                WritePixel(plane[i, j, 0]);
            }
            WriteLine();
        }

    }


    private void Kill()
    {
        Clear();
        Write("over: " + score);
        WriteLine();
        Write("You want to share the game?");
        ReadLine();
    }

    public static void WriteAt(int left, int top, Color color)
    {
        int currentLeft = CursorLeft,
            currentTop = CursorTop;
        CursorVisible = false;
        SetCursorPosition(left, top);
        WritePixel(color);
        SetCursorPosition(currentLeft, currentTop);

    }
    public static void WritePixel(Color cValue)
    {
        Color[] cTable =
            cColors.Select(x => Color.FromArgb(x)).ToArray();

        char[] rList =
            new char[] { (char)9617, (char)9618, (char)9619, (char)9608 };

        int[] bestHit =
            new int[] { 0, 0, 4, int.MaxValue };

        for (int rChar = rList.Length; rChar > 0; rChar--)
        {
            for (int cFore = 0; cFore < cTable.Length; cFore++)
            {
                for (int cBack = 0; cBack < cTable.Length; cBack++)
                {
                    int R = (cTable[cFore].R * rChar + cTable[cBack].R * (rList.Length - rChar)) / rList.Length;
                    int G = (cTable[cFore].G * rChar + cTable[cBack].G * (rList.Length - rChar)) / rList.Length;
                    int B = (cTable[cFore].B * rChar + cTable[cBack].B * (rList.Length - rChar)) / rList.Length;
                    int iScore = (cValue.R - R) * (cValue.R - R) + (cValue.G - G) * (cValue.G - G) + (cValue.B - B) * (cValue.B - B);
                    if (!(rChar > 1 && rChar < 4 && iScore > 50000))
                    {
                        if (iScore < bestHit[3])
                        {
                            bestHit[3] = iScore;
                            bestHit[0] = cFore;
                            bestHit[1] = cBack;
                            bestHit[2] = rChar;
                        }
                    }
                }
            }
        }

        ForegroundColor = (ConsoleColor)bestHit[0];
        BackgroundColor = (ConsoleColor)bestHit[1];
        Write(rList[bestHit[2] - 1]);
    }
}
