using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Components;

namespace HitCounter.Components.Pages
{
    public partial class Home : ComponentBase, IDisposable
    {
        public string MyCircuitMessage = "";
        public string MyRemovedMessage = "";
        public string MyUserId = ""; // This is a placeholder for an actual real identifier in real implementation.

        CircuitHandlingService handler;

        protected override void OnInitialized()
        {
            handler = (CircuitHandlingService)MyCircuitHandler;
            string timeSpanInSeconds = "";

            if (handler.CircuitId == null) // hack to "workaround" the fact that CircuitId never gets set from CircuitHandler.OnCircuitOpenedAsync()
                                           // in fact, OnCircuitOpenedAsync is never called?!
            {
                timeSpanInSeconds = ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
            }

            MyCircuitMessage = $"My Circuit ID = {handler.CircuitId}";

            if (UserService.Circuits.Count == 0)
            {
                MyUserId = "Jonny1";
            }
            else if (UserService.Circuits.Count == 1)
            {
                MyUserId = "Christine2";
            }
            else if (UserService.Circuits.Count == 2)
            {
                MyUserId = "Sammy3";
            }

            if (handler.CircuitId == null)
            {
                UserService.Connect(timeSpanInSeconds, MyUserId);
            }
            else
            {
                UserService.Connect(handler.CircuitId, MyUserId);
            }

            UserService.CircuitsChanged += UserService_CircuitesChanged;
            UserService.UserRemoved += UserService_UserRemoved;

        }

        private void UserService_UserRemoved(object sender, UserRemovedEventArgs e)
        {
            MyRemovedMessage = $"{e.UserId} was disconnected";
            InvokeAsync(() => StateHasChanged());
        }

        private void UserService_CircuitesChanged(object? sender, EventArgs e)
        {
            InvokeAsync(() => StateHasChanged());
        }

        public void Dispose()
        {
            UserService.CircuitsChanged -= UserService_CircuitesChanged;
            UserService.UserRemoved -= UserService_UserRemoved;
        }
    }
}
