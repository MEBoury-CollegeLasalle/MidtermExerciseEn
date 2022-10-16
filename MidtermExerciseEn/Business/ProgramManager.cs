using MidtermExerciseEn.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MidtermExerciseEn.Business {
    public class ProgramManager {

        private ProgramMainMenu menuWindow;
        private DataSet dataSet;
        private SqlConnection connection;
        private CoursesManager courseManager;
        private StudentsManager studentsManager;

        public ProgramManager() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            this.menuWindow = new ProgramMainMenu(this);
            this.dataSet = new DataSet();
            this.connection = new SqlConnection("Server=.\\SQL2019EXPRESS;Integrated security=true;Database=db_demo1;");
            this.courseManager = new CoursesManager(this.connection, this.dataSet);
            this.studentsManager = new StudentsManager(this.connection, this.dataSet);
        }

        public void OpenMainMenu() {
            Application.Run(this.menuWindow);
        }

        public void ExitProgram() {
            Application.Exit();
        }

        public void OpenCoursesWindow() {
            this.courseManager.OpenCoursesWindow();
        }

        public void OpenStudentsWindow() {
            this.studentsManager.OpenStudentsWindow();
        }
    }
}
