using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CSharp6
{
    class Program
    {
        static void Main(string[] args)
        {
            var ev = new EventManager(new AlarmGateway());
            var triggerableEvent = ev.CreateEvent(TriggerableEventType.Alarm);
            triggerableEvent.Trigger();

            Console.ReadLine(); 
        }
    }

    public interface ISorterAlarmGateway
    {
        void RaiseAlarm(string msg);
    }
    public class AlarmGateway : ISorterAlarmGateway
    {
        public void RaiseAlarm(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    public interface IEventManager
    {
        ITriggerableEvent CreateEvent(TriggerableEventType triggerable);
    }
    public class EventManager : IEventManager
    {
        public ISorterAlarmGateway Gateway { get; private set; }

        public EventManager(ISorterAlarmGateway gateway)
        {
            Gateway = gateway;
        }

        public ITriggerableEvent CreateEvent(TriggerableEventType triggerable)
        {
            switch (triggerable)
            {
                case TriggerableEventType.Alarm:
                    return new AlarmEvent(Gateway);
                case TriggerableEventType.Warning:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentException(nameof(triggerable));
            }
        }
    }

    
    public enum TriggerableEventType
    {
        Alarm, Warning
    }

    public interface ITriggerableEvent
    {
        void Trigger();
    }

    public abstract class AlarmTypeEvent : ITriggerableEvent
    {
        private ISorterAlarmGateway _gateway;

        public AlarmTypeEvent(ISorterAlarmGateway alarmGateway)
        {

        }

        public abstract void Trigger();
    }

    public class AlarmEvent : AlarmTypeEvent
    {
        private ISorterAlarmGateway _gateway;
        public AlarmEvent(ISorterAlarmGateway alarmGateway) : base(alarmGateway)
        {
            _gateway = alarmGateway;
        }

        public override void Trigger()
        {
            _gateway.RaiseAlarm("Alarm raised!");
        }
    }
}
