using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for listing and managing treatment records.
/// </summary>
public partial class TreatmentListForm : Form
{
    private readonly TreatmentService _treatmentService = new();

    public TreatmentListForm()
    {
        InitializeComponent();
        LoadTreatments();
    }

    private void LoadTreatments()
    {
        var treatments = _treatmentService.GetAll();
        dgvTreatments.DataSource = treatments;
        FormatGrid();
        lblCount.Text = $"Всего: {treatments.Count}";
    }

    private void FormatGrid()
    {
        if (dgvTreatments.Columns.Count == 0) return;

        var hide = new[] { "Id", "PatientId", "AppointmentId", "CreatedAt" };
        foreach (var col in hide)
            if (dgvTreatments.Columns.Contains(col))
                dgvTreatments.Columns[col]!.Visible = false;

        if (dgvTreatments.Columns.Contains("PatientName"))
            dgvTreatments.Columns["PatientName"]!.HeaderText = "Пациент";
        if (dgvTreatments.Columns.Contains("TreatmentDate"))
            dgvTreatments.Columns["TreatmentDate"]!.HeaderText = "Дата";
        if (dgvTreatments.Columns.Contains("TreatmentType"))
            dgvTreatments.Columns["TreatmentType"]!.HeaderText = "Тип";
        if (dgvTreatments.Columns.Contains("ToothNumber"))
            dgvTreatments.Columns["ToothNumber"]!.HeaderText = "Зуб";
        if (dgvTreatments.Columns.Contains("Description"))
            dgvTreatments.Columns["Description"]!.HeaderText = "Описание";
        if (dgvTreatments.Columns.Contains("DoctorName"))
            dgvTreatments.Columns["DoctorName"]!.HeaderText = "Врач";
        if (dgvTreatments.Columns.Contains("Cost"))
        {
            dgvTreatments.Columns["Cost"]!.HeaderText = "Стоимость";
            dgvTreatments.Columns["Cost"]!.DefaultCellStyle.Format = "N2";
        }
        if (dgvTreatments.Columns.Contains("Notes"))
            dgvTreatments.Columns["Notes"]!.HeaderText = "Заметки";
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        using var form = new TreatmentEditForm();
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadTreatments();
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvTreatments.SelectedRows.Count == 0) return;
        var id = (int)dgvTreatments.SelectedRows[0].Cells["Id"].Value;
        var treatment = _treatmentService.GetById(id);
        if (treatment == null) return;

        using var form = new TreatmentEditForm(treatment);
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadTreatments();
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvTreatments.SelectedRows.Count == 0) return;
        var id = (int)dgvTreatments.SelectedRows[0].Cells["Id"].Value;

        if (MessageBox.Show("Удалить запись лечения?", "Подтверждение",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            _treatmentService.Delete(id);
            LoadTreatments();
        }
    }

    private void dgvTreatments_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnEdit_Click(sender, e);
    }
}
