namespace DentalOffice.Forms;

partial class TreatmentListForm
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
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        lblCount = new Label();
        dgvTreatments = new DataGridView();

        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTreatments).BeginInit();
        SuspendLayout();

        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 60;
        panelTop.Padding = new Padding(10);
        panelTop.Controls.Add(btnAdd);
        panelTop.Controls.Add(btnEdit);
        panelTop.Controls.Add(btnDelete);
        panelTop.Controls.Add(lblCount);

        btnAdd.Location = new Point(10, 15);
        btnAdd.Size = new Size(100, 30);
        btnAdd.Text = "Добавить";
        btnAdd.BackColor = Color.FromArgb(40, 167, 69);
        btnAdd.ForeColor = Color.White;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Cursor = Cursors.Hand;
        btnAdd.Click += btnAdd_Click;

        btnEdit.Location = new Point(120, 15);
        btnEdit.Size = new Size(110, 30);
        btnEdit.Text = "Редактировать";
        btnEdit.BackColor = Color.FromArgb(255, 193, 7);
        btnEdit.ForeColor = Color.Black;
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.FlatAppearance.BorderSize = 0;
        btnEdit.Cursor = Cursors.Hand;
        btnEdit.Click += btnEdit_Click;

        btnDelete.Location = new Point(240, 15);
        btnDelete.Size = new Size(90, 30);
        btnDelete.Text = "Удалить";
        btnDelete.BackColor = Color.FromArgb(220, 53, 69);
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.Cursor = Cursors.Hand;
        btnDelete.Click += btnDelete_Click;

        lblCount.Location = new Point(350, 22);
        lblCount.AutoSize = true;
        lblCount.Font = new Font("Segoe UI", 10F);

        dgvTreatments.Dock = DockStyle.Fill;
        dgvTreatments.ReadOnly = true;
        dgvTreatments.AllowUserToAddRows = false;
        dgvTreatments.AllowUserToDeleteRows = false;
        dgvTreatments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTreatments.MultiSelect = false;
        dgvTreatments.BackgroundColor = Color.White;
        dgvTreatments.BorderStyle = BorderStyle.None;
        dgvTreatments.RowHeadersVisible = false;
        dgvTreatments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTreatments.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        dgvTreatments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgvTreatments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
        dgvTreatments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvTreatments.EnableHeadersVisualStyles = false;
        dgvTreatments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
        dgvTreatments.CellDoubleClick += dgvTreatments_CellDoubleClick;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1000, 550);
        Text = "Лечение";
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        Controls.Add(dgvTreatments);
        Controls.Add(panelTop);

        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTreatments).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Label lblCount;
    private DataGridView dgvTreatments;
}
