using MidtermExerciseEn.DataAccess;
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
    public class CoursesManager {

        private CoursesForm coursesWindow;
        private CoursesDAO coursesDAO;
        private DataSet dataSet;

        public CoursesManager(SqlConnection connection, DataSet dataSet) {
            this.dataSet = dataSet;
            this.coursesWindow = new CoursesForm(this);
            this.coursesDAO = new CoursesDAO(connection);
        }

        public void OpenCoursesWindow() {
            this.coursesWindow.ShowDialog();
        }

        public void CloseCoursesWindow() {
            this.coursesWindow.DialogResult = DialogResult.Cancel;
        }

        public void LoadAndDisplayCoursesData() {
            this.coursesDAO.FillDataSet(this.dataSet);
            DataTable coursesTable = this.dataSet.Tables[this.coursesDAO.datasetTableName];
            this.coursesWindow.BindTableToGridView(coursesTable);
        }

        public void UpdateCoursesInDatabase() {
            this.coursesDAO.UpdateDataSet(this.dataSet);
        }

    }
}
