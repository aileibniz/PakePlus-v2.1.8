namespace DentalOffice.Forms;

partial class AppointmentEditForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        cmbPatient = new ComboBox();
        dtpDate = new DateTimePicker();
        cmbStartTime = new ComboBox();
        cmbEndTime = new ComboBox();
        txtDoctor = new TextBox();
        cmbStatus = new ComboBox();
        cmbTreatmentType = new ComboBox();
        txtNotes = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();

        SuspendLayout();

        int y = 20, labelX = 20, inputX = 160, inputW = 280, rh = 35;

        AddLabel("Пациент *:", labelX, y);
        cmbPatient.Location = new Point(inputX, y);
        cmbPatient.Size = new Size(inputW, 25);
        cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(cmbPatient);
        y += rh;

        AddLabel("Дата *:", labelX, y);
        dtpDate.Location = new Point(inputX, y);
        dtpDate.Size = new Size(inputW, 25);
        dtpDate.Format = DateTimePickerFormat.Short;
        Controls.Add(dtpDate);
        y += rh;

        AddLabel("Начало *:", labelX, y);
        cmbStartTime.Location = new Point(inputX, y);
        cmbStartTime.Size = new Size(120, 25);
        cmbStartTime.DropDownStyle = ComboBoxStyle.DropDown;
        Controls.Add(cmbStartTime);
        y += rh;

        AddLabel("Окончание *:", labelX, y);
        cmbEndTime.Location = new Point(inputX, y);
        cmbEndTime.Size = new Size(120, 25);
        cmbEndTime.DropDownStyle = ComboBoxStyle.DropDown;
        Controls.Add(cmbEndTime);
        y += rh;

        AddLabel("Врач *:", labelX, y);
        txtDoctor.Location = new Point(inputX, y);
        txtDoctor.Size = new Size(inputW, 25);
        txtDoctor.Font = new Font("Segoe UI", 10F);
        Controls.Add(txtDoctor);
        y += rh;

        AddLabel("Статус:", labelX, y);
        cmbStatus.Location = new Point(inputX, y);
        cmbStatus.Size = new Size(inputW, 25);
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(cmbStatus);
        y += rh;

        AddLabel("Тип лечения:", labelX, y);
        cmbTreatmentType.Location = new Point(inputX, y);
        cmbTreatmentType.Size = new Size(inputW, 25);
        cmbTreatmentType.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(cmbTreatmentType);
        y += rh;

        AddLabel("Заметки:", labelX, y);
        txtNotes.Location = new Point(inputX, y);
        txtNotes.Size = new Size(inputW, 60);
        txtNotes.Multiline = true;
        txtNotes.Font = new Font("Segoe UI", 10F);
        Controls.Add(txtNotes);
        y += 75;

        btnSave.Location = new Point(inputX, y);
        btnSave.Size = new Size(120, 35);
        btnSave.Text = "Сохранить";
        btnSave.BackColor = Color.FromArgb(40, 167, 69);
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.Cursor = Cursors.Hand;
        btnSave.Click += btnSave_Click;

        btnCancel.Location = new Point(inputX + 140, y);
        btnCancel.Size = new Size(120, 35);
        btnCancel.Text = "Отмена";
        btnCancel.BackColor = Color.FromArgb(108, 117, 125);
        btnCancel.ForeColor = Color.White;
        btnCancel.FlatStyle = FlatStyle.Flat;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.Font = new Font("Segoe UI", 10F);
        btnCancel.Cursor = Cursors.Hand;
        btnCancel.Click += btnCancel_Click;

        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(480, y + 55);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        ResumeLayout(false);
        PerformLayout();
    }

    private void AddLabel(string text, int x, int y)
    {
        var lbl = new Label
        {
            Text = text,
            Location = new Point(x, y + 3),
            AutoSize = true,
            Font = new Font("Segoe UI", 10F)
        };
        Controls.Add(lbl);
    }

    private ComboBox cmbPatient;
    private DateTimePicker dtpDate;
    private ComboBox cmbStartTime;
    private ComboBox cmbEndTime;
    private TextBox txtDoctor;
    private ComboBox cmbStatus;
    private ComboBox cmbTreatmentType;
    private TextBox txtNotes;
    private Button btnSave;
    private Button btnCancel;
}
