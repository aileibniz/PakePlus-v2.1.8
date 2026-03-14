namespace DentalOffice.Forms;

partial class InvoiceEditForm
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
        lstTreatments = new ListView();
        numTotal = new NumericUpDown();
        txtNotes = new TextBox();
        btnSave = new Button();
        btnCancel = new Button();

        ((System.ComponentModel.ISupportInitialize)numTotal).BeginInit();
        SuspendLayout();

        int y = 20, lx = 20, ix = 160, iw = 400;

        var lblPatient = new Label { Text = "Пациент *:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        cmbPatient.Location = new Point(ix, y);
        cmbPatient.Size = new Size(iw, 25);
        cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
        Controls.Add(lblPatient);
        Controls.Add(cmbPatient);
        y += 35;

        var lblDate = new Label { Text = "Дата *:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        dtpDate.Location = new Point(ix, y);
        dtpDate.Size = new Size(200, 25);
        dtpDate.Format = DateTimePickerFormat.Short;
        Controls.Add(lblDate);
        Controls.Add(dtpDate);
        y += 35;

        var lblTreatments = new Label { Text = "Лечение:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        lstTreatments.Location = new Point(ix, y);
        lstTreatments.Size = new Size(iw, 150);
        lstTreatments.View = View.Details;
        lstTreatments.CheckBoxes = true;
        lstTreatments.FullRowSelect = true;
        lstTreatments.GridLines = true;
        lstTreatments.Font = new Font("Segoe UI", 9F);
        lstTreatments.Columns.Add("ID", 40);
        lstTreatments.Columns.Add("Дата", 80);
        lstTreatments.Columns.Add("Тип", 100);
        lstTreatments.Columns.Add("Описание", 120);
        lstTreatments.Columns.Add("Стоимость", 70);
        lstTreatments.ItemChecked += lstTreatments_ItemChecked;
        Controls.Add(lblTreatments);
        Controls.Add(lstTreatments);
        y += 160;

        var lblTotal = new Label { Text = "Итого *:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        numTotal.Location = new Point(ix, y);
        numTotal.Size = new Size(200, 25);
        numTotal.Maximum = 9999999;
        numTotal.DecimalPlaces = 2;
        numTotal.Font = new Font("Segoe UI", 10F);
        Controls.Add(lblTotal);
        Controls.Add(numTotal);
        y += 35;

        var lblNotes = new Label { Text = "Заметки:", Location = new Point(lx, y + 3), AutoSize = true, Font = new Font("Segoe UI", 10F) };
        txtNotes.Location = new Point(ix, y);
        txtNotes.Size = new Size(iw, 50);
        txtNotes.Multiline = true;
        txtNotes.Font = new Font("Segoe UI", 10F);
        Controls.Add(lblNotes);
        Controls.Add(txtNotes);
        y += 60;

        btnSave.Location = new Point(ix, y);
        btnSave.Size = new Size(120, 35);
        btnSave.Text = "Создать счет";
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
        ClientSize = new Size(600, y + 55);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.FromArgb(248, 249, 250);

        ((System.ComponentModel.ISupportInitialize)numTotal).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private ComboBox cmbPatient;
    private DateTimePicker dtpDate;
    private ListView lstTreatments;
    private NumericUpDown numTotal;
    private TextBox txtNotes;
    private Button btnSave;
    private Button btnCancel;
}
