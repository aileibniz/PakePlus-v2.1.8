using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for listing and managing appointments with date filtering.
/// </summary>
public partial class AppointmentListForm : Form
{
    private readonly AppointmentService _appointmentService = new();

    public AppointmentListForm()
    {
        InitializeComponent();
        dtpFilter.Value = DateTime.Today;
        cmbStatusFilter.Items.Add("Все");
        cmbStatusFilter.Items.AddRange(AppointmentStatus.All);
        cmbStatusFilter.SelectedIndex = 0;
        LoadAppointments();
    }

    private void LoadAppointments()
    {
        DateTime? date = chkFilterDate.Checked ? dtpFilter.Value.Date : null;
        string? status = cmbStatusFilter.SelectedIndex > 0 ? cmbStatusFilter.SelectedItem?.ToString() : null;

        var appointments = _appointmentService.GetAll(date, status);
        dgvAppointments.DataSource = appointments;
        FormatGrid();
        lblCount.Text = $"Всего: {appointments.Count}";
    }

    private void FormatGrid()
    {
        if (dgvAppointments.Columns.Count == 0) return;

        var hide = new[] { "Id", "PatientId", "CreatedAt", "StartTime", "EndTime" };
        foreach (var col in hide)
            if (dgvAppointments.Columns.Contains(col))
                dgvAppointments.Columns[col]!.Visible = false;

        if (dgvAppointments.Columns.Contains("PatientName"))
            dgvAppointments.Columns["PatientName"]!.HeaderText = "Пациент";
        if (dgvAppointments.Columns.Contains("AppointmentDate"))
            dgvAppointments.Columns["AppointmentDate"]!.HeaderText = "Дата";
        if (dgvAppointments.Columns.Contains("TimeRange"))
            dgvAppointments.Columns["TimeRange"]!.HeaderText = "Время";
        if (dgvAppointments.Columns.Contains("DoctorName"))
            dgvAppointments.Columns["DoctorName"]!.HeaderText = "Врач";
        if (dgvAppointments.Columns.Contains("Status"))
            dgvAppointments.Columns["Status"]!.HeaderText = "Статус";
        if (dgvAppointments.Columns.Contains("TreatmentType"))
            dgvAppointments.Columns["TreatmentType"]!.HeaderText = "Тип";
        if (dgvAppointments.Columns.Contains("Notes"))
            dgvAppointments.Columns["Notes"]!.HeaderText = "Заметки";
    }

    private void btnFilter_Click(object? sender, EventArgs e) => LoadAppointments();

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        using var form = new AppointmentEditForm();
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadAppointments();
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        if (dgvAppointments.SelectedRows.Count == 0) return;
        var id = (int)dgvAppointments.SelectedRows[0].Cells["Id"].Value;
        var appt = _appointmentService.GetById(id);
        if (appt == null) return;

        using var form = new AppointmentEditForm(appt);
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadAppointments();
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        if (dgvAppointments.SelectedRows.Count == 0) return;
        var id = (int)dgvAppointments.SelectedRows[0].Cells["Id"].Value;

        if (MessageBox.Show("Удалить запись приема?", "Подтверждение",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        {
            _appointmentService.Delete(id);
            LoadAppointments();
        }
    }

    private void dgvAppointments_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) btnEdit_Click(sender, e);
    }
}
