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
    public partial class StudentsForm : Form {

        private StudentsManager studentsManager;

        public StudentsForm(StudentsManager manager) {
            this.studentsManager = manager;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.studentsManager.CloseStudentsWindow();
        }

        private void buttonLoadData_Click(object sender, EventArgs e) {
            this.studentsManager.LoadAndDisplayData();
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            this.studentsManager.UpdateDataInDatabase();
        }

        public void BindDataTAbleToGridView(DataTable table) {
            this.dataGridView1.DataSource = table;
            this.dataGridView1.Refresh();
        }
    }
}
