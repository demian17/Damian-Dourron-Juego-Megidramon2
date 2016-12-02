#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Animad2
{
    static class Program
    {
        private static Game1 game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			Game1 vidas;
            game = new Game1();
            game.Run();
			//if (vidas==0)
			//{

			//}
        }
    }
}
