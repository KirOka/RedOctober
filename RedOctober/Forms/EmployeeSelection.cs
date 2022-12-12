using Microsoft.EntityFrameworkCore;
using RedOctober.DAL.Entities;
using RedOctober.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedOctober.Forms
{
    public partial class EmployeeSelection : Form
    {
        private IEmployeesReposytory _employeesReposytory;

        private List<Employee> _employees = new List<Employee>();
        
        private Employee _employee;

        public EmployeeSelection(IEmployeesReposytory employeesReposytory)
        {
            InitializeComponent();

            this._employeesReposytory = employeesReposytory;
        }

        /// <summary>
        /// Возвращает результат выбора
        /// </summary>
        /// <returns>Выбранный Сотрудник</returns>
        public Employee GetResult()
        {
            return this._employee;
        }

        private void EmployeeSelection_Load(object sender, EventArgs e)
        {
        }

        private async void buttonFind_Click(object sender, EventArgs e)
        {
             await this.FindEmployees(this.textBoxName.Text, this.textBoxPatronymic.Text, this.textBoxSurname.Text);
        }

        private async Task FindEmployees(string nameFilter, string patronymicFilter, string surnameFilter)
        {
            this._employees = await this._employeesReposytory.GetFilteredEmployeesAsync( p => EF.Functions.Like(p.Name!, "%" + nameFilter + "%") &&
                EF.Functions.Like(p.Patronymic!, "%" + patronymicFilter + "%") &&
                EF.Functions.Like(p.Surname!, "%" + surnameFilter + "%"));
            this.dataGridViewEmployees.DataSource = this._employees;
            if (this.dataGridViewEmployees.Columns.Contains("Id"))
            {
                this.dataGridViewEmployees.Columns["Id"].Visible = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewEmployees.CurrentRow == null)
            {
                return;
            }
            this._employee = (Employee)this.dataGridViewEmployees.CurrentRow.DataBoundItem;
            this.DialogResult= DialogResult.OK;
            this.Close();
        }
    }
}
