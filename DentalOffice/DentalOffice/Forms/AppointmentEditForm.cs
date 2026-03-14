using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for creating and editing appointments.
/// </summary>
public partial class AppointmentEditForm : Form
{
    private readonly AppointmentService _appointmentService = new();
    private readonly PatientService _patientService = new();
    private readonly Appointment? _existing;
    private List<Patient> _patients = new();

    public AppointmentEditForm(Appointment? appointment = null)
    {
        _existing = appointment;
        InitializeComponent();
        SetupForm();
    }

    private void SetupForm()
    {
        _patients = _patientService.GetAll();
        cmbPatient.DisplayMember = "FullName";
        cmbPatient.ValueMember = "Id";
        cmbPatient.DataSource = _patients;

        cmbStatus.Items.AddRange(AppointmentStatus.All);
        cmbTreatmentType.Items.AddRange(TreatmentTypes.All);

        // Time slots every 30 minutes from 8:00 to 20:00
        for (int h = 8; h <= 20; h++)
        {
            cmbStartTime.Items.Add($"{h:D2}:00");
            cmbStartTime.Items.Add($"{h:D2}:30");
            cmbEndTime.Items.Add($"{h:D2}:00");
            cmbEndTime.Items.Add($"{h:D2}:30");
        }

        if (_existing != null)
        {
            Text = "Редактирование приема";
            cmbPatient.SelectedValue = _existing.PatientId;
            dtpDate.Value = _existing.AppointmentDate;
            cmbStartTime.Text = _existing.StartTime.ToString(@"hh\:mm");
            cmbEndTime.Text = _existing.EndTime.ToString(@"hh\:mm");
            txtDoctor.Text = _existing.DoctorName;
            cmbStatus.SelectedItem = _existing.Status;
            cmbTreatmentType.SelectedItem = _existing.TreatmentType;
            txtNotes.Text = _existing.Notes;
        }
        else
        {
            Text = "Новый прием";
            dtpDate.Value = DateTime.Today;
            cmbStartTime.SelectedIndex = 0;
            cmbEndTime.SelectedIndex = 1;
            cmbStatus.SelectedIndex = 0;
        }
    }

    private void btnSave_Click(object? sender, EventArgs e)
    {
        if (cmbPatient.SelectedValue == null)
        {
            MessageBox.Show("Выберите пациента.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (string.IsNullOrWhiteSpace(txtDoctor.Text))
        {
            MessageBox.Show("Введите имя врача.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            var appt = _existing ?? new Appointment();
            appt.PatientId = (int)cmbPatient.SelectedValue;
            appt.AppointmentDate = dtpDate.Value.Date;
            appt.StartTime = TimeSpan.Parse(cmbStartTime.Text);
            appt.EndTime = TimeSpan.Parse(cmbEndTime.Text);
            appt.DoctorName = txtDoctor.Text.Trim();
            appt.Status = cmbStatus.SelectedItem?.ToString() ?? AppointmentStatus.Scheduled;
            appt.TreatmentType = cmbTreatmentType.SelectedItem?.ToString();
            appt.Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();

            if (_existing != null)
                _appointmentService.Update(appt);
            else
                _appointmentService.Create(appt);

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
