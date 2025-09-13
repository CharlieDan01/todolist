using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace todolist
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTasks();
        }
        private void LoadTasks()
        {
            string query = "SELECT id, title, details,priority, isCompleted, maxDate FROM Tasks ORDER BY id";
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();
                gridListTD.DataSource = cmd.ExecuteReader();
                gridListTD.DataBind(); //linea 27
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            validateInputs();
            string query = "UPDATE Tasks SET Title=@Title, Details=@Details, Priority=@Priority, IsCompleted=@IsCompleted,maxDate=@maxDate WHERE Id=@Id";

            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Details", txtDet.Text);
                cmd.Parameters.AddWithValue("@Priority", ddlPrio.SelectedValue);
                cmd.Parameters.AddWithValue("@IsCompleted", checkDo.Checked ? 1 : 0);
                cmd.Parameters.AddWithValue("@maxDate", Calendar1.SelectedDate);
                cmd.Parameters.AddWithValue("@Id", txtID.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadTasks(); // Recargar tabla
            clear();
            //come back enable button
            btnAdd.Enabled = true;
            checkDo.Enabled = false; // disable checkbox to not mark as done when creating
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!validateInputs()) return; // si no pasa, no sigue

            bool done = false; // siempre nueva tarea sin completar
            string query = "INSERT INTO Tasks (Title, details,priority,isCompleted,maxDate) VALUES (@title,@details,@priority,@isCompleted,@maxDate)";
            using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@details", txtDet.Text);
                cmd.Parameters.AddWithValue("@priority", ddlPrio.SelectedValue);
                cmd.Parameters.AddWithValue("@isCompleted", done);
                cmd.Parameters.AddWithValue("@maxDate", Calendar1.SelectedDate);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            txtTitle.Text = "";
            txtDet.Text = "";
            ddlPrio.SelectedIndex = 0;
            checkDo.Checked = false;
            Calendar1.SelectedDate = DateTime.MinValue;
            LoadTasks();
            checkDo.Enabled = false; // disable checkbox to not mark as done when creating

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            //come back enable 
            btnAdd.Enabled = true;
            clear();


        }
        private bool validateInputs()
        {
            if (ddlPrio.SelectedIndex == 0)
            {
                ddlPrio.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                txtTitle.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDet.Text))
            {
                txtDet.Focus();
                return false;
            }
            if (checkDo.Checked) // no permitir tareas creadas como completadas
            {
                checkDo.Focus();
                return false;
            }

            // Validar fecha en Calendar
            if (Calendar1.SelectedDate == DateTime.MinValue)
            {
                // no se seleccionó ninguna fecha
                Calendar1.Focus();
                return false;
            }
            if (Calendar1.SelectedDate < DateTime.Today)
            {
                // fecha anterior a hoy
                Calendar1.Focus();
                return false;
            }
            return true;
        }

        protected void gridListTD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectRow")
            {
                int index = Convert.ToInt32(e.CommandArgument); // índice de fila
                GridViewRow row = gridListTD.Rows[index];

                // Llenar TextBox con los valores de la fila seleccionada
                txtID.Text = row.Cells[0].Text;        // ID
                txtTitle.Text = row.Cells[1].Text;     // Title
                txtDet.Text = row.Cells[2].Text;   // Details
                ddlPrio.SelectedValue = row.Cells[3].Text; // Priority
                checkDo.Checked = (row.Cells[4].Text == "True");

                //enable false button to not duplicate values
                btnAdd.Enabled = false;
                //enable true checkbox to edit
                checkDo.Enabled = true;
            }
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument); // ahora es el Id de la fila, no el índice
                deleteTask(id);
            }
        }

        

        private void deleteTask(int id)
        {
            try
            {
                string query = "DELETE FROM Tasks WHERE Id=@Id";
                using (MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadTasks(); // Recargar tabla
            }
            catch (Exception ex)
            {
                lblError.Text= "Error en deleteTask" + ex.Message;
            }
        }
        private void clear()
        {
            txtID.Text = "";
            txtTitle.Text = "";
            txtDet.Text = "";
            ddlPrio.SelectedIndex = 0;
            checkDo.Checked = false;


        }
    }
}