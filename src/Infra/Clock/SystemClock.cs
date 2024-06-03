using Domain;

namespace Infra.Clock;

public class SystemClock : IClock
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}