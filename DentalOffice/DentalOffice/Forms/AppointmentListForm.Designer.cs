namespace DentalOffice.Forms;

partial class AppointmentListForm
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
        panelTop = new Panel();
        chkFilterDate = new CheckBox();
        dtpFilter = new DateTimePicker();
        cmbStatusFilter = new ComboBox();
        btnFilter = new Button();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        lblCount = new Label();
        dgvAppointments = new DataGridView();

        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAppointments).BeginInit();
        SuspendLayout();

        // panelTop
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 60;
        panelTop.Padding = new Padding(10);
        panelTop.Controls.Add(chkFilterDate);
        panelTop.Controls.Add(dtpFilter);
        panelTop.Controls.Add(cmbStatusFilter);
        panelTop.Controls.Add(btnFilter);
        panelTop.Controls.Add(btnAdd);
        panelTop.Controls.Add(btnEdit);
        panelTop.Controls.Add(btnDelete);
        panelTop.Controls.Add(lblCount);

        chkFilterDate.Text = "По дате:";
        chkFilterDate.Location = new Point(10, 20);
        chkFilterDate.AutoSize = true;
        chkFilterDate.Checked = true;
        chkFilterDate.Font = new Font("Segoe UI", 10F);

        dtpFilter.Location = new Point(100, 17);
        dtpFilter.Size = new Size(150, 25);
        dtpFilter.Format = DateTimePickerFormat.Short;

        cmbStatusFilter.Location = new Point(260, 17);
        cmbStatusFilter.Size = new Size(140, 25);
        cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;

        btnFilter.Location = new Point(410, 15);
        btnFilter.Size = new Size(90, 30);
        btnFilter.Text = "Фильтр";
        btnFilter.BackColor = Color.FromArgb(0, 123, 255);
        btnFilter.ForeColor = Color.White;
        btnFilter.FlatStyle = FlatStyle.Flat;
        btnFilter.FlatAppearance.BorderSize = 0;
        btnFilter.Cursor = Cursors.Hand;
        btnFilter.Click += btnFilter_Click;

        btnAdd.Location = new Point(520, 15);
        btnAdd.Size = new Size(100, 30);
        btnAdd.Text = "Добавить";
        btnAdd.BackColor = Color.FromArgb(40, 167, 69);
        btnAdd.ForeColor = Color.White;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Cursor = Cursors.Hand;
        btnAdd.Click += btnAdd_Click;

        btnEdit.Location = new Point(630, 15);
        btnEdit.Size = new Size(110, 30);
        btnEdit.Text = "Редактировать";
        btnEdit.BackColor = Color.FromArgb(255, 193, 7);
        btnEdit.ForeColor = Color.Black;
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.FlatAppearance.BorderSize = 0;
        btnEdit.Cursor = Cursors.Hand;
        btnEdit.Click += btnEdit_Click;

        btnDelete.Location = new Point(750, 15);
        btnDelete.Size = new Size(90, 30);
        btnDelete.Text = "Удалить";
        btnDelete.BackColor = Color.FromArgb(220, 53, 69);
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.Cursor = Cursors.Hand;
        btnDelete.Click += btnDelete_Click;

        lblCount.Location = new Point(860, 22);
        lblCount.AutoSize = true;
        lblCount.Font = new Font("Segoe UI", 10F);

        // dgvAppointments
        dgvAppointments.Dock = DockStyle.Fill;
        dgvAppointments.ReadOnly = true;
        dgvAppointments.AllowUserToAddRows = false;
        dgvAppointments.AllowUserToDeleteRows = false;
        dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvAppointments.MultiSelect = false;
        dgvAppointments.BackgroundColor = Color.White;
        dgvAppointments.BorderStyle = BorderStyle.None;
        dgvAppointments.RowHeadersVisible = false;
        dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvAppointments.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        dgvAppointments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgvAppointments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
        dgvAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvAppointments.EnableHeadersVisualStyles = false;
        dgvAppointments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        dgvAppointments.CellDoubleClick += dgvAppointments_CellDoubleClick;

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 550);
        Text = "Расписание приемов";
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        Controls.Add(dgvAppointments);
        Controls.Add(panelTop);

        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvAppointments).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private CheckBox chkFilterDate;
    private DateTimePicker dtpFilter;
    private ComboBox cmbStatusFilter;
    private Button btnFilter;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Label lblCount;
    private DataGridView dgvAppointments;
}
