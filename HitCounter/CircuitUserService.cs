using System.Collections.Concurrent;

namespace HitCounter
{
    public class CircuitUserService : ICircuitUserService
    {
        public ConcurrentDictionary<string, CircuitUser> Circuits { get; private set; }
        public event EventHandler CircuitsChanged;
        public event UserRemovedEventHandler UserRemoved;

        void OnCircuitsChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);

        void OnUserRemoved(string UserId)
        {
            var args = new UserRemovedEventArgs();
            args.UserId = UserId;
            UserRemoved?.Invoke(this, args);
        }
        public CircuitUserService()
        {
            Circuits = new ConcurrentDictionary<string, CircuitUser>();
        }
        public void Connect(string CircuitId, string UserId)
        {
            if (Circuits.ContainsKey(CircuitId))
            {
                Circuits[CircuitId].UserId = UserId;
            }
            else
            {
                var circuitUser = new CircuitUser();
                circuitUser.UserId = UserId;
                circuitUser.CircuitId = CircuitId;
                Circuits[CircuitId] = circuitUser;
            }
            OnCircuitsChanged();
        }

        public void Disconnect(string CircuitId)
        {
            CircuitUser circuitRemoved;
            Circuits.TryRemove(CircuitId, out circuitRemoved);
            if (circuitRemoved != null)
            {
                OnUserRemoved(circuitRemoved.UserId);
                OnCircuitsChanged();
            }
        }
    }
}
