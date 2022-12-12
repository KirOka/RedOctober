using RedOctober.DAL.Entities;
using RedOctober.DAL.Repositories.Abstractions;
using System.Windows.Forms;

namespace RedOctober.Forms
{
    public partial class EmployeeDictionary : Form
    {
        private IEmployeesReposytory _employeesReposytory;

        private List<Employee> _employees = new List<Employee>();

        public EmployeeDictionary(IEmployeesReposytory employeesReposytory)
        {
            InitializeComponent();

            this._employeesReposytory = employeesReposytory;
        }

        private async void EmployeeDictionary_Load(object sender, EventArgs e)
        {
            await this.ReloadEmployeeDictionary();
        }

        private async Task ReloadEmployeeDictionary(Employee? employee = null)
        {
            this._employees = await this._employeesReposytory.GetAllAsync();
            this.dataGridViewEmployees.DataSource = this._employees;
            FormatingEmployeeDataGridView.DataGridColumnsStyle(this.dataGridViewEmployees);
        }

        private async void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            EmployeeEdit employeeEdit = new EmployeeEdit(employee);
            employeeEdit.ShowDialog();
            if (employeeEdit.DialogResult == DialogResult.OK)
            {
                employee = employeeEdit.GetResult();
                await this._employeesReposytory.AddAsync(employee);
                await this.ReloadEmployeeDictionary();
            }
        }

        private async void toolStripButtonEdit_ClickAsync(object sender, EventArgs e)
        {
            if (this.dataGridViewEmployees.CurrentRow == null)
            {
                return;
            }
            Employee employee = (Employee)this.dataGridViewEmployees.CurrentRow.DataBoundItem;
            EmployeeEdit employeeEdit = new EmployeeEdit(employee);
            employeeEdit.ShowDialog();
            if (employeeEdit.DialogResult == DialogResult.OK)
            {
                employee = employeeEdit.GetResult();
                this._employeesReposytory.Update(employee);
                await this.ReloadEmployeeDictionary();
            }
        }

        private async void toolStripButtonDelete_ClickAsync(object sender, EventArgs e)
        {
            if (this.dataGridViewEmployees.CurrentRow == null)
            {
                return;
            }
            Employee employee = (Employee)this.dataGridViewEmployees.SelectedRows[0].DataBoundItem;
            this._employeesReposytory.Delete(employee);
            await this.ReloadEmployeeDictionary();
        }
    }
}
