using MudBlazor;

namespace WebApp.Utilities
{
    public class SnackbarHelper
    {
        public SnackbarHelper(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void Show(string message, Severity severity, string position = Defaults.Classes.Position.BottomCenter)
        {
            _snackbar.Clear();
            _snackbar.Configuration.PositionClass = position;
            _snackbar.Add(message, severity);
        }


        private readonly ISnackbar _snackbar;
    }
}
