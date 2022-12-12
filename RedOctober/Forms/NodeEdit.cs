using RedOctober.ModelView.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedOctober.Forms
{
    public partial class NodeEdit : Form
    {
        private TreeType _treeType;
        private string _name;
        public NodeEdit(TreeType treeType, string name)
        {
            InitializeComponent();

            foreach (int i in Enum.GetValues(typeof(TreeType)))
            {
                this.comboBoxTreeType.Items.Add((TreeType)i);
            }
            this._treeType = treeType;
            this._name = name;
            this.comboBoxTreeType.SelectedItem = this._treeType;
            this.textBoxName.Text = this._name;
            if (!string.IsNullOrEmpty(this._name))
            {
                this.comboBoxTreeType.Enabled = false;
            }
        }

        /// <summary>
        /// Получение типа и имени элемента
        /// </summary>
        /// <param name="treeType">Тип</param>
        /// <param name="name">Имя</param>
        public void GetResult(out TreeType treeType, out string name)
        {
            treeType = this._treeType;
            name = this._name;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this._treeType = (TreeType)this.comboBoxTreeType.SelectedItem;
            this._name = this.textBoxName.Text;
            if (!string.IsNullOrEmpty(this._name))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
