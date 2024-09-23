using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Blaster
{
    static int blasterPositionX = 0; // Initial X position of the blaster
    static int blasterPositionY = 1; // Initial Y position of the blaster
    static int windowWidth = 40; // Define the width of the console "window"
    static int windowHeight = 20; // Define the height of the console "window"
    static string blasterIcon = "🚀"; // The blaster icon
    static int direction = 1; // Moving right initially

    public void Play()
    {
        Console.CursorVisible = false; // Hide the blinking cursor
        Console.Clear();
        while (true)
        {
            // Move the blaster in the current direction
            MoveBlaster();

            // Delay for smoother movement
            Thread.Sleep(100);

            // Check if the blaster has hit the boundary
            if (blasterPositionX >= windowWidth - 1 || blasterPositionX <= 0)
            {
                // Reverse direction
                direction *= -1;
                MakeSound();
                // Drop the blaster down by one row
                blasterPositionY++;

                // End the game if the blaster reaches the bottom
                if (blasterPositionY >= windowHeight)
                {
                    GameOver();
                    break;
                }
            }
        }
    }

    void MoveBlaster()
    {
        // Erase the current blaster by overwriting with space
        Console.SetCursorPosition(blasterPositionX, blasterPositionY);
        Console.Write(" ");

        // Update the blaster's X position based on direction
        blasterPositionX += direction;

        // Redraw the blaster at its new position
        DrawBlaster();
    }

    // Method to draw the blaster in its current position
    void DrawBlaster()
    {
        Console.SetCursorPosition(blasterPositionX, blasterPositionY);
        Console.Write(blasterIcon);
    }

    // Method to handle the game over scenario
    void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(windowWidth / 2 - 5, windowHeight / 2);
        Console.WriteLine("Game Over");
    }

    private void MakeSound(){
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Console.Beep();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // Play beep sound
            Process.Start("osascript", "-e 'beep'");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Console.WriteLine("Running on Linux");
        }
    }
}