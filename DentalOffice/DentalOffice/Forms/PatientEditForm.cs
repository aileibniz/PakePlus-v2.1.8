using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for creating and editing patient records.
/// </summary>
public partial class PatientEditForm : Form
{
    private readonly PatientService _patientService = new();
    private readonly Patient? _existingPatient;

    public PatientEditForm(Patient? patient = null)
    {
        _existingPatient = patient;
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        cmbGender.Items.AddRange(new[] { "Мужской", "Женский" });

        if (_existingPatient != null)
        {
            Text = "Редактирование пациента";
            txtLastName.Text = _existingPatient.LastName;
            txtFirstName.Text = _existingPatient.FirstName;
            txtMiddleName.Text = _existingPatient.MiddleName;
            dtpBirthDate.Value = _existingPatient.DateOfBirth;
            cmbGender.SelectedItem = _existingPatient.Gender;
            txtPhone.Text = _existingPatient.Phone;
            txtEmail.Text = _existingPatient.Email;
            txtAddress.Text = _existingPatient.Address;
            txtInsurance.Text = _existingPatient.InsurancePolicy;
            txtAllergies.Text = _existingPatient.Allergies;
            txtMedicalNotes.Text = _existingPatient.MedicalNotes;
        }
        else
        {
            Text = "Новый пациент";
            cmbGender.SelectedIndex = 0;
        }
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput()) return;

        try
        {
            var patient = _existingPatient ?? new Patient();
            patient.LastName = txtLastName.Text.Trim();
            patient.FirstName = txtFirstName.Text.Trim();
            patient.MiddleName = string.IsNullOrWhiteSpace(txtMiddleName.Text) ? null : txtMiddleName.Text.Trim();
            patient.DateOfBirth = dtpBirthDate.Value.Date;
            patient.Gender = cmbGender.SelectedItem?.ToString() ?? "Мужской";
            patient.Phone = txtPhone.Text.Trim();
            patient.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
            patient.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
            patient.InsurancePolicy = string.IsNullOrWhiteSpace(txtInsurance.Text) ? null : txtInsurance.Text.Trim();
            patient.Allergies = string.IsNullOrWhiteSpace(txtAllergies.Text) ? null : txtAllergies.Text.Trim();
            patient.MedicalNotes = string.IsNullOrWhiteSpace(txtMedicalNotes.Text) ? null : txtMedicalNotes.Text.Trim();

            if (_existingPatient != null)
                _patientService.Update(patient);
            else
                _patientService.Create(patient);

            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(txtLastName.Text))
        {
            MessageBox.Show("Введите фамилию.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtLastName.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtFirstName.Text))
        {
            MessageBox.Show("Введите имя.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtFirstName.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(txtPhone.Text))
        {
            MessageBox.Show("Введите телефон.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPhone.Focus();
            return false;
        }
        return true;
    }

    private void btnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
