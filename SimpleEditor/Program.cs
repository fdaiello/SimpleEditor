using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleEditor
{
    class Editor
    {
        private readonly StringBuilder text = new StringBuilder();
        public readonly StringBuilder output = new StringBuilder();
        private readonly Stack<string> UndoCommands = new Stack<string>();

        public void Append( string w, bool saveUndo = true )
        {
            if (saveUndo)
                // Save undo command - delete last k elements
                UndoCommands.Push($"2 {w.Length}");

            // Append
            text.Append(w);
        }
        public void Delete( int k, bool saveUndo = true )
        {
            if (saveUndo)
                // Save undo command - Append last k elements
                UndoCommands.Push($"1 {text.ToString(text.Length - k, k)}");

            // Delete last k elements
            text.Remove(text.Length - k, k);
        }
        public void Print ( int k)
        {
            output.AppendLine (text[k-1].ToString());
        }
        public void Undo()
        {
            // POP last undo command
            string undoCommand = UndoCommands.Pop();

            // Slipt commands
            string[] commands = undoCommand.Split(' ');

            // Check what to do
            switch (commands[0])
            {
                case "1":
                    Append(commands[1], false);
                    break;
                case "2":
                    Delete(Int32.Parse(commands[1]),false);
                    break;
            }
        }
    }
    class Program
    {
        static void Main0(string[] args)
        {
            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\input07.txt");

            // Declare our Editor Class
            Editor editor = new Editor();

            // First line contains number of operations
            string line = sr.ReadLine();

            // Store command line
            string command;

            // Comand parts
            string[] commands;

            // Try Parse
            if (Int32.TryParse(line, out int q))
            {
                // Loop the numbers of input lines - commands
                for (int i = 0; i < q; i++)
                {
                    // Read command
                    command = sr.ReadLine();

                    // Split to get commands
                    commands = command.Split(' ');

                    // Check what was the command
                    switch (commands[0])
                    {
                        case "1":
                            editor.Append(commands[1]);
                            break;

                        case "2":
                            editor.Delete(Int32.Parse(commands[1]));
                            break;

                        case "3":
                            editor.Print(Int32.Parse(commands[1]));
                            break;

                        case "4":
                            editor.Undo();
                            break;
                    }
                }
            }

            // Output to Console
            Console.WriteLine(editor.output.ToString());
        }

        static void Main(string[] args)
        {

            // Declare our Editor Class
            Editor editor = new Editor();

            // First line contains number of operations
            string line = Console.ReadLine();

            // Store command line
            string command;

            // Comand parts
            string[] commands;

            // Try Parse
            if ( Int32.TryParse(line, out int q))
            {
                // Loop the numbers of input lines - commands
                for ( int i =0; i < q; i++)
                {
                    // Read command
                    command = Console.ReadLine();

                    // Split to get commands
                    commands = command.Split(' ');

                    // Check what was the command
                    switch (commands[0])
                    {
                        case "1":
                            editor.Append(commands[1]);
                            break;

                        case "2":
                            editor.Delete(Int32.Parse(commands[1]));
                            break;

                        case "3":
                            editor.Print(Int32.Parse(commands[1]));
                            break;

                        case "4":
                            editor.Undo();
                            break;
                    }
                }
            }

            // Output to Console
            Console.WriteLine(editor.output.ToString());

        }
    }
}
