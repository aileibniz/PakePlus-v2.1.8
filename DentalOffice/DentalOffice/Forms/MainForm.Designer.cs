namespace DentalOffice.Forms;

partial class MainForm
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
        // Navigation panel
        panelNav = new Panel();
        lblAppTitle = new Label();
        btnPatients = new Button();
        btnAppointments = new Button();
        btnTreatments = new Button();
        btnInvoices = new Button();
        btnRefresh = new Button();

        // Dashboard cards
        panelDashboard = new Panel();
        lblDashboardTitle = new Label();

        panelCard1 = new Panel();
        lblCard1Title = new Label();
        lblTotalPatients = new Label();

        panelCard2 = new Panel();
        lblCard2Title = new Label();
        lblTodayAppointments = new Label();

        panelCard3 = new Panel();
        lblCard3Title = new Label();
        lblMonthRevenue = new Label();

        panelCard4 = new Panel();
        lblCard4Title = new Label();
        lblUnpaidInvoices = new Label();

        // Today's appointments grid
        lblTodayTitle = new Label();
        dgvTodayAppointments = new DataGridView();

        panelNav.SuspendLayout();
        panelDashboard.SuspendLayout();
        panelCard1.SuspendLayout();
        panelCard2.SuspendLayout();
        panelCard3.SuspendLayout();
        panelCard4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvTodayAppointments).BeginInit();
        SuspendLayout();

        // === Navigation Panel ===
        panelNav.BackColor = Color.FromArgb(33, 37, 41);
        panelNav.Dock = DockStyle.Left;
        panelNav.Width = 220;
        panelNav.Padding = new Padding(10);
        panelNav.Controls.Add(btnRefresh);
        panelNav.Controls.Add(btnInvoices);
        panelNav.Controls.Add(btnTreatments);
        panelNav.Controls.Add(btnAppointments);
        panelNav.Controls.Add(btnPatients);
        panelNav.Controls.Add(lblAppTitle);

        // App title
        lblAppTitle.Text = "Стоматология";
        lblAppTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblAppTitle.ForeColor = Color.White;
        lblAppTitle.Dock = DockStyle.Top;
        lblAppTitle.Height = 60;
        lblAppTitle.TextAlign = ContentAlignment.MiddleCenter;
        lblAppTitle.Padding = new Padding(0, 10, 0, 10);

        // Nav buttons style
        var navButtonStyle = new Action<Button, string, int>((btn, text, top) =>
        {
            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 58, 64);
            btn.BackColor = Color.FromArgb(33, 37, 41);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 11F);
            btn.Dock = DockStyle.Top;
            btn.Height = 50;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.Cursor = Cursors.Hand;
        });

        navButtonStyle(btnPatients, "Пациенты", 0);
        navButtonStyle(btnAppointments, "Расписание", 0);
        navButtonStyle(btnTreatments, "Лечение", 0);
        navButtonStyle(btnInvoices, "Счета", 0);
        navButtonStyle(btnRefresh, "Обновить", 0);

        btnPatients.Click += btnPatients_Click;
        btnAppointments.Click += btnAppointments_Click;
        btnTreatments.Click += btnTreatments_Click;
        btnInvoices.Click += btnInvoices_Click;
        btnRefresh.Click += btnRefresh_Click;

        // === Dashboard Panel ===
        panelDashboard.Location = new Point(240, 10);
        panelDashboard.Size = new Size(820, 140);
        panelDashboard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        // Dashboard title
        lblDashboardTitle.Text = "Панель управления";
        lblDashboardTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblDashboardTitle.Location = new Point(240, 15);
        lblDashboardTitle.AutoSize = true;

        // Card 1 - Total patients
        SetupCard(panelCard1, lblCard1Title, lblTotalPatients,
            "Всего пациентов", "0", Color.FromArgb(0, 123, 255), 0);

        // Card 2 - Today appointments
        SetupCard(panelCard2, lblCard2Title, lblTodayAppointments,
            "Приемов сегодня", "0", Color.FromArgb(40, 167, 69), 200);

        // Card 3 - Month revenue
        SetupCard(panelCard3, lblCard3Title, lblMonthRevenue,
            "Доход за месяц", "0 руб.", Color.FromArgb(255, 193, 7), 400);

        // Card 4 - Unpaid
        SetupCard(panelCard4, lblCard4Title, lblUnpaidInvoices,
            "Неоплаченные счета", "0 руб.", Color.FromArgb(220, 53, 69), 600);

        panelDashboard.Controls.AddRange(new Control[] { panelCard1, panelCard2, panelCard3, panelCard4 });

        // === Today's appointments ===
        lblTodayTitle.Text = "Приемы на сегодня";
        lblTodayTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTodayTitle.Location = new Point(240, 170);
        lblTodayTitle.AutoSize = true;

        dgvTodayAppointments.Location = new Point(240, 200);
        dgvTodayAppointments.Size = new Size(820, 380);
        dgvTodayAppointments.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvTodayAppointments.ReadOnly = true;
        dgvTodayAppointments.AllowUserToAddRows = false;
        dgvTodayAppointments.AllowUserToDeleteRows = false;
        dgvTodayAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTodayAppointments.BackgroundColor = Color.White;
        dgvTodayAppointments.BorderStyle = BorderStyle.None;
        dgvTodayAppointments.RowHeadersVisible = false;
        dgvTodayAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTodayAppointments.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
        dgvTodayAppointments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dgvTodayAppointments.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
        dgvTodayAppointments.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvTodayAppointments.EnableHeadersVisualStyles = false;
        dgvTodayAppointments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

        // === Form ===
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 600);
        MinimumSize = new Size(900, 500);
        Text = "DentalOffice - Управление стоматологическим кабинетом";
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(248, 249, 250);

        Controls.Add(dgvTodayAppointments);
        Controls.Add(lblTodayTitle);
        Controls.Add(panelDashboard);
        Controls.Add(lblDashboardTitle);
        Controls.Add(panelNav);

        panelNav.ResumeLayout(false);
        panelDashboard.ResumeLayout(false);
        panelCard1.ResumeLayout(false);
        panelCard2.ResumeLayout(false);
        panelCard3.ResumeLayout(false);
        panelCard4.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvTodayAppointments).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private void SetupCard(Panel panel, Label titleLabel, Label valueLabel,
        string title, string value, Color color, int xOffset)
    {
        panel.Location = new Point(xOffset, 0);
        panel.Size = new Size(190, 130);
        panel.BackColor = color;
        panel.Padding = new Padding(15);

        titleLabel.Text = title;
        titleLabel.ForeColor = Color.White;
        titleLabel.Font = new Font("Segoe UI", 9F);
        titleLabel.Location = new Point(15, 15);
        titleLabel.AutoSize = true;

        valueLabel.Text = value;
        valueLabel.ForeColor = Color.White;
        valueLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        valueLabel.Location = new Point(15, 55);
        valueLabel.AutoSize = true;

        panel.Controls.Add(valueLabel);
        panel.Controls.Add(titleLabel);
    }

    private Panel panelNav;
    private Label lblAppTitle;
    private Button btnPatients;
    private Button btnAppointments;
    private Button btnTreatments;
    private Button btnInvoices;
    private Button btnRefresh;

    private Panel panelDashboard;
    private Label lblDashboardTitle;

    private Panel panelCard1;
    private Label lblCard1Title;
    private Label lblTotalPatients;

    private Panel panelCard2;
    private Label lblCard2Title;
    private Label lblTodayAppointments;

    private Panel panelCard3;
    private Label lblCard3Title;
    private Label lblMonthRevenue;

    private Panel panelCard4;
    private Label lblCard4Title;
    private Label lblUnpaidInvoices;

    private Label lblTodayTitle;
    private DataGridView dgvTodayAppointments;
}
