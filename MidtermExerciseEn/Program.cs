using MidtermExerciseEn.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermExerciseEn {
    internal static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            ProgramManager pManager = new ProgramManager();
            pManager.OpenMainMenu();
        }
    }
}
