using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for creating a new invoice.
/// </summary>
public partial class InvoiceEditForm : Form
{
    private readonly InvoiceService _invoiceService = new();
    private readonly PatientService _patientService = new();
    private readonly TreatmentService _treatmentService = new();

    public InvoiceEditForm()
    {
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        Text = "Новый счет";
        var patients = _patientService.GetAll();
        cmbPatient.DisplayMember = "FullName";
        cmbPatient.ValueMember = "Id";
        cmbPatient.DataSource = patients;
        cmbPatient.SelectedIndexChanged += CmbPatient_SelectedIndexChanged;
        dtpDate.Value = DateTime.Today;
    }

    private void CmbPatient_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (cmbPatient.SelectedValue is int patientId)
        {
            var treatments = _treatmentService.GetAll(patientId);
            lstTreatments.Items.Clear();
            foreach (var t in treatments)
            {
                lstTreatments.Items.Add(new ListViewItem(new[]
                {
                    t.Id.ToString(),
                    t.TreatmentDate.ToString("dd.MM.yyyy"),
                    t.TreatmentType,
                    t.Description,
                    t.Cost.ToString("N2")
                })
                { Tag = t, Checked = false });
            }
        }
    }

    private void lstTreatments_ItemChecked(object? sender, ItemCheckedEventArgs e)
    {
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        decimal total = 0;
        foreach (ListViewItem item in lstTreatments.CheckedItems)
        {
            if (item.Tag is Treatment t)
                total += t.Cost;
        }
        numTotal.Value = Math.Min(total, numTotal.Maximum);
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (cmbPatient.SelectedValue == null)
        {
            MessageBox.Show("Выберите пациента.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (numTotal.Value <= 0)
        {
            MessageBox.Show("Сумма счета должна быть больше 0.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var invoice = new Invoice
            {
                PatientId = (int)cmbPatient.SelectedValue,
                InvoiceDate = dtpDate.Value.Date,
                TotalAmount = numTotal.Value,
                PaidAmount = 0,
                Status = InvoiceStatus.Unpaid,
                Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim()
            };

            var items = new List<InvoiceItem>();
            foreach (ListViewItem item in lstTreatments.CheckedItems)
            {
                if (item.Tag is Treatment t)
                {
                    items.Add(new InvoiceItem
                    {
                        TreatmentId = t.Id,
                        Description = $"{t.TreatmentType}: {t.Description}",
                        Quantity = 1,
                        UnitPrice = t.Cost
                    });
                }
            }

            _invoiceService.Create(invoice, items.Count > 0 ? items : null);
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
