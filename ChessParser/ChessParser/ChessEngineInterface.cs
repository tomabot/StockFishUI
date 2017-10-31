
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessParser
{
    class ChessEngineInterface
    {
        private static Process process;
        private static StreamWriter chessEngineWriter;
        private static string preamble = "position startpos moves ";
        private static string movetime = "go movetime 5000";
        private static string engineResponse;
        private static string engineMove;

        private static List<string> playList;

        static void Main(string[] args)
        {
            StartChessEngine();
            StartPlaying();
        }

        private static void StartChessEngine()
        {

            process = new Process();
            //process.StartInfo.FileName = "C:\\stockfish-8-win\\Windows\\stockfish_8_x64.exe";
            process.StartInfo.FileName = "C:\\stockfish-8-win\\stockfish_8_x64.exe";
            //p.StartInfo.WorkingDirectory = "C:\\stockfish_8_win\\";
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            process.StartInfo.CreateNoWindow = true;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();

            process.OutputDataReceived += new DataReceivedEventHandler(StandardOutputReceiver);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += new DataReceivedEventHandler(StandardErrorReceiver);
            process.BeginErrorReadLine();

            chessEngineWriter = process.StandardInput;

        }

        private static void StartPlaying()
        {
            string humanMove;

            preamble = "position startpos moves ";
            movetime = "go movetime 5000";
            playList = new List<string>();

            ChessParser cp = new ChessParser();

            Console.WriteLine("{0}", cp);
            Console.WriteLine("enter a move");
            humanMove = Console.ReadLine();

            for (;;)
            {
                if (humanMove == "exit")
                {
                    return;
                }

                try
                {
                    if (cp.LegalMove(humanMove))
                    {
                        // the move is valid, break 
                        // out of the loop
                        break;
                    }
                    else
                    {
                        // the move breaks a chess rule, try again
                        Console.WriteLine("invalid move, try again");
                        humanMove = Console.ReadLine();
                    }
                }
                catch ( ArgumentOutOfRangeException ex)
                {
                    // the move is off of the board,
                    // try again
                    Console.WriteLine(ex.Message);
                }
            }

            // game loop
            for (;;)
            {
                Console.WriteLine("{0}", cp);

                // add the human move to the play list and the preamble
                playList.Add(humanMove);
                preamble += humanMove + " ";

                // write the new preamble and play list to standard out (debugging)
                Console.WriteLine("{0}", preamble);

                // send the preamble to the chess engine
                chessEngineWriter.WriteLine(preamble);
                chessEngineWriter.WriteLine(movetime);

                // get the engine move and the "ponder" (hint) move (if there is one)
                engineResponse = GetChessEngineResponse();
                string engineMove = engineResponse.Substring(0, 4);
                Console.WriteLine("engine moves {0}", engineMove);

                if (!cp.LegalMove(engineMove))
                {
                    throw new Exception("...invalid move by engine...");
                }

                // add the engine move to the play list and the preamble
                playList.Add(engineMove);
                preamble += engineMove + " ";

                // write the new preamble to standard out (debugging)
                Console.WriteLine("{0}", preamble);

                // send the preamble to the chess engine
                chessEngineWriter.WriteLine(preamble);
                chessEngineWriter.WriteLine(movetime);

                Console.WriteLine("{0}", cp);

                // get human move loop
                for (;;)
                {
                    // get the human's move
                    Console.WriteLine("enter a move");
                    humanMove = Console.ReadLine();
                    if (humanMove == "list")
                    {
                        foreach (String s in playList) {
                            Console.WriteLine(s);
                        }
                        continue;
                    }

                    if (humanMove == "exit")
                    {
                        process.Close();
                        return;
                    }

                    try {
                        if (cp.LegalMove(humanMove))
                        {
                            // the move is legal, break out
                            // of the "get human move loop"
                            break;
                        }
                        else
                        {
                            // the move is on the board but
                            // breaks a chess rule, try again
                            Console.WriteLine("invalid move");
                        }
                    }
                    catch(ArgumentOutOfRangeException ex)
                    {
                        // the human move was off the board,
                        // try again
                        Console.WriteLine(ex.Message);
                    }

                }  // human move loop
            }  // game loop
        }

        private static string GetChessEngineResponse()
        {
            engineResponse = "";

            do
            {
                Thread.Sleep(250);
            } while (!engineResponse.Contains("bestmove "));
            Thread.Sleep(500);

            int moveIndex = engineResponse.IndexOf("bestmove ") + "bestmove ".Length;
            engineMove = engineResponse.Substring(moveIndex);
            // debugging
            Console.WriteLine(engineMove);
            return engineMove;
        }

        private static void StandardErrorReceiver(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("{0}", e.Data);
        }

        private static void StandardOutputReceiver(object sender, DataReceivedEventArgs e)
        {
            engineResponse += e.Data;
            //Console.WriteLine("{0}", e.Data);
        }
    }
}
