using RedOctober.DAL.Entities;

namespace RedOctober.Forms
{
    public partial class EmployeeEdit : Form
    {
        private Employee _employee;
        public EmployeeEdit(Employee employee)
        {
            InitializeComponent();

            this._employee= employee;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this._employee.Name = this.textBoxName.Text;
            this._employee.Surname = this.textBoxSurname.Text;
            this._employee.Patronymic = this.textBoxPatronymic.Text;
            this.DialogResult= DialogResult.OK;
            this.Close();
        }

        private void EmployeeEdit_Load(object sender, EventArgs e)
        {
            this.textBoxName.Text = this._employee.Name;
            this.textBoxSurname.Text = this._employee.Surname;
            this.textBoxPatronymic.Text = this._employee.Patronymic;
        }

        public Employee GetResult()
        {
            return this._employee;
        }
    }
}
