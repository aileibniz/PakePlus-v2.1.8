namespace DentalOffice.Forms;

partial class TreatmentEditForm
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
        cmbType = new ComboBox();
        cmbTooth = new ComboBox();
        txtDescription = new TextBox();
        txtDoctor = new TextBox();
        numCost = new NumericUpDown();
        txtNotes = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();

        ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
        SuspendLayout();

        int y = 20, lx = 20, ix = 160, iw = 280, rh = 35;

        AddLabel("Пациент *:", lx, y);
        cmbPatient.Location = new Point(ix, y); cmbPatient.Size = new Size(iw, 25);
        cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(cmbPatient); y += rh;

        AddLabel("Дата *:", lx, y);
        dtpDate.Location = new Point(ix, y); dtpDate.Size = new Size(iw, 25);
        dtpDate.Format = DateTimePickerFormat.Short;
        Controls.Add(dtpDate); y += rh;

        AddLabel("Тип лечения *:", lx, y);
        cmbType.Location = new Point(ix, y); cmbType.Size = new Size(iw, 25);
        cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(cmbType); y += rh;

        AddLabel("Номер зуба:", lx, y);
        cmbTooth.Location = new Point(ix, y); cmbTooth.Size = new Size(100, 25);
        cmbTooth.DropDownStyle = ComboBoxStyle.DropDown;
        Controls.Add(cmbTooth); y += rh;

        AddLabel("Описание *:", lx, y);
        txtDescription.Location = new Point(ix, y); txtDescription.Size = new Size(iw, 60);
        txtDescription.Multiline = true; txtDescription.Font = new Font("Segoe UI", 10F);
        Controls.Add(txtDescription); y += 70;

        AddLabel("Врач *:", lx, y);
        txtDoctor.Location = new Point(ix, y); txtDoctor.Size = new Size(iw, 25);
        txtDoctor.Font = new Font("Segoe UI", 10F);
        Controls.Add(txtDoctor); y += rh;

        AddLabel("Стоимость:", lx, y);
        numCost.Location = new Point(ix, y); numCost.Size = new Size(150, 25);
        numCost.Maximum = 999999; numCost.DecimalPlaces = 2;
        numCost.Font = new Font("Segoe UI", 10F);
        Controls.Add(numCost); y += rh;

        AddLabel("Заметки:", lx, y);
        txtNotes.Location = new Point(ix, y); txtNotes.Size = new Size(iw, 50);
        txtNotes.Multiline = true; txtNotes.Font = new Font("Segoe UI", 10F);
        Controls.Add(txtNotes); y += 60;

        btnSave.Location = new Point(ix, y);
        btnSave.Size = new Size(120, 35);
        btnSave.Text = "Сохранить";
        btnSave.BackColor = Color.FromArgb(40, 167, 69);
        btnSave.ForeColor = Color.White;
        btnSave.FlatStyle = FlatStyle.Flat;
        btnSave.FlatAppearance.BorderSize = 0;
        btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnSave.Cursor = Cursors.Hand;
        btnSave.Click += btnSave_Click;

        btnCancel.Location = new Point(ix + 140, y);
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
        MaximizeBox = false; MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private void AddLabel(string text, int x, int y)
    {
        Controls.Add(new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) });
    }

    private ComboBox cmbPatient;
    private DateTimePicker dtpDate;
    private ComboBox cmbType;
    private ComboBox cmbTooth;
    private TextBox txtDescription;
    private TextBox txtDoctor;
    private NumericUpDown numCost;
    private TextBox txtNotes;
    private Button btnSave;
    private Button btnCancel;
}
