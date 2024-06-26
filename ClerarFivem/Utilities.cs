﻿using System.Diagnostics;

namespace ClerarFivem
{
    internal class Utilities
    {
        static public void PrintAsciiArt()
        {
            Console.WriteLine("    ____                          _          ______                                       ");
            Console.WriteLine("   /  _/___ ___  ____  ___  _____(_)___     / ____/___  ____ ___  ____  ____ _____  __  __");
            Console.WriteLine("   / // __ `__ \\/ __ \\/ _ \\/ ___/ / __ \\   / /   / __ \\/ __ `__ \\/ __ \\/ __ `/ __ \\/ / / /");
            Console.WriteLine(" _/ // / / / / / /_/ /  __/ /  / / /_/ /  / /___/ /_/ / / / / / / /_/ / /_/ / / / / /_/ / ");
            Console.WriteLine("/___/_/_/_/ /_/ .___/\\___/_/  /_/\\____/   \\____/\\____/_/ /_/ /_/ .___/\\__,_/_/ /_/\\__, /  ");
            Console.WriteLine("             /_/                                              /_/                /____/   ");
        }

        static public bool IsProcessRunning(string processName)
        {
            return Process.GetProcessesByName(processName).Any();
        }

        static public void DeleteDirectory(string path)
        {
            var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
            int totalFiles = files.Length;
            int deletedFiles = 0;

            if (totalFiles == 0)
            {
                Directory.Delete(path, true);
                return;
            }

            foreach (var file in files)
            {
                File.Delete(file);
                deletedFiles++;
                DrawProgressBar(deletedFiles, totalFiles);
            }

            Directory.Delete(path, true);
        }

        public static void ClearDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Directory not found: {path}");
                return;
            }

            var files = Directory.GetFiles(path);
            int totalFiles = files.Length;
            int deletedFiles = 0;

            foreach (var file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
                deletedFiles++;
                DrawProgressBar(deletedFiles, totalFiles);
            }

            var directories = Directory.GetDirectories(path);
            foreach (var dir in directories)
            {
                try
                {
                    DeleteDirectory(dir); // Recursively delete subdirectories
                    Directory.Delete(dir, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting directory {dir}: {ex.Message}");
                }
                deletedFiles++;
                DrawProgressBar(deletedFiles, totalFiles + directories.Length);
            }
        }

        static public void DeleteFile(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
        }

        static public void DrawProgressBar(int progress, int total)
        {
            if (total == 0) total = 1;

            Console.CursorLeft = 0;
            Console.Write("[");
            int totalBars = 30;
            int bars = progress * totalBars / total;

            for (int i = 0; i < totalBars; i++)
            {
                if (i < bars)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(" ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write($"] {progress}/{total} Files");
        }

    }
}
