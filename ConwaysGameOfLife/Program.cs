using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife {
	class Program {
		private static Int32 Width = 317;
		private static Int32 Height = 112;
		private static Boolean[][] Data = new Boolean[Width][];

		static void Main(string[] args) {
			Random rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
			Console.SetWindowSize(Width, Height);
			for (int x = 0; x < Width; x++) {
				Data[x] = new Boolean[Height];
				for (int y = 0; y < Height; y++) {
					if (rnd.Next(2) == 0) {
						Data[x][y] = false;
					} else {
						Data[x][y] = true;
					}
				}
			}
			PaintScreen();
			//Console.ReadLine();
			while (true) {
				Console.SetCursorPosition(0, 0);
				Data = Iterate();
				PaintScreen();
				//Console.ReadLine();
			}
		}

		private static void PaintScreen() {
			String Output = String.Empty;
			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < (Width - 1); x++) {
					if (Data[x][y] == true) {
						Output += 'X';
					} else {
						Output += ' ';
					}
				}
				Output += "\n";
            }
			// This is where 98.5% of the code execution happens! Console output is slow.
			Console.Write(Output);
		}

		private static Boolean[][] Iterate() {
            Boolean[][] NewData = new Boolean[Width][];
			for (int x = 0; x < Width; x++) {
				NewData[x] = new Boolean[Height];
			}
			for (int x = 0; x < Width; x++) {
				for (int y = 0; y < Height; y++) {
					int Count = CountNeighbors(x, y);
					if (Data[x][y] == true) {
						if (Count < 2) { NewData[x][y] = false; }
						if ((Count == 2) || (Count == 3)) { NewData[x][y] = true; }
						if (Count > 3) { NewData[x][y] = false; }
					} else {
						if (Count == 3) { NewData[x][y] = true; }
					}
				}
			}
			return NewData;
		}

		private static int CountNeighbors(int x, int y) {
			int returnVal = 0;
			if (x > 0) {
				if (y > 0) {
					if (Data[x - 1][y - 1] == true) { returnVal++; }
				}
				if (Data[x - 1][y] == true) { returnVal++; }
				if (y < (Height - 1)) {
					if (Data[x - 1][y + 1] == true) { returnVal++; }
				}
			}
			if (x < (Width - 1)) {
				if (y > 0) {
					if (Data[x + 1][y - 1] == true) { returnVal++; }
				}
				if (Data[x + 1][y] == true) { returnVal++; }
				if (y < (Height - 1)) {
					if (Data[x + 1][y + 1] == true) { returnVal++; }
				}
			}
			if (y > 0) {
				if (Data[x][y - 1] == true) { returnVal++; }
			}
			if (y < (Height - 1)) {
				if (Data[x][y + 1] == true) { returnVal++; }
			}
			return returnVal;
		}

	}
}
