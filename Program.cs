using System;
using System.Collections.Generic;

class MemoryGame
{
    static void Main()
    {
        char[] symbols = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
        int gridSize = 4;
        char[,] board = InitializeBoard(gridSize, symbols);
        char[,] displayBoard = InitializeDisplayBoard(gridSize);

        int totalPairs = gridSize * gridSize / 2;
        int remainingPairs = totalPairs;
        int steps = 0;

        while (remainingPairs > 0)
        {
            DisplayBoard(displayBoard);

            int[] selections = GetPlayerSelections(gridSize);
            int selection1 = selections[0];
            int selection2 = selections[1];

            // Check if selections are valid
            if (!IsValidSelection(selection1, gridSize) || !IsValidSelection(selection2, gridSize))
            {
                Console.WriteLine("Geçersiz kart seçimi. Lütfen tekrar deneyin.");
                continue;
            }

            // Check if the same card is selected twice
            if (selection1 == selection2)
            {
                Console.WriteLine("Aynı kartı iki kez seçtiniz. Lütfen farklı kartlar seçin.");
                continue;
            }

            int row1 = (selection1 - 1) / gridSize;
            int col1 = (selection1 - 1) % gridSize;
            int row2 = (selection2 - 1) / gridSize;
            int col2 = (selection2 - 1) % gridSize;

            // Check if both cards are already open
            if (displayBoard[row1, col1] != '-' || displayBoard[row2, col2] != '-')
            {
                Console.WriteLine("Açık olan bir kartı tekrar seçtiniz. Lütfen farklı kartlar seçin.");
                continue;
            }

            // Display selected cards
            displayBoard[row1, col1] = board[row1, col1];
            displayBoard[row2, col2] = board[row2, col2];
            DisplayBoard(displayBoard);

            // Check if cards match
            if (board[row1, col1] == board[row2, col2])
            {
                Console.WriteLine("Eşleşme bulundu!");
                remainingPairs--;
            }
            else
            {
                Console.WriteLine("Eşleşme bulunamadı.");
                displayBoard[row1, col1] = '-';
                displayBoard[row2, col2] = '-';
            }

            steps++;
        }

        Console.WriteLine("OYUN BİTTİ!");
        Console.WriteLine($"TOPLAM ADIM SAYISI = {steps}");

        // Simulate a random duration for the game
        Random rnd = new Random();
        double totalTime = rnd.Next(200, 600) / 100.0; // in seconds
        Console.WriteLine($"TOPLAM SÜRE = {totalTime:F2} dk");
    }

    static char[,] InitializeBoard(int size, char[] symbols)
    {
        List<char> symbolList = new List<char>(symbols);
        Random rnd = new Random();
        char[,] board = new char[size, size];

        for (int i = 0; i < size * size / 2; i++)
        {
            char symbol = symbolList[i];
            int count = 0;

            while (count < 2)
            {
                int row = rnd.Next(0, size);
                int col = rnd.Next(0, size);

                if (board[row, col] == '\0')
                {
                    board[row, col] = symbol;
                    count++;
                }
            }
        }

        return board;
    }

    static char[,] InitializeDisplayBoard(int size)
    {
        char[,] displayBoard = new char[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                displayBoard[i, j] = '-';
            }
        }

        return displayBoard;
    }

    static void DisplayBoard(char[,] board)
    {
        int size = (int)Math.Sqrt(board.Length);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Console.Write("| " + board[i, j] + " ");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("--------------------------");
    }

    static int[] GetPlayerSelections(int size)
    {
        int[] selections = new int[2];

        for (int i = 0; i < 2; i++)
        {
            bool validInput = false;
            int selection = 0;

            while (!validInput)
            {
                Console.Write($"Lütfen {i + 1}. Kartı seçiniz >> ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out selection) && IsValidSelection(selection, size))
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Geçersiz giriş. Lütfen bir sayı girin.");
                }
            }

            selections[i] = selection;
        }

        return selections;
    }

    static bool IsValidSelection(int selection, int size)
    {
        return selection >= 1 && selection <= size * size;
    }
}
