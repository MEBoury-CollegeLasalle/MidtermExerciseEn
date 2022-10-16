using MidtermExerciseEn.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermExerciseEn.GUI {
    public partial class ProgramMainMenu : Form {
        private ProgramManager pManager;

        public ProgramMainMenu(ProgramManager manager) {
            this.pManager = manager;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.pManager.ExitProgram();
        }

        private void buttonManageCourses_Click(object sender, EventArgs e) {
            this.pManager.OpenCoursesWindow();
        }

        private void buttonManageStudents_Click(object sender, EventArgs e) {
            this.pManager.OpenStudentsWindow();
        }
    }
}
