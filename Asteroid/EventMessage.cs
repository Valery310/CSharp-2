using System.Windows.Forms;

namespace Asteroid
{
    public class EventMessage
    {
        public string Message { get; private set; }

        public EventMessage(string message) => Message = message;
    }
}