namespace IdleMaster
{
	partial class frmChoiceGame
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChoiceGame));
			this._GamesDataGridView = new System.Windows.Forms.DataGridView();
			this._NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._CardsCountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._AveragePriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._OkButton = new System.Windows.Forms.Button();
			this._CancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._GamesDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// _GamesDataGridView
			// 
			this._GamesDataGridView.AllowUserToAddRows = false;
			this._GamesDataGridView.AllowUserToDeleteRows = false;
			this._GamesDataGridView.AllowUserToResizeColumns = false;
			this._GamesDataGridView.AllowUserToResizeRows = false;
			this._GamesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._GamesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._GamesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._NameColumn,
            this._CardsCountColumn,
            this._AveragePriceColumn});
			this._GamesDataGridView.Location = new System.Drawing.Point(12, 12);
			this._GamesDataGridView.MultiSelect = false;
			this._GamesDataGridView.Name = "_GamesDataGridView";
			this._GamesDataGridView.ReadOnly = true;
			this._GamesDataGridView.RowHeadersVisible = false;
			this._GamesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this._GamesDataGridView.Size = new System.Drawing.Size(438, 175);
			this._GamesDataGridView.TabIndex = 0;
			this._GamesDataGridView.TabStop = false;
			this._GamesDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._GamesDataGridView_CellDoubleClick);
			this._GamesDataGridView.SelectionChanged += new System.EventHandler(this._GamesDataGridView_SelectionChanged);
			// 
			// _NameColumn
			// 
			this._NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this._NameColumn.HeaderText = "Game name";
			this._NameColumn.Name = "_NameColumn";
			this._NameColumn.ReadOnly = true;
			// 
			// _CardsCountColumn
			// 
			this._CardsCountColumn.HeaderText = "Cards count";
			this._CardsCountColumn.Name = "_CardsCountColumn";
			this._CardsCountColumn.ReadOnly = true;
			// 
			// _AveragePriceColumn
			// 
			this._AveragePriceColumn.HeaderText = "Average price";
			this._AveragePriceColumn.Name = "_AveragePriceColumn";
			this._AveragePriceColumn.ReadOnly = true;
			// 
			// _OkButton
			// 
			this._OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this._OkButton.Location = new System.Drawing.Point(294, 193);
			this._OkButton.Name = "_OkButton";
			this._OkButton.Size = new System.Drawing.Size(75, 23);
			this._OkButton.TabIndex = 1;
			this._OkButton.Text = "OK";
			this._OkButton.UseVisualStyleBackColor = true;
			// 
			// _CancelButton
			// 
			this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this._CancelButton.Location = new System.Drawing.Point(375, 193);
			this._CancelButton.Name = "_CancelButton";
			this._CancelButton.Size = new System.Drawing.Size(75, 23);
			this._CancelButton.TabIndex = 2;
			this._CancelButton.Text = "Cancel";
			this._CancelButton.UseVisualStyleBackColor = true;
			// 
			// frmChoiceGame
			// 
			this.AcceptButton = this._OkButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this._CancelButton;
			this.ClientSize = new System.Drawing.Size(462, 228);
			this.Controls.Add(this._CancelButton);
			this.Controls.Add(this._OkButton);
			this.Controls.Add(this._GamesDataGridView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmChoiceGame";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Idle Master Choice game";
			((System.ComponentModel.ISupportInitialize)(this._GamesDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _GamesDataGridView;
		private System.Windows.Forms.Button _OkButton;
		private System.Windows.Forms.Button _CancelButton;
		private System.Windows.Forms.DataGridViewTextBoxColumn _NameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _CardsCountColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn _AveragePriceColumn;
	}
}