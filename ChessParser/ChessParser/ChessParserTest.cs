using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    class ChessParserTest
    {
        static void _Main_(string[] args)
        {
            ChessParser cParser = new ChessParser();
            Console.WriteLine(cParser.ToString());

            cParser.LegalMove("e2e4");
            Console.WriteLine(cParser.ToString());

            cParser.LegalMove("e7e5");
            Console.WriteLine(cParser.ToString());

            cParser.LegalMove("f1c4");
            Console.WriteLine(cParser.ToString());

            cParser.LegalMove("d7d5");
            Console.WriteLine(cParser.ToString());

            cParser.LegalMove("d1g4");
            Console.WriteLine(cParser.ToString());

            if (cParser.LegalMove("h1h3") == true)
            {
                Console.WriteLine(cParser.ToString());
            }
            else
            {
                Console.WriteLine("illegal Rook move");
            }

        }
    }
}
