namespace DentalOffice.Forms;

partial class InvoiceListForm
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
        cmbStatusFilter = new ComboBox();
        btnFilter = new Button();
        btnAdd = new Button();
        btnPayment = new Button();
        btnDelete = new Button();
        lblCount = new Label();
        dgvInvoices = new DataGridView();

        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvInvoices).BeginInit();
        SuspendLayout();

        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 60;
        panelTop.Padding = new Padding(10);
        panelTop.Controls.Add(cmbStatusFilter);
        panelTop.Controls.Add(btnFilter);
        panelTop.Controls.Add(btnAdd);
        panelTop.Controls.Add(btnPayment);
        panelTop.Controls.Add(btnDelete);
        panelTop.Controls.Add(lblCount);

        cmbStatusFilter.Location = new Point(10, 17);
        cmbStatusFilter.Size = new Size(150, 25);
        cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;

        btnFilter.Location = new Point(170, 15);
        btnFilter.Size = new Size(90, 30);
        btnFilter.Text = "Фильтр";
        btnFilter.BackColor = Color.FromArgb(0, 123, 255);
        btnFilter.ForeColor = Color.White;
        btnFilter.FlatStyle = FlatStyle.Flat;
        btnFilter.FlatAppearance.BorderSize = 0;
        btnFilter.Cursor = Cursors.Hand;
        btnFilter.Click += btnFilter_Click;

        btnAdd.Location = new Point(280, 15);
        btnAdd.Size = new Size(120, 30);
        btnAdd.Text = "Новый счет";
        btnAdd.BackColor = Color.FromArgb(40, 167, 69);
        btnAdd.ForeColor = Color.White;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.Cursor = Cursors.Hand;
        btnAdd.Click += btnAdd_Click;

        btnPayment.Location = new Point(410, 15);
        btnPayment.Size = new Size(120, 30);
        btnPayment.Text = "Оплата";
        btnPayment.BackColor = Color.FromArgb(23, 162, 184);
        btnPayment.ForeColor = Color.White;
        btnPayment.FlatStyle = FlatStyle.Flat;
        btnPayment.FlatAppearance.BorderSize = 0;
        btnPayment.Cursor = Cursors.Hand;
        btnPayment.Click += btnPayment_Click;

        btnDelete.Location = new Point(540, 15);
        btnDelete.Size = new Size(90, 30);
        btnDelete.Text = "Удалить";
        btnDelete.BackColor = Color.FromArgb(220, 53, 69);
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.Cursor = Cursors.Hand;
        btnDelete.Click += btnDelete_Click;

        lblCount.Location = new Point(650, 22);
        lblCount.AutoSize = true;
        lblCount.Font = new Font("Segoe UI", 10F);

        dgvInvoices.Dock = DockStyle.Fill;
        dgvInvoices.ReadOnly = true;
        dgvInvoices.AllowUserToAddRows = false;
        dgvInvoices.AllowUserToDeleteRows = false;
        dgvInvoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvInvoices.MultiSelect = false;
        dgvInvoices.BackgroundColor = Color.White;
        dgvInvoices.BorderStyle = BorderStyle.None;
        dgvInvoices.RowHeadersVisible = false;
        dgvInvoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvInvoices.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        dgvInvoices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgvInvoices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
        dgvInvoices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvInvoices.EnableHeadersVisualStyles = false;
        dgvInvoices.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 500);
        Text = "Счета и оплата";
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        Controls.Add(dgvInvoices);
        Controls.Add(panelTop);

        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvInvoices).EndInit();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private ComboBox cmbStatusFilter;
    private Button btnFilter;
    private Button btnAdd;
    private Button btnPayment;
    private Button btnDelete;
    private Label lblCount;
    private DataGridView dgvInvoices;
}
