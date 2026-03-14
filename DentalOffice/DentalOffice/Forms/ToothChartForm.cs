using DentalOffice.Models;
using DentalOffice.Services;

namespace DentalOffice.Forms;

/// <summary>
/// Visual tooth chart form showing all 32 teeth with color-coded status.
/// Uses FDI dental notation.
/// </summary>
public partial class ToothChartForm : Form
{
    private readonly ToothRecordService _toothService = new();
    private readonly Patient _patient;
    private Dictionary<string, ToothRecord> _records = new();

    public ToothChartForm(Patient patient)
    {
        _patient = patient;
        InitializeComponent();
        Text = $"Зубная карта - {patient.FullName}";
        _toothService.InitializeForPatient(patient.Id);
        LoadRecords();
        CreateToothButtons();
    }

    private void LoadRecords()
    {
        var records = _toothService.GetByPatient(_patient.Id);
        _records = records.ToDictionary(r => r.ToothNumber);
    }

    private void CreateToothButtons()
    {
        panelTeeth.Controls.Clear();

        int startX = 20;
        int toothSize = 45;
        int gap = 5;
        int upperY = 60;
        int lowerY = 180;

        // Labels
        var lblUpper = new Label { Text = "Верхняя челюсть", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(startX, upperY - 30), AutoSize = true };
        var lblLower = new Label { Text = "Нижняя челюсть", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Location = new Point(startX, lowerY - 30), AutoSize = true };
        panelTeeth.Controls.Add(lblUpper);
        panelTeeth.Controls.Add(lblLower);

        // Separator labels
        var lblRight = new Label { Text = "Правая", Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, Location = new Point(startX, upperY - 15), AutoSize = true };
        var lblLeft = new Label { Text = "Левая", Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, Location = new Point(startX + 8 * (toothSize + gap) + 20, upperY - 15), AutoSize = true };
        panelTeeth.Controls.Add(lblRight);
        panelTeeth.Controls.Add(lblLeft);

        // Upper right: 18-11
        for (int i = 0; i < ToothChart.UpperRight.Length; i++)
        {
            var tooth = ToothChart.UpperRight[i];
            CreateToothButton(tooth, startX + i * (toothSize + gap), upperY, toothSize);
        }

        // Upper left: 21-28
        int leftStartX = startX + 8 * (toothSize + gap) + 20;
        for (int i = 0; i < ToothChart.UpperLeft.Length; i++)
        {
            var tooth = ToothChart.UpperLeft[i];
            CreateToothButton(tooth, leftStartX + i * (toothSize + gap), upperY, toothSize);
        }

        // Lower left: 38-31 (reversed display: 31 closest to center)
        for (int i = 0; i < ToothChart.LowerLeft.Length; i++)
        {
            var tooth = ToothChart.LowerLeft[i];
            CreateToothButton(tooth, startX + i * (toothSize + gap), lowerY, toothSize);
        }

        // Lower right: 41-48
        for (int i = 0; i < ToothChart.LowerRight.Length; i++)
        {
            var tooth = ToothChart.LowerRight[i];
            CreateToothButton(tooth, leftStartX + i * (toothSize + gap), lowerY, toothSize);
        }
    }

    private void CreateToothButton(string toothNumber, int x, int y, int size)
    {
        var btn = new Button
        {
            Text = toothNumber,
            Location = new Point(x, y),
            Size = new Size(size, size),
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            Tag = toothNumber,
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderSize = 1;

        // Color based on status
        if (_records.TryGetValue(toothNumber, out var record))
        {
            btn.BackColor = GetStatusColor(record.Status);
            btn.ForeColor = record.Status == ToothStatus.Missing ? Color.White : Color.Black;
        }
        else
        {
            btn.BackColor = Color.White;
        }

        btn.Click += ToothButton_Click;
        panelTeeth.Controls.Add(btn);
    }

    private static Color GetStatusColor(string status)
    {
        return status switch
        {
            ToothStatus.Healthy => Color.FromArgb(200, 255, 200),
            ToothStatus.Caries => Color.FromArgb(255, 200, 200),
            ToothStatus.Filled => Color.FromArgb(200, 200, 255),
            ToothStatus.Crown => Color.FromArgb(255, 255, 150),
            ToothStatus.Missing => Color.FromArgb(128, 128, 128),
            ToothStatus.Implant => Color.FromArgb(200, 255, 255),
            ToothStatus.RootCanal => Color.FromArgb(255, 200, 255),
            ToothStatus.NeedsTreatment => Color.FromArgb(255, 165, 0),
            _ => Color.White
        };
    }

    private void ToothButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button btn || btn.Tag is not string toothNumber) return;

        _records.TryGetValue(toothNumber, out var record);
        record ??= new ToothRecord { PatientId = _patient.Id, ToothNumber = toothNumber };

        using var form = new ToothEditDialog(record);
        if (form.ShowDialog(this) == DialogResult.OK)
        {
            _toothService.SaveOrUpdate(record);
            LoadRecords();
            CreateToothButtons();
        }
    }
}

/// <summary>
/// Simple dialog for editing a single tooth's status.
/// </summary>
public class ToothEditDialog : Form
{
    private readonly ToothRecord _record;
    private readonly ComboBox _cmbStatus;
    private readonly TextBox _txtNotes;

    public ToothEditDialog(ToothRecord record)
    {
        _record = record;

        Text = $"Зуб {record.ToothNumber}";
        Size = new Size(350, 250);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        var lblStatus = new Label { Text = "Состояние:", Location = new Point(20, 20), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        _cmbStatus = new ComboBox { Location = new Point(20, 45), Size = new Size(290, 25), DropDownStyle = ComboBoxStyle.DropDownList };
        _cmbStatus.Items.AddRange(ToothStatus.All);
        _cmbStatus.SelectedItem = record.Status;

        var lblNotes = new Label { Text = "Заметки:", Location = new Point(20, 80), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        _txtNotes = new TextBox { Location = new Point(20, 105), Size = new Size(290, 50), Multiline = true, Text = record.Notes ?? "", Font = new Font("Segoe UI", 10F) };

        var btnSave = new Button
        {
            Text = "Сохранить", Location = new Point(20, 170), Size = new Size(120, 35),
            BackColor = Color.FromArgb(40, 167, 69), ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F, FontStyle.Bold), Cursor = Cursors.Hand
        };
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Click += (s, e) =>
        {
            _record.Status = _cmbStatus.SelectedItem?.ToString() ?? ToothStatus.Healthy;
            _record.Notes = string.IsNullOrWhiteSpace(_txtNotes.Text) ? null : _txtNotes.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        };

        var btnCancel = new Button
        {
            Text = "Отмена", Location = new Point(160, 170), Size = new Size(120, 35),
            BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10F), Cursor = Cursors.Hand
        };
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        Controls.AddRange(new Control[] { lblStatus, _cmbStatus, lblNotes, _txtNotes, btnSave, btnCancel });
    }
}
