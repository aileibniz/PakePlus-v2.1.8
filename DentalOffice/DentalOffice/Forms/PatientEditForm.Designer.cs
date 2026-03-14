namespace DentalOffice.Forms;

partial class PatientEditForm
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
        txtLastName = new TextBox();
        txtFirstName = new TextBox();
        txtMiddleName = new TextBox();
        dtpBirthDate = new DateTimePicker();
        cmbGender = new ComboBox();
        txtPhone = new TextBox();
        txtEmail = new TextBox();
        txtAddress = new TextBox();
        txtInsurance = new TextBox();
        txtAllergies = new TextBox();
        txtMedicalNotes = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();

        SuspendLayout();

        int y = 20;
        int labelX = 20;
        int inputX = 180;
        int inputWidth = 300;
        int rowHeight = 35;

        AddFormRow("Фамилия *:", txtLastName, ref y, labelX, inputX, inputWidth, rowHeight);
        AddFormRow("Имя *:", txtFirstName, ref y, labelX, inputX, inputWidth, rowHeight);
        AddFormRow("Отчество:", txtMiddleName, ref y, labelX, inputX, inputWidth, rowHeight);

        // Date of birth
        var lblBirth = new Label { Text = "Дата рождения *:", Location = new Point(labelX, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        dtpBirthDate.Location = new Point(inputX, y);
        dtpBirthDate.Size = new Size(inputWidth, 25);
        dtpBirthDate.Format = DateTimePickerFormat.Short;
        Controls.Add(lblBirth);
        Controls.Add(dtpBirthDate);
        y += rowHeight;

        // Gender
        var lblGender = new Label { Text = "Пол *:", Location = new Point(labelX, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        cmbGender.Location = new Point(inputX, y);
        cmbGender.Size = new Size(inputWidth, 25);
        cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(lblGender);
        Controls.Add(cmbGender);
        y += rowHeight;

        AddFormRow("Телефон *:", txtPhone, ref y, labelX, inputX, inputWidth, rowHeight);
        AddFormRow("Email:", txtEmail, ref y, labelX, inputX, inputWidth, rowHeight);
        AddFormRow("Адрес:", txtAddress, ref y, labelX, inputX, inputWidth, rowHeight);
        AddFormRow("Страховой полис:", txtInsurance, ref y, labelX, inputX, inputWidth, rowHeight);

        // Allergies (multiline)
        var lblAllergies = new Label { Text = "Аллергии:", Location = new Point(labelX, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        txtAllergies.Location = new Point(inputX, y);
        txtAllergies.Size = new Size(inputWidth, 50);
        txtAllergies.Multiline = true;
        txtAllergies.Font = new Font("Segoe UI", 10F);
        Controls.Add(lblAllergies);
        Controls.Add(txtAllergies);
        y += 60;

        // Medical notes (multiline)
        var lblNotes = new Label { Text = "Мед. заметки:", Location = new Point(labelX, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        txtMedicalNotes.Location = new Point(inputX, y);
        txtMedicalNotes.Size = new Size(inputWidth, 60);
        txtMedicalNotes.Multiline = true;
        txtMedicalNotes.Font = new Font("Segoe UI", 10F);
        Controls.Add(lblNotes);
        Controls.Add(txtMedicalNotes);
        y += 75;

        // Buttons
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

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(520, y + 55);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        ResumeLayout(false);
        PerformLayout();
    }

    private void AddFormRow(string labelText, TextBox textBox, ref int y, int labelX, int inputX, int inputWidth, int rowHeight)
    {
        var label = new Label
        {
            Text = labelText,
            Location = new Point(labelX, y + 3),
            AutoSize = true,
            Font = new Font("Segoe UI", 10F)
        };
        textBox.Location = new Point(inputX, y);
        textBox.Size = new Size(inputWidth, 25);
        textBox.Font = new Font("Segoe UI", 10F);
        Controls.Add(label);
        Controls.Add(textBox);
        y += rowHeight;
    }

    private TextBox txtLastName;
    private TextBox txtFirstName;
    private TextBox txtMiddleName;
    private DateTimePicker dtpBirthDate;
    private ComboBox cmbGender;
    private TextBox txtPhone;
    private TextBox txtEmail;
    private TextBox txtAddress;
    private TextBox txtInsurance;
    private TextBox txtAllergies;
    private TextBox txtMedicalNotes;
    private Button btnSave;
    private Button btnCancel;
}
