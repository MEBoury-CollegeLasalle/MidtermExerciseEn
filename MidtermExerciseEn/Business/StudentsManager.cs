using MidtermExerciseEn.DataAccess;
using MidtermExerciseEn.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermExerciseEn.Business {
    public class StudentsManager {

        private StudentsForm studentsWindow;
        private StudentsDAO studentsDAO;
        private DataSet dataSet;

        public StudentsManager(SqlConnection connection, DataSet dataSet) {
            this.dataSet = dataSet;
            this.studentsDAO = new StudentsDAO(connection);
            this.studentsWindow = new StudentsForm(this);
        }

        public void OpenStudentsWindow() {
            this.studentsWindow.ShowDialog();
        }

        public void CloseStudentsWindow() {
            this.studentsWindow.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public void LoadAndDisplayData() {
            this.studentsDAO.FillDataSet(this.dataSet);
            DataTable studentsTable = dataSet.Tables[this.studentsDAO.datasetTableName];
            this.studentsWindow.BindDataTAbleToGridView(studentsTable);
        }

        public void UpdateDataInDatabase() {
            this.studentsDAO.UpdateDataSet(this.dataSet);
        }
    }
}
