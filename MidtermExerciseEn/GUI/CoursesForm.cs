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
    public partial class CoursesForm : Form {

        private CoursesManager manager;

        public CoursesForm(CoursesManager cManager) {
            this.manager = cManager;
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e) {
            this.manager.CloseCoursesWindow();
        }

        private void buttonLoadData_Click(object sender, EventArgs e) {
            this.manager.LoadAndDisplayCoursesData();
        }

        private void buttonSaveChanges_Click(object sender, EventArgs e) {
            this.manager.UpdateCoursesInDatabase();
        }

        public void BindTableToGridView(DataTable coursesTable) {
            this.dataGridView1.DataSource = coursesTable;
            this.dataGridView1.Refresh();
        }
    }
}
