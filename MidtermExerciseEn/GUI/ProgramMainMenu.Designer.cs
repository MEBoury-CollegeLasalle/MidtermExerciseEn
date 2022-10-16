namespace MidtermExerciseEn.GUI {
    partial class ProgramMainMenu {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonManageCourses = new System.Windows.Forms.Button();
            this.buttonManageStudents = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(307, 329);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(196, 38);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Exit Program";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonManageCourses
            // 
            this.buttonManageCourses.Location = new System.Drawing.Point(307, 108);
            this.buttonManageCourses.Name = "buttonManageCourses";
            this.buttonManageCourses.Size = new System.Drawing.Size(196, 38);
            this.buttonManageCourses.TabIndex = 1;
            this.buttonManageCourses.Text = "Manage Courses";
            this.buttonManageCourses.UseVisualStyleBackColor = true;
            this.buttonManageCourses.Click += new System.EventHandler(this.buttonManageCourses_Click);
            // 
            // buttonManageStudents
            // 
            this.buttonManageStudents.Location = new System.Drawing.Point(307, 152);
            this.buttonManageStudents.Name = "buttonManageStudents";
            this.buttonManageStudents.Size = new System.Drawing.Size(196, 38);
            this.buttonManageStudents.TabIndex = 2;
            this.buttonManageStudents.Text = "Manage Students";
            this.buttonManageStudents.UseVisualStyleBackColor = true;
            this.buttonManageStudents.Click += new System.EventHandler(this.buttonManageStudents_Click);
            // 
            // ProgramMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonManageStudents);
            this.Controls.Add(this.buttonManageCourses);
            this.Controls.Add(this.buttonClose);
            this.Name = "ProgramMainMenu";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonManageCourses;
        private System.Windows.Forms.Button buttonManageStudents;
    }
}

