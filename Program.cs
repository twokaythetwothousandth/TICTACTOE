using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace tictactoe
{
    internal enum state
    {
        Undecided,
        X,
        O
    };
    internal class position
    {
        private state[,] pos = new state[3, 3];

        public position()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    this.pos[i, i2] = state.Undecided;
                }
            }
        }
        public state[,] getPos()
        {
            return this.pos;
        }
        public bool setPos(int row, int column, state value)
        {
            if (this.pos[row, column] == state.Undecided)
            {
                this.pos[row, column] = value;
                return true;
            }
            return false;

        }
    }
    internal class board
    {
        private position pos;

        public board(position pos)
        {
            this.pos = pos;
        }
        
        public void updateBoard()
        {
            string check(state i)
            {
                if (i == state.Undecided)
                    return " ";
                else if (i == state.X)
                    return "X";
                else if (i == state.O)
                    return "O";
                else
                    return " ";
            }

            state[,] arr = this.pos.getPos();

            Console.Clear();

            Console.WriteLine($" {check(arr[0, 0])} | {check(arr[0, 1])} | {check(arr[0, 2])} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {check(arr[1, 0])} | {check(arr[1, 1])} | {check(arr[1, 2])} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {check(arr[2, 0])} | {check(arr[2, 1])} | {check(arr[2, 2])} ");
        }
    }
    internal class player
    {
        private position pos;
        private state plr;

        public player(position pos)
        {
            this.pos = pos;

            Random random = new Random();
            int result = random.Next(1, 2);

            plr = (state)result;
        }

        public state getPlr()
        {
            return plr;
        }
        public void switchPlr()
        {
            plr = (plr == state.O) ? state.X : state.O;
        }
    }
    internal class winChecker
    {
        private position pos;

        public winChecker(position pos)
        {
            this.pos = pos;
        }
        public bool checkWin(int row, int column)
        {
            state[,] arr = pos.getPos();
            state arrState = arr[row, column];

            byte check = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    if (arr[i2, i] == state.Undecided)
                    {
                        check += 1;
                    }
                }
            }

            if (check == 0)
            {
                Console.WriteLine("\nStalemate!");
                return true;
            }
            if (arrState == state.Undecided)
                return false;

            byte vertCheck = 0;
            byte horiCheck = 0;
            byte diagUpCheck = 0;
            byte diagDownCheck = 0;

            for (int i = 0; i < 3; i++)
            {
                if (arr[row, i] == arrState)
                    vertCheck += 1;
            }
            for (int i = 0; i < 3; i++)
            {
                if (arr[i, column] == arrState)
                    horiCheck += 1;
            }
            for (int i = 0; i < 3; i++)
            {
                if (arr[i, i] == arrState)
                    diagDownCheck += 1;
            }
            for (int i = 0; i < 3; i++)
            {
                if (arr[i, 2 - i] == arrState)
                    diagUpCheck += 1;
            }
            if (vertCheck >= 3 || horiCheck >= 3 || diagUpCheck >= 3 || diagDownCheck >= 3)
            {
                Console.WriteLine($"\n{arrState} has won the round!");
                return true;
            }
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int roundsPlayed = 0;

            Console.WriteLine("Tic Tac Toe game made by twokay!");
            while (true)
            {
                Console.WriteLine($"Play{(roundsPlayed == 0 ? "" : " again")}? Say \"yes\" to play, otherwise say \"no\" or any other word to exit.");
                string result = Console.ReadLine();

                if (result.ToLower() == "yes")
                {
                    roundsPlayed += 1;

                    position gamePosition = new position();
                    board gameBoard = new board(gamePosition);
                    player gamePlr = new player(gamePosition);
                    winChecker gameWinCheck = new winChecker(gamePosition);

                    gameBoard.updateBoard();

                    int row = 0;
                    int column = 0;
                    while (!gameWinCheck.checkWin(row, column))
                    {
                        gamePlr.switchPlr();
                        Console.WriteLine($"\n{gamePlr.getPlr().ToString()}'s turn! Choose a spot (keys Q to C, Q = top-left box, C = bottom-right box)");


                        gotoRef:
                        ConsoleKeyInfo key = Console.ReadKey();

                        switch (key.Key)
                        {
                            case ConsoleKey.Q:
                                if (gamePosition.setPos(0, 0, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 0;
                                    column = 0;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.W:
                                if (gamePosition.setPos(0, 1, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 0;
                                    column = 1;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.E:
                                if (gamePosition.setPos(0, 2, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 0;
                                    column = 2;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.A:
                                if (gamePosition.setPos(1, 0, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 1;
                                    column = 0;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.S:
                                if (gamePosition.setPos(1, 1, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 1;
                                    column = 1;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.D:
                                if (gamePosition.setPos(1, 2, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 1;
                                    column = 2;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.Z:
                                if (gamePosition.setPos(2, 0, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 2;
                                    column = 0;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.X:
                                if (gamePosition.setPos(2, 1, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 2;
                                    column = 1;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            case ConsoleKey.C:
                                if (gamePosition.setPos(2, 2, gamePlr.getPlr()))
                                {
                                    gameBoard.updateBoard();
                                    row = 2;
                                    column = 2;
                                }
                                else
                                {
                                    Console.WriteLine("\n\nSpot is already taken!");
                                    goto gotoRef;
                                }
                                break;
                            default:
                                Console.WriteLine("\n\nNot a valid key! Please press a valid key.");
                                goto gotoRef;
                        }
                    }
                    Thread.Sleep(2000);
                }
                else
                {
                    Console.WriteLine("Exiting. Thanks for playing!");

                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }
}
