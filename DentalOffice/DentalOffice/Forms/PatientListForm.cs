using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Form for listing, searching, and managing patients.
/// </summary>
public partial class PatientListForm : Form
{
    private readonly PatientService _patientService = new();

    public PatientListForm()
    {
        InitializeComponent();
        LoadPatients();
    }

    private void LoadPatients(string? search = null)
    {
        var patients = _patientService.GetAll(search);
        dgvPatients.DataSource = patients;
        FormatGrid();
        lblCount.Text = $"Всего: {patients.Count}";
    }

    private void FormatGrid()
    {
        if (dgvPatients.Columns.Count == 0) return;

        var hide = new[] { "MiddleName", "Email", "Address", "InsurancePolicy", "Allergies", "MedicalNotes", "CreatedAt", "UpdatedAt", "FullName" };
        foreach (var col in hide)
            if (dgvPatients.Columns.Contains(col))
                dgvPatients.Columns[col]!.Visible = false;

        if (dgvPatients.Columns.Contains("Id"))
        { dgvPatients.Columns["Id"]!.HeaderText = "ID"; dgvPatients.Columns["Id"]!.Width = 50; }
        if (dgvPatients.Columns.Contains("LastName"))
            dgvPatients.Columns["LastName"]!.HeaderText = "Фамилия";
        if (dgvPatients.Columns.Contains("FirstName"))
            dgvPatients.Columns["FirstName"]!.HeaderText = "Имя";
        if (dgvPatients.Columns.Contains("DateOfBirth"))
            dgvPatients.Columns["DateOfBirth"]!.HeaderText = "Дата рождения";
        if (dgvPatients.Columns.Contains("Gender"))
            dgvPatients.Columns["Gender"]!.HeaderText = "Пол";
        if (dgvPatients.Columns.Contains("Phone"))
            dgvPatients.Columns["Phone"]!.HeaderText = "Телефон";
    }

    private void btnSearch_Click(object? sender, EventArgs e)
    {
        LoadPatients(txtSearch.Text.Trim());
    }

    private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            LoadPatients(txtSearch.Text.Trim());
    }

    private void btnAdd_Click(object? sender, EventArgs e)
    {
        using var form = new PatientEditForm();
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadPatients();
    }

    private void btnEdit_Click(object? sender, EventArgs e)
    {
        var patient = GetSelectedPatient();
        if (patient == null) return;

        using var form = new PatientEditForm(patient);
        if (form.ShowDialog(this) == DialogResult.OK)
            LoadPatients();
    }

    private void btnDelete_Click(object? sender, EventArgs e)
    {
        var patient = GetSelectedPatient();
        if (patient == null) return;

        var result = MessageBox.Show(
            $"Удалить пациента {patient.FullName}? Все связанные данные будут удалены.",
            "Подтверждение удаления",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            _patientService.Delete(patient.Id);
            LoadPatients();
        }
    }

    private void btnToothChart_Click(object? sender, EventArgs e)
    {
        var patient = GetSelectedPatient();
        if (patient == null) return;

        using var form = new ToothChartForm(patient);
        form.ShowDialog(this);
    }

    private void dgvPatients_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            btnEdit_Click(sender, e);
    }

    private Patient? GetSelectedPatient()
    {
        if (dgvPatients.SelectedRows.Count == 0)
        {
            MessageBox.Show("Выберите пациента.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return null;
        }
        var row = dgvPatients.SelectedRows[0];
        var id = (int)row.Cells["Id"].Value;
        return _patientService.GetById(id);
    }
}
