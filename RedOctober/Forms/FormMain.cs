using RedOctober.DAL.Abstraction;
using RedOctober.DAL.Entities;
using RedOctober.DAL.Repositories.Abstractions;
using RedOctober.ModelView.Models;

namespace RedOctober.Forms
{
    public partial class FormMain : Form
    {
        private IDepartmentsReposytory _departmentsReposytory;
        private IEmployeesReposytory _employeesReposytory;
        private IPositionsReposytory _positionsReposytory;
        public FormMain(IDepartmentsReposytory departmentsReposytory, IEmployeesReposytory employeesReposytory, IPositionsReposytory positionsReposytory)
        {
            InitializeComponent();

            this._departmentsReposytory = departmentsReposytory;
            this._employeesReposytory = employeesReposytory;
            this._positionsReposytory = positionsReposytory;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            this.DisableEmployeeInfoBox();
            this.treeView1.BeginUpdate();
            await this.LoadNodeChildren(this.treeView1.Nodes, null);
            this.treeView1.EndUpdate();
        }

        private async Task LoadNodeChildren(TreeNodeCollection nodes, int? parentId)
        {
            nodes.Clear();
            List<Position> positions = parentId != null ? await this._positionsReposytory.GetPositionsOfDepartmentAsync((int)parentId) : new List<Position>();
            foreach (Position position in positions)
            {
                TreeNode node = new TreeNode();
                node.Text = position.Name;
                node.Tag = position;
                nodes.Add(node);

            }
            List<Department> departments = await this._departmentsReposytory.GetAffiliateDepartmentsAsync(parentId);
            foreach (Department department in departments)
            {
                TreeNode node = new TreeNode();
                node.Text = department.Name;
                node.Tag = department;
                nodes.Add(node);
                await this.LoadNodeChildren(node.Nodes, department.Id);
            }
        }

        private async void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            if (!(this.treeView1.SelectedNode.Tag is Department))
            {
                return;
            }
            NodeEdit nodeEdit = new NodeEdit(TreeType.Должность, string.Empty);
            nodeEdit.ShowDialog();
            if (nodeEdit.DialogResult == DialogResult.OK)
            {
                TreeType treeType = TreeType.Должность;
                string name = string.Empty;
                nodeEdit.GetResult(out treeType, out name);
                await this.AddNewStructureNode(treeType, name);
            }
        }

        private async Task AddNewStructureNode(TreeType treeType, string name)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            var curentDepartmentId = ((Department)this.treeView1.SelectedNode.Tag).Id;
            if (treeType == TreeType.Должность)
            {
                Position position = new Position();
                position.Name = name;
                position.DepartmentId = curentDepartmentId;
                await this._positionsReposytory.AddAsync(position);
            }
            else
            {
                Department department = new Department();
                department.Name = name;
                department.ParentDepartment = curentDepartmentId;
                await this._departmentsReposytory.AddAsync(department);
            }
            await this.LoadNodeChildren(this.treeView1.SelectedNode.Nodes, curentDepartmentId);
        }

        private void toolStripButtonEdit_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            TreeType treeType = TreeType.Должность;
            if (this.treeView1.SelectedNode.Tag is Department)
            {
                treeType = TreeType.Подразделение;
            }
            string name = (this.treeView1.SelectedNode.Tag as INamed).Name;
            NodeEdit nodeEdit = new NodeEdit(treeType, name);
            nodeEdit.ShowDialog();
            if (nodeEdit.DialogResult == DialogResult.OK)
            {
                nodeEdit.GetResult(out treeType, out name);
                this.EditStructureNode(treeType, name);
            }
        }

        private void EditStructureNode(TreeType treeType, string name)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            (this.treeView1.SelectedNode.Tag as INamed).Name = name;
            if (this.treeView1.SelectedNode.Tag is Department)
            {
                if (((Department)this.treeView1.SelectedNode.Tag).ParentDepartment == null)
                {
                    return;
                }
                this._departmentsReposytory.Update((Department)this.treeView1.SelectedNode.Tag);
            }
            else
            {
                this._positionsReposytory.Update((Position)this.treeView1.SelectedNode.Tag);
            }
            this.treeView1.SelectedNode.Text = name;
        }

        private async void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            this.treeView1.BeginUpdate();
            int? parentId = null;
            if (this.treeView1.SelectedNode.Tag is Department)
            {
                parentId = (this.treeView1.SelectedNode.Tag as Department).ParentDepartment;
                if (parentId == null)
                {
                    return;
                }
                await this._departmentsReposytory.DeleteDepartmentsCascadeAsync((Department)this.treeView1.SelectedNode.Tag);
            }
            else
            {
                parentId = (this.treeView1.SelectedNode.Tag as Position).DepartmentId;
                this._positionsReposytory.Delete((Position)this.treeView1.SelectedNode.Tag);
            }
            await this.LoadNodeChildren(this.treeView1.SelectedNode.Parent.Nodes, parentId);
            this.treeView1.EndUpdate();
        }

        private void toolStripButtonEmployees_Click(object sender, EventArgs e)
        {
            EmployeeDictionary employeeDictionary = new EmployeeDictionary(this._employeesReposytory);
            employeeDictionary.ShowDialog();
        }

        private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            if (!(this.treeView1.SelectedNode.Tag is Position))
            {
                this.DisableEmployeeInfoBox();
                return;
            }
            this.groupBoxEmployee.Enabled = true;
            this.ClearEmployeeInfoBox();
            await this.SetEmployeeInfoBoxValues((Position)this.treeView1.SelectedNode.Tag);
        }

        private void DisableEmployeeInfoBox()
        {
            this.groupBoxEmployee.Enabled = false;
            this.ClearEmployeeInfoBox();
        }

        private void ClearEmployeeInfoBox()
        {
            this.textBoxName.Text = string.Empty;
            this.textBoxPatronymic.Text = string.Empty;
            this.textBoxSurname.Text = string.Empty;
            this.dateTimePickerDateOfEmployment.Value = DateTime.Now;
        }

        private async Task SetEmployeeInfoBoxValues(Position position)
        {
            if (position.EmployeeId == null)
            {
                return;
            }
            Employee employee = await this._employeesReposytory.GetAsync((int)position.EmployeeId);
            this.textBoxName.Text = employee.Name;
            this.textBoxPatronymic.Text = employee.Patronymic;
            this.textBoxSurname.Text = employee.Surname;
            this.dateTimePickerDateOfEmployment.Value = position.DateOfEmployment;
        }

        private async void buttonSelectEmployee_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            if (!(this.treeView1.SelectedNode.Tag is Position))
            {
                return;
            }
            Position position = (Position)this.treeView1.SelectedNode.Tag;
            EmployeeSelection employeeSelection = new EmployeeSelection(this._employeesReposytory);
            employeeSelection.ShowDialog();
            if (employeeSelection.DialogResult == DialogResult.OK)
            {
                Employee employee = employeeSelection.GetResult();
                position.EmployeeId = employee.Id;
                position.DateOfEmployment = DateTime.Now;
                this._positionsReposytory.Update(position);
                await this.SetEmployeeInfoBoxValues((Position)this.treeView1.SelectedNode.Tag);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode == null)
            {
                return;
            }
            if (!(this.treeView1.SelectedNode.Tag is Position))
            {
                return;
            }
            Position position = (Position)this.treeView1.SelectedNode.Tag;
            position.EmployeeId = null;
            this._positionsReposytory.Update(position);
            this.ClearEmployeeInfoBox();
        }
    }
}