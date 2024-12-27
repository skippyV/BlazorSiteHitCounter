using Microsoft.AspNetCore.Components.Server.Circuits;

/*
 * User Tracking in Blazor Server: Carl Franklin's Blazor Train Ep 72 
 * https://www.youtube.com/watch?v=YWYUIjWKh7w
 */

namespace HitCounter
{
    public class CircuitHandlingService : CircuitHandler
    {
        public string CircuitId { get; private set; }

        ICircuitUserService _userService;

        public CircuitHandlingService(ICircuitUserService iUserService)
        {
            _userService = iUserService;
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            CircuitId = circuit.Id;
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _userService.Disconnect(circuit.Id);
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }
    }

}
