using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidtermExerciseEn.DataAccess {
    public class StudentsDAO {

        private string databaseTableName = "dbo.Students";
        public string datasetTableName = "Students";
        private SqlDataAdapter adapter;
        private SqlConnection connection;

        public StudentsDAO(SqlConnection conn) {
            this.connection = conn;
            this.adapter = this.CreateAdapter();
        }

        public void FillDataSet(DataSet dataSet) {
            if (dataSet.Tables.Contains(this.datasetTableName)) {
                dataSet.Tables.Remove(this.datasetTableName);
            }
            this.connection.Open();
            this.adapter.Fill(dataSet, this.datasetTableName);
            this.connection.Close();

            DataTable studentsTable = dataSet.Tables[this.datasetTableName];
            studentsTable.Columns["studentCode"].AutoIncrementSeed = 0;
            studentsTable.Columns["studentCode"].AutoIncrementStep = -1;
            studentsTable.Columns["studentCode"].ReadOnly = true;
            studentsTable.Columns["studentCode"].AllowDBNull = true;

            studentsTable.Columns["firstName"].AllowDBNull = false;
            studentsTable.Columns["firstName"].MaxLength = 50;
            studentsTable.Columns["lastName"].AllowDBNull = false;
            studentsTable.Columns["lastName"].MaxLength = 50;

            studentsTable.Columns["dateCreated"].AllowDBNull = true;
            studentsTable.Columns["dateUpdated"].AllowDBNull = true;
            studentsTable.Columns["dateDeleted"].AllowDBNull = true;
            studentsTable.Columns["dateCreated"].ReadOnly = true;
            studentsTable.Columns["dateUpdated"].ReadOnly = true;
            studentsTable.Columns["dateDeleted"].ReadOnly = true;

        }

        public void UpdateDataSet(DataSet dataSet) {
            this.connection.Open();
            this.adapter.Update(dataSet, this.datasetTableName);
            this.connection.Close();
            DataTable studentsTable = dataSet.Tables[this.datasetTableName];
            studentsTable.AcceptChanges();

        }

        private SqlDataAdapter CreateAdapter() {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            SqlCommand selectCommand = new SqlCommand($"SELECT * FROM {this.databaseTableName};", this.connection);

            SqlCommand insertCommand = new SqlCommand($"INSERT INTO {this.databaseTableName} " +
                $"(firstName, lastName, dateCreated, dateUpdated) " +
                $"VALUES (@firstName, @lastName, @dateCreated, @dateUpdated);" +
                $"SELECT * FROM {this.databaseTableName} WHERE studentCode = SCOPE_IDENTITY();", this.connection);
            insertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
            insertCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 50, "firstName");
            insertCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50, "lastName");
            insertCommand.Parameters.Add("@dateCreated", SqlDbType.DateTime);
            insertCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);

            SqlCommand updateCommand = new SqlCommand($"UPDATE {this.databaseTableName} SET " +
                $"firstName = @firstName, " +
                $"lastName = @lastName, " +
                $"dateUpdated = @dateUpdated " +
                $"WHERE studentCode = @studentCode AND " +
                $"dateUpdated = @oldDateUpdated;", this.connection);
            updateCommand.Parameters.Add("@firstName", SqlDbType.NVarChar, 50, "firstName");
            updateCommand.Parameters.Add("@lastName", SqlDbType.NVarChar, 50, "lastName");
            updateCommand.Parameters.Add("@dateUpdated", SqlDbType.DateTime);
            updateCommand.Parameters.Add("@studentCode", SqlDbType.Int, 4, "studentCode");
            updateCommand.Parameters.Add("@oldDateUpdated", SqlDbType.DateTime, 6, "dateUpdated").SourceVersion = DataRowVersion.Original; ;

            SqlCommand deleteCommand = new SqlCommand($"DELETE FROM {this.databaseTableName} " +
                $"WHERE studentCode = @studentCode;", this.connection);
            deleteCommand.Parameters.Add("@studentCode", SqlDbType.Int, 4, "studentCode");

            adapter.SelectCommand = selectCommand;
            adapter.UpdateCommand = updateCommand;
            adapter.InsertCommand = insertCommand;
            adapter.DeleteCommand = deleteCommand;


            adapter.RowUpdating += new SqlRowUpdatingEventHandler(OnRowUpdating);
            adapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);

            return adapter;

        }

        private void OnRowUpdating(object sender, SqlRowUpdatingEventArgs arguments) {
            if (arguments.StatementType == StatementType.Insert) {
                DateTime now = DateTime.Now;
                arguments.Command.Parameters["@dateCreated"].Value = now;
                arguments.Command.Parameters["@dateUpdated"].Value = now;

            } else if (arguments.StatementType == StatementType.Update) {
                arguments.Command.Parameters["@dateUpdated"].Value = DateTime.Now;

            }
        }

        private void OnRowUpdated(object sender, SqlRowUpdatedEventArgs arguments) {

            if (arguments.StatementType == StatementType.Insert) {
                arguments.Status = UpdateStatus.SkipCurrentRow;

            } else if (arguments.StatementType == StatementType.Update) {
                if (arguments.RecordsAffected == 0) {
                    throw new Exception("No rows affected during update.");
                }
            } else if (arguments.StatementType == StatementType.Delete) {
                if (arguments.RecordsAffected == 0) {
                    throw new Exception("No rows affected during delete.");
                }
            }
        }
    }
}
