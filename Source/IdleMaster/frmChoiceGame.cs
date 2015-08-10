using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleMaster
{
	public partial class frmChoiceGame : Form
	{
		public frmChoiceGame()
		{
			InitializeComponent();
		}

		public void Initialize(IEnumerable<Badge> CanIdleBadges)
		{
			if (CanIdleBadges == null)
				return;

			try
			{
				_GamesDataGridView.Rows.Clear();

				foreach (var item in CanIdleBadges)
				{
					var row = new DataGridViewRow();
					row.CreateCells(_GamesDataGridView, item.Name, item.RemainingCard, item.AveragePrice);
					row.Tag = item;

					_GamesDataGridView.Rows.Add(row);
				}
			}
			finally
			{
				_UpdateButtonState();
			}
		}

		private void _UpdateButtonState()
		{
			_OkButton.Enabled = _GamesDataGridView.SelectedRows.Count != 0;
		}

		private void _GamesDataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1)
				return;

			if (_OkButton.Enabled)
				_OkButton.PerformClick();
		}

		private void _GamesDataGridView_SelectionChanged(object sender, EventArgs e)
		{
			_UpdateButtonState();
		}

		public Badge Badge
		{
			get
			{
				if (_GamesDataGridView.SelectedRows.Count == 0)
					return null;

				return _GamesDataGridView.SelectedRows[0].Tag as Badge;
			}
			set
			{
				_GamesDataGridView.ClearSelection();

				if (value == null)
					return;

				for (int i = 0; i < _GamesDataGridView.Rows.Count; i++)
				{
					if (value != _GamesDataGridView.Rows[i].Tag)
						continue;
					
					_GamesDataGridView.Rows[i].Selected = true;
					break;
				}
			}
		}

		public List<Badge> SortedBadges
		{
			get
			{
				var badges = new List<Badge>();

				for (int i = 0; i < _GamesDataGridView.Rows.Count; i++)
				{
					var badge = _GamesDataGridView.Rows[i].Tag as Badge;
					if (badge == null)
						continue;

					badges.Add(badge);
				}


				return badges;
			}
		}
	}
}
