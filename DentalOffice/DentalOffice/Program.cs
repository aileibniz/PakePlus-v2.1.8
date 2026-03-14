using DentalOffice.Data;
using DentalOffice.Forms;

namespace DentalOffice;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Initialize database on first run
        DatabaseContext.Initialize();

        Application.Run(new MainForm());
    }
}
