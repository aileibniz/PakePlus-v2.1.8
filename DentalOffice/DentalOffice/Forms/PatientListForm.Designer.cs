namespace DentalOffice.Forms;

partial class PatientListForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        btnToothChart = new Button();
        dgvPatients = new DataGridView();
        lblCount = new Label();
        panelTop = new Panel();

        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
        SuspendLayout();

        // panelTop
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 60;
        panelTop.Padding = new Padding(10);
        panelTop.Controls.Add(txtSearch);
        panelTop.Controls.Add(btnSearch);
        panelTop.Controls.Add(btnAdd);
        panelTop.Controls.Add(btnEdit);
        panelTop.Controls.Add(btnDelete);
        panelTop.Controls.Add(btnToothChart);
        panelTop.Controls.Add(lblCount);

        // txtSearch
        txtSearch.Location = new Point(10, 18);
        txtSearch.Size = new Size(250, 25);
        txtSearch.PlaceholderText = "Поиск по фамилии, имени, телефону...";
        txtSearch.Font = new Font("Segoe UI", 10F);
        txtSearch.KeyDown += txtSearch_KeyDown;

        // btnSearch
        btnSearch.Location = new Point(270, 16);
        btnSearch.Size = new Size(80, 30);
        btnSearch.Text = "Поиск";
        btnSearch.BackColor = Color.FromArgb(0, 123, 255);
        btnSearch.ForeColor = Color.White;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.Cursor = Cursors.Hand;
        btnSearch.Click += btnSearch_Click;

        // btnAdd
        btnAdd.Location = new Point(380, 16);
        btnAdd.Size = new Size(100, 30);
        btnAdd.Text = "Добавить";
        btnAdd.BackColor = Color.FromArgb(40, 167, 69);
        btnAdd.ForeColor = Color.White;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Cursor = Cursors.Hand;
        btnAdd.Click += btnAdd_Click;

        // btnEdit
        btnEdit.Location = new Point(490, 16);
        btnEdit.Size = new Size(110, 30);
        btnEdit.Text = "Редактировать";
        btnEdit.BackColor = Color.FromArgb(255, 193, 7);
        btnEdit.ForeColor = Color.Black;
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.FlatAppearance.BorderSize = 0;
        btnEdit.Cursor = Cursors.Hand;
        btnEdit.Click += btnEdit_Click;

        // btnDelete
        btnDelete.Location = new Point(610, 16);
        btnDelete.Size = new Size(90, 30);
        btnDelete.Text = "Удалить";
        btnDelete.BackColor = Color.FromArgb(220, 53, 69);
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.Cursor = Cursors.Hand;
        btnDelete.Click += btnDelete_Click;

        // btnToothChart
        btnToothChart.Location = new Point(710, 16);
        btnToothChart.Size = new Size(120, 30);
        btnToothChart.Text = "Зубная карта";
        btnToothChart.BackColor = Color.FromArgb(23, 162, 184);
        btnToothChart.ForeColor = Color.White;
        btnToothChart.FlatStyle = FlatStyle.Flat;
        btnToothChart.FlatAppearance.BorderSize = 0;
        btnToothChart.Cursor = Cursors.Hand;
        btnToothChart.Click += btnToothChart_Click;

        // lblCount
        lblCount.Location = new Point(850, 22);
        lblCount.AutoSize = true;
        lblCount.Font = new Font("Segoe UI", 10F);

        // dgvPatients
        dgvPatients.Dock = DockStyle.Fill;
        dgvPatients.ReadOnly = true;
        dgvPatients.AllowUserToAddRows = false;
        dgvPatients.AllowUserToDeleteRows = false;
        dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvPatients.MultiSelect = false;
        dgvPatients.BackgroundColor = Color.White;
        dgvPatients.BorderStyle = BorderStyle.None;
        dgvPatients.RowHeadersVisible = false;
        dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvPatients.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        dgvPatients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgvPatients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
        dgvPatients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvPatients.EnableHeadersVisualStyles = false;
        dgvPatients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        dgvPatients.CellDoubleClick += dgvPatients_CellDoubleClick;

        // PatientListForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 550);
        Text = "Пациенты";
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        Controls.Add(dgvPatients);
        Controls.Add(panelTop);

        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
        ResumeLayout(false);
    }

    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Button btnToothChart;
    private DataGridView dgvPatients;
    private Label lblCount;
    private Panel panelTop;
}
