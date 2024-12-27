using System.Collections.Concurrent;

namespace HitCounter
{
    public interface ICircuitUserService
    {
        ConcurrentDictionary<string, CircuitUser> Circuits { get; }

        event EventHandler CircuitsChanged;
        event UserRemovedEventHandler UserRemoved;

        void Connect(string CircuitId, string UserId);

        void Disconnect(string CircuitId);
    }
}
