namespace DentalOffice.Forms;

partial class ToothChartForm
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
        panelTeeth = new Panel();
        panelLegend = new Panel();

        SuspendLayout();

        // panelTeeth
        panelTeeth.Dock = DockStyle.Fill;
        panelTeeth.AutoScroll = true;
        panelTeeth.BackColor = Color.White;

        // panelLegend - color legend at bottom
        panelLegend.Dock = DockStyle.Bottom;
        panelLegend.Height = 50;
        panelLegend.BackColor = Color.FromArgb(248, 249, 250);
        panelLegend.Padding = new Padding(10);

        int lx = 10;
        var legendItems = new (string label, Color color)[]
        {
            ("Здоров", Color.FromArgb(200, 255, 200)),
            ("Кариес", Color.FromArgb(255, 200, 200)),
            ("Пломба", Color.FromArgb(200, 200, 255)),
            ("Коронка", Color.FromArgb(255, 255, 150)),
            ("Отсутствует", Color.FromArgb(128, 128, 128)),
            ("Имплант", Color.FromArgb(200, 255, 255)),
            ("Депульпирован", Color.FromArgb(255, 200, 255)),
            ("Требует лечения", Color.FromArgb(255, 165, 0)),
        };

        foreach (var (label, color) in legendItems)
        {
            var colorBox = new Panel
            {
                Location = new Point(lx, 15),
                Size = new Size(16, 16),
                BackColor = color,
                BorderStyle = BorderStyle.FixedSingle
            };
            var lbl = new Label
            {
                Text = label,
                Location = new Point(lx + 20, 15),
                AutoSize = true,
                Font = new Font("Segoe UI", 8F)
            };
            panelLegend.Controls.Add(colorBox);
            panelLegend.Controls.Add(lbl);
            lx += lbl.PreferredWidth + 35;
        }

        // Form
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(870, 350);
        MinimumSize = new Size(870, 350);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = Color.White;

        Controls.Add(panelTeeth);
        Controls.Add(panelLegend);

        ResumeLayout(false);
    }

    private Panel panelTeeth;
    private Panel panelLegend;
}
