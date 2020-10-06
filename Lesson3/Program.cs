using System;

namespace Lesson3
{
    public delegate void MyDelegate<T>(T o);
   // public delegate void MyDelegate(object o);
    class Source<T>
    {
        public event EventHandler<EventArgsGen<T>> Run;

        public void Start()
        {
            Console.WriteLine("RUN");
            if (Run != null) Run.Invoke(this, new EventArgsGen<T> { t  });
        }
    }

    public class EventArgsGen<T>
    {
        public T t {get;set;}
        public EventArgsGen(T target) => t = target;
    }

    class Observer1 // Наблюдатель 1
    {
        public void Do(object o)
        {
            Console.WriteLine("Первый. Принял, что объект {0} побежал", o);
        }
        public void Do(string o)
        {
            Console.WriteLine("Четвертый. Принял, что объект {0} побежал", o);
        }
    }
    class Observer2 // Наблюдатель 2
    {
        public void Do(object o)
        {
            Console.WriteLine("Второй. Принял, что объект {0} побежал", o);
        }
    }
    class Observer3 // Наблюдатель 2
    {
        public void Do(object o)
        {
            Console.WriteLine("Третий. Принял, что объект {0} побежал", o);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source();
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();
            Observer3 o3 = new Observer3();
            Observer1 o4 = new Observer1();
            MyDelegate<object> d1 = new MyDelegate<object>(o1.Do);
            MyDelegate<String> sd = new MyDelegate<string>(o3.Do);
            s.Run += d1;
            s.Run += o2.Do;
            s.Start();
            s.Run += sd;
            s.Run += o4.Do;
            s.Run -= d1;
            s.Start();
        }
    }
}
