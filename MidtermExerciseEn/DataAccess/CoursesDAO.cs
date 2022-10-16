using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermExerciseEn.DataAccess {
    public class CoursesDAO {

        private string databaseTableName = "dbo.Courses";
        public string datasetTableName = "Courses";
        private SqlDataAdapter adapter;
        private SqlConnection connection;

        public CoursesDAO(SqlConnection conn) { 
            this.connection = conn;
            this.adapter = this.CreateDataAdapter();
        }

        public void FillDataSet(DataSet dataSet) {
            if (dataSet.Tables.Contains(this.datasetTableName)) {
                dataSet.Tables.Remove(this.datasetTableName);
            }
            this.connection.Open();
            this.adapter.Fill(dataSet, this.datasetTableName);
            this.connection.Close();

            DataTable coursesDataTable = dataSet.Tables[this.datasetTableName];
            coursesDataTable.Columns["id"].ReadOnly = true;
            coursesDataTable.Columns["id"].AutoIncrementSeed = 0;
            coursesDataTable.Columns["id"].AutoIncrementStep = -1;
            coursesDataTable.Columns["courseCode"].AllowDBNull = false;
            coursesDataTable.Columns["courseCode"].MaxLength = 50;
            coursesDataTable.Columns["name"].AllowDBNull = false;
            coursesDataTable.Columns["description"].AllowDBNull = true;
            coursesDataTable.Columns["dateCreated"].AllowDBNull = true;
            coursesDataTable.Columns["dateCreated"].ReadOnly = true;
            coursesDataTable.Columns["dateUpdated"].AllowDBNull = true;
            coursesDataTable.Columns["dateUpdated"].ReadOnly = true;
            coursesDataTable.Columns["dateDeleted"].AllowDBNull = true;
            coursesDataTable.Columns["dateDeleted"].ReadOnly = true;

        }

        public void UpdateDataSet(DataSet dataSet) {
            this.connection.Open();
            this.adapter.Update(dataSet, this.datasetTableName);
            this.connection.Close();
            dataSet.Tables[this.datasetTableName].AcceptChanges();
        }

        private SqlDataAdapter CreateDataAdapter() {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            SqlCommand selectCommand = new SqlCommand($"SELECT * FROM {this.databaseTableName};", this.connection);

            SqlCommand insertCommand = new SqlCommand($"INSERT INTO {this.databaseTableName} " +
                $"(courseCode, name, description, dateCreated, dateUpdated) " +
                $"VALUES (@courseCode, @name, @description, @dateCreated, @dateUpdated);" +
                $"SELECT * FROM {this.databaseTableName} WHERE id = SCOPE_IDENTITY();", this.connection);
            insertCommand.Parameters.Add("@courseCode", SqlDbType.NVarChar, 50, "courseCode");
            insertCommand.Parameters.Add("@name", SqlDbType.Text, -1, "name");
            insertCommand.Parameters.Add("@description", SqlDbType.Text, -1, "description");
            insertCommand.Parameters.Add("@dateCreated", SqlDbType.DateTime);
            insertCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);
            insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;


            SqlCommand updateCommand = new SqlCommand($"UPDATE {this.databaseTableName} SET " +
                $"courseCode = @courseCode, " +
                $"name = @name, " +
                $"description = @description, " +
                $"dateUpdated = @dateUpdated " +
                $"WHERE id = @whereId AND dateUpdated = @oldDateUpdated;", this.connection);
            updateCommand.Parameters.Add("@courseCode", SqlDbType.NVarChar, 50, "courseCode");
            updateCommand.Parameters.Add("@name", SqlDbType.Text, -1, "name");
            updateCommand.Parameters.Add("@description", SqlDbType.Text, -1, "description");
            updateCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);
            updateCommand.Parameters.Add("@whereId", SqlDbType.Int, 4, "id").SourceVersion = DataRowVersion.Original;
            updateCommand.Parameters.Add("@oldDateUpdated", SqlDbType.DateTime, 6, "dateUpdated").SourceVersion = DataRowVersion.Original;

            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {this.databaseTableName} WHERE id = @id;", this.connection);
            deleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");

            adapter.SelectCommand = selectCommand;
            adapter.InsertCommand = insertCommand;
            adapter.UpdateCommand = updateCommand;
            adapter.DeleteCommand = deleteCommand;


            adapter.RowUpdating += new SqlRowUpdatingEventHandler(OnRowUpdating);
            adapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);


            return adapter;
        }

        private void OnRowUpdating(object sender, SqlRowUpdatingEventArgs eventArguments) {
            if (eventArguments.StatementType == StatementType.Insert) {
                DateTime currentDateAndTime = DateTime.Now;
                eventArguments.Command.Parameters["@dateCreated"].Value = currentDateAndTime;
                eventArguments.Command.Parameters["@dateUpdated"].Value = currentDateAndTime;

            } else if (eventArguments.StatementType == StatementType.Update) {
                DateTime currentDateAndTime = DateTime.Now;
                eventArguments.Command.Parameters["@dateUpdated"].Value = currentDateAndTime;

            }
        }

        private void OnRowUpdated(object sender, SqlRowUpdatedEventArgs eventArguments) {
            if (eventArguments.StatementType == StatementType.Insert) {
                eventArguments.Status = UpdateStatus.SkipCurrentRow;

            } else if (eventArguments.StatementType == StatementType.Update) {
                if (eventArguments.RecordsAffected == 0) {
                    throw new Exception("No rows affected during update.");
                }
            } else if (eventArguments.StatementType == StatementType.Delete) {
                if (eventArguments.RecordsAffected == 0) {
                    throw new Exception("No rows affected during delete.");
                }
            }
        }

    }
}
