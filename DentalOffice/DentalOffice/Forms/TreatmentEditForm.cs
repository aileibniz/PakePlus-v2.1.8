using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for creating and editing treatment records.
/// </summary>
public partial class TreatmentEditForm : Form
{
    private readonly TreatmentService _treatmentService = new();
    private readonly PatientService _patientService = new();
    private readonly Treatment? _existing;

    public TreatmentEditForm(Treatment? treatment = null)
    {
        _existing = treatment;
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        var patients = _patientService.GetAll();
        cmbPatient.DisplayMember = "FullName";
        cmbPatient.ValueMember = "Id";
        cmbPatient.DataSource = patients;

        cmbType.Items.AddRange(TreatmentTypes.All);
        cmbTooth.Items.AddRange(ToothChart.AllTeeth);

        if (_existing != null)
        {
            Text = "Редактирование лечения";
            cmbPatient.SelectedValue = _existing.PatientId;
            dtpDate.Value = _existing.TreatmentDate;
            cmbType.SelectedItem = _existing.TreatmentType;
            cmbTooth.Text = _existing.ToothNumber ?? "";
            txtDescription.Text = _existing.Description;
            txtDoctor.Text = _existing.DoctorName;
            numCost.Value = _existing.Cost;
            txtNotes.Text = _existing.Notes;
        }
        else
        {
            Text = "Новое лечение";
            dtpDate.Value = DateTime.Today;
            cmbType.SelectedIndex = 0;
        }
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (cmbPatient.SelectedValue == null)
        {
            MessageBox.Show("Выберите пациента.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            MessageBox.Show("Введите описание лечения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtDoctor.Text))
        {
            MessageBox.Show("Введите имя врача.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var treatment = _existing ?? new Treatment();
            treatment.PatientId = (int)cmbPatient.SelectedValue;
            treatment.TreatmentDate = dtpDate.Value.Date;
            treatment.TreatmentType = cmbType.SelectedItem?.ToString() ?? TreatmentTypes.Other;
            treatment.ToothNumber = string.IsNullOrWhiteSpace(cmbTooth.Text) ? null : cmbTooth.Text;
            treatment.Description = txtDescription.Text.Trim();
            treatment.DoctorName = txtDoctor.Text.Trim();
            treatment.Cost = numCost.Value;
            treatment.Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();

            if (_existing != null)
                _treatmentService.Update(treatment);
            else
                _treatmentService.Create(treatment);

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
