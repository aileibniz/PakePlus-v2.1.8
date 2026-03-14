using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Main application form with navigation panel and dashboard.
/// </summary>
public partial class MainForm : Form
{
    private readonly PatientService _patientService = new();
    private readonly AppointmentService _appointmentService = new();
    private readonly TreatmentService _treatmentService = new();
    private readonly InvoiceService _invoiceService = new();

    public MainForm()
    {
        InitializeComponent();
        LoadDashboard();
    }

    private void LoadDashboard()
    {
        try
        {
            lblTotalPatients.Text = _patientService.GetTotalCount().ToString();
            lblTodayAppointments.Text = _appointmentService.GetTodayCount().ToString();

            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            lblMonthRevenue.Text = _treatmentService.GetTotalRevenue(monthStart, DateTime.Now).ToString("N2") + " руб.";
            lblUnpaidInvoices.Text = _invoiceService.GetTotalUnpaid().ToString("N2") + " руб.";

            // Load today's appointments into the grid
            var todayAppts = _appointmentService.GetTodayAppointments();
            dgvTodayAppointments.DataSource = todayAppts;
            FormatAppointmentGrid();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void FormatAppointmentGrid()
    {
        if (dgvTodayAppointments.Columns.Count == 0) return;

        dgvTodayAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        var columnsToHide = new[] { "Id", "PatientId", "CreatedAt", "AppointmentDate" };
        foreach (var col in columnsToHide)
        {
            if (dgvTodayAppointments.Columns.Contains(col))
                dgvTodayAppointments.Columns[col]!.Visible = false;
        }

        if (dgvTodayAppointments.Columns.Contains("PatientName"))
            dgvTodayAppointments.Columns["PatientName"]!.HeaderText = "Пациент";
        if (dgvTodayAppointments.Columns.Contains("TimeRange"))
            dgvTodayAppointments.Columns["TimeRange"]!.HeaderText = "Время";
        if (dgvTodayAppointments.Columns.Contains("DoctorName"))
            dgvTodayAppointments.Columns["DoctorName"]!.HeaderText = "Врач";
        if (dgvTodayAppointments.Columns.Contains("Status"))
            dgvTodayAppointments.Columns["Status"]!.HeaderText = "Статус";
        if (dgvTodayAppointments.Columns.Contains("TreatmentType"))
            dgvTodayAppointments.Columns["TreatmentType"]!.HeaderText = "Тип лечения";
        if (dgvTodayAppointments.Columns.Contains("Notes"))
            dgvTodayAppointments.Columns["Notes"]!.HeaderText = "Заметки";
    }

    private void btnPatients_Click(object? sender, EventArgs e)
    {
        using var form = new PatientListForm();
        form.ShowDialog(this);
        LoadDashboard(); // Refresh stats
    }

    private void btnAppointments_Click(object? sender, EventArgs e)
    {
        using var form = new AppointmentListForm();
        form.ShowDialog(this);
        LoadDashboard();
    }

    private void btnTreatments_Click(object? sender, EventArgs e)
    {
        using var form = new TreatmentListForm();
        form.ShowDialog(this);
        LoadDashboard();
    }

    private void btnInvoices_Click(object? sender, EventArgs e)
    {
        using var form = new InvoiceListForm();
        form.ShowDialog(this);
        LoadDashboard();
    }

    private void btnRefresh_Click(object? sender, EventArgs e)
    {
        LoadDashboard();
    }
}
