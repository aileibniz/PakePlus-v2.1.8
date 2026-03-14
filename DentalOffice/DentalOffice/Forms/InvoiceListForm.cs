using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for listing and managing invoices.
/// </summary>
public partial class InvoiceListForm : Form
{
    private readonly InvoiceService _invoiceService = new();

    public InvoiceListForm()
    {
        InitializeComponent();
        cmbStatusFilter.Items.Add("Все");
        cmbStatusFilter.Items.AddRange(InvoiceStatus.All);
        cmbStatusFilter.SelectedIndex = 0;
        LoadInvoices();
    }

    private void LoadInvoices()
    {
        string? status = cmbStatusFilter.SelectedIndex > 0 ? cmbStatusFilter.SelectedItem?.ToString() : null;
        var invoices = _invoiceService.GetAll(status: status);
        dgvInvoices.DataSource = invoices;
        FormatGrid();
        lblCount.Text = $"Всего: {invoices.Count}";
    }

    private void FormatGrid()
    {
        if (dgvInvoices.Columns.Count == 0) return;

        var hide = new[] { "Id", "PatientId", "CreatedAt" };
        foreach (var col in hide)
            if (dgvInvoices.Columns.Contains(col))
                dgvInvoices.Columns[col]!.Visible = false;

        if (dgvInvoices.Columns.Contains("PatientName"))
            dgvInvoices.Columns["PatientName"]!.HeaderText = "Пациент";
        if (dgvInvoices.Columns.Contains("InvoiceDate"))
            dgvInvoices.Columns["InvoiceDate"]!.HeaderText = "Дата";
        if (dgvInvoices.Columns.Contains("TotalAmount"))
        {
            dgvInvoices.Columns["TotalAmount"]!.HeaderText = "Сумма";
            dgvInvoices.Columns["TotalAmount"]!.DefaultCellStyle.Format = "N2";
        }
        if (dgvInvoices.Columns.Contains("PaidAmount"))
        {
            dgvInvoices.Columns["PaidAmount"]!.HeaderText = "Оплачено";
            dgvInvoices.Columns["PaidAmount"]!.DefaultCellStyle.Format = "N2";
        }
        if (dgvInvoices.Columns.Contains("RemainingAmount"))
        {
            dgvInvoices.Columns["RemainingAmount"]!.HeaderText = "Остаток";
            dgvInvoices.Columns["RemainingAmount"]!.DefaultCellStyle.Format = "N2";
        }
        if (dgvInvoices.Columns.Contains("Status"))
            dgvInvoices.Columns["Status"]!.HeaderText = "Статус";
        if (dgvInvoices.Columns.Contains("PaymentMethod"))
            dgvInvoices.Columns["PaymentMethod"]!.HeaderText = "Способ оплаты";
        if (dgvInvoices.Columns.Contains("Notes"))
            dgvInvoices.Columns["Notes"]!.HeaderText = "Заметки";
    }

    private void btnFilter_Click(object? sender, EventArgs e) => LoadInvoices();

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        using var form = new InvoiceEditForm();
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadInvoices();
    }

    private void btnPayment_Click(object? sender, EventArgs e)
    {
        if (dgvInvoices.SelectedRows.Count == 0) return;
        var id = (int)dgvInvoices.SelectedRows[0].Cells["Id"].Value;
        var invoice = _invoiceService.GetById(id);
        if (invoice == null) return;

        if (invoice.Status == InvoiceStatus.Paid)
        {
            MessageBox.Show("Счет уже полностью оплачен.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var payForm = new PaymentDialog(invoice);
        if (payForm.ShowDialog(this) == DialogResult.OK)
        {
            _invoiceService.AddPayment(id, payForm.PaymentAmount, payForm.SelectedPaymentMethod);
            LoadInvoices();
        }
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvInvoices.SelectedRows.Count == 0) return;
        var id = (int)dgvInvoices.SelectedRows[0].Cells["Id"].Value;

        if (MessageBox.Show("Удалить счет?", "Подтверждение",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            _invoiceService.Delete(id);
            LoadInvoices();
        }
    }
}

/// <summary>
/// Dialog for recording a payment against an invoice.
/// </summary>
public class PaymentDialog : Form
{
    private readonly NumericUpDown _numAmount;
    private readonly ComboBox _cmbMethod;

    public decimal PaymentAmount => _numAmount.Value;
    public string SelectedPaymentMethod => _cmbMethod.SelectedItem?.ToString() ?? PaymentMethods.Cash;

    public PaymentDialog(Invoice invoice)
    {
        Text = $"Оплата счета (остаток: {invoice.RemainingAmount:N2} руб.)";
        Size = new Size(400, 220);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        var lblAmount = new Label { Text = "Сумма оплаты:", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        _numAmount = new NumericUpDown
        {
            Location = new Point(20, 45), Size = new Size(200, 25),
            Maximum = (decimal)invoice.RemainingAmount, DecimalPlaces = 2,
            Value = (decimal)invoice.RemainingAmount,
            Font = new Font("Segoe UI", 10F)
        };

        var lblMethod = new Label { Text = "Способ оплаты:", Location = new Point(20, 80), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        _cmbMethod = new ComboBox
        {
            Location = new Point(20, 105), Size = new Size(200, 25),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        _cmbMethod.Items.AddRange(PaymentMethods.All);
        _cmbMethod.SelectedIndex = 0;

        var btnPay = new Button
        {
            Text = "Оплатить", Location = new Point(20, 145), Size = new Size(120, 35),
            BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand
        };
        btnPay.FlatAppearance.BorderSize = 0;
        btnPay.Click += (s, e) =>
        {
            if (_numAmount.Value <= 0)
            {
                MessageBox.Show("Введите сумму оплаты.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        };

        var btnCancel = new Button
        {
            Text = "Отмена", Location = new Point(160, 145), Size = new Size(120, 35),
            BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F), Cursor = Cursors.Hand
        };
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.AddRange(new Control[] { lblAmount, _numAmount, lblMethod, _cmbMethod, btnPay, btnCancel });
    }
}
