using ITBS_Platform;
using ITBS_Platform.Data;
using ITBS_Platform.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ITBS_Platform
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            authService authService = new authService();
            // ✅ Lancer directement le LoginForm sans base de données
            Application.Run(new LoginForm(authService));
        }
    }
}
