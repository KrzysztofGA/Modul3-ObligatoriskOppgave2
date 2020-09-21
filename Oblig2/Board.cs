using System;
using System.Collections.Generic;
using System.Threading;

namespace Oblig2
{
    class Board
    {
        readonly Dictionary<string, Piece> _pieces = new Dictionary<string, Piece>();

        public void Set(string position, Piece piece)
        {
            if (_pieces.ContainsKey(position)) _pieces[position] = piece;
            else _pieces.Add(position, piece);
        }

        public bool Move(string fromPosition, string toPosition)
        {
            if (HasValue(toPosition) || !HasValue(fromPosition))
            {
                ImpossibleMovementInfo();
                return false;
            }
            var piece = _pieces[fromPosition];
            var isPossible = piece.Move(fromPosition, toPosition);
            if (!isPossible)
            {
                ImpossibleMovementInfo();
                return false;
            }
            Set(toPosition, piece);
            Set(fromPosition, null);
            return true;
        }

        private void ImpossibleMovementInfo()
        {
            Console.ForegroundColor = Colors.Alert;
            Console.WriteLine(("\nBevegelse ikke tillatt").ToUpper());
            Thread.Sleep(3000);
        }

        private bool HasValue(string position)
        {
            return _pieces.ContainsKey(position) && _pieces[position] != null;
        }

        public void Show()
        {
            Console.Clear();
            //Console.WriteLine("\n8\n\n\n7\n\n\n6\n\n\n5\n\n\n4\n\n\n3\n\n\n2\n\n\n1");
            Console.WriteLine(
                "\n   \n 8 \n   \n   \n 7 \n   \n   \n 6 \n   \n   \n 5 \n   \n   \n 4 \n   \n   \n 3 \n   \n   \n 2 \n   \n   \n 1 ");
            for (var row = 8; row >= 1; row--)
            for (var col = 'a'; col <= 'h'; col++)
            {
                var left = 3 + (col - 'a') * 7;
                var top = 1 + (8 - row) * 3;
                var fillChar = row % 2 == col % 2 ? ' ' : '█';
                Write(7, fillChar, left, top);
                Write(1, fillChar, left, top + 1);
                Console.Write(GetPieceSymbol(col, row));
                Write(1, fillChar);
                Write(7, fillChar, left, top + 2);
            }

            Console.WriteLine("\n\n      A      B      C      D      E      F      G      H\n\n");
        }

        private string GetPieceSymbol(char col, int row)
        {
            var position = "" + col + row;
            if (!_pieces.ContainsKey(position) || _pieces[position] == null) return "     ";
            return _pieces[position].Symbol;
        }

        private static void Write(int count, char c, int? left = null, int? top = null)
        {
            if (left != null) Console.CursorLeft = left.Value;
            if (top != null) Console.CursorTop = top.Value;

            Console.Write("".PadLeft(count, c));
        }
    }
}
