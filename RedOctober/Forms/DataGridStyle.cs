using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;

namespace RedOctober.Forms
{
    /// <summary>
    /// Настройки столбцов таблиц
    /// </summary>
    internal class FormatingEmployeeDataGridView
    {
        /// <summary>
        /// Настройка столбцов таблицы сотрудников
        /// </summary>
        /// <param name="dataGridView"></param>
        internal static void DataGridColumnsStyle(DataGridView dataGridView)
        {
            FormatingEmployeeDataGridView.SetDataGridColumnVisible(dataGridView, "Id", false);
            FormatingEmployeeDataGridView.SetDataGridColumnText(dataGridView, "Name", "Имя");
            FormatingEmployeeDataGridView.SetDataGridColumnText(dataGridView, "Patronymic", "Отчество");
            FormatingEmployeeDataGridView.SetDataGridColumnText(dataGridView, "Surname", "Фамилия");
            FormatingEmployeeDataGridView.SetDataGridColumnDisplayIndex(dataGridView, "Name", 2);
            FormatingEmployeeDataGridView.SetDataGridColumnDisplayIndex(dataGridView, "Patronymic", 3);
            FormatingEmployeeDataGridView.SetDataGridColumnDisplayIndex(dataGridView, "Surname", 1);
        }

        private static void SetDataGridColumnVisible(DataGridView dataGridView, string name, bool visible)
        {
            if (dataGridView.Columns.Contains(name))
            {
                dataGridView.Columns[name].Visible = visible;
            }
        }

        private static void SetDataGridColumnText(DataGridView dataGridView, string name, string text)
        {
            if (dataGridView.Columns.Contains(name))
            {
                dataGridView.Columns[name].HeaderText = text;
            }
        }

        private static void SetDataGridColumnDisplayIndex(DataGridView dataGridView, string name, int displayIndex)
        {
            if (dataGridView.Columns.Contains(name))
            {
                dataGridView.Columns[name].DisplayIndex = displayIndex;
            }
        }
    }
}
