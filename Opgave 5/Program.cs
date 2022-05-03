using System;
using System.Collections;
using System.Threading;

namespace Opgave_5
{
    internal class Program
    {
        static Queue queue = new Queue();
        static readonly object locker = new object();
        static int maxLength = 3;

        static void Main(string[] args)
        {
            Thread producer = new Thread(Producer);
            Thread consumer = new Thread(Consumer);

            producer.Name = "producer";
            consumer.Name = "consumer";

            producer.Start();
            consumer.Start();

            producer.Join();
            consumer.Join();
        }

        static void Producer()
        {
            lock (locker)
            {
                while (true)
                {
                    if (queue.Count != 0)
                    {
                        Monitor.Wait(locker);
                    }
                    for (int i = 0; i < maxLength; i++)
                    {
                        queue.Enqueue(i);
                        Console.WriteLine($"Producer has produced: {queue.Count}");
                    }
                    Console.WriteLine("Producer waits");

                    Monitor.PulseAll(locker);

                }
            }
        }

        static void Consumer()
        {
            lock (locker)
            {
                while (true)
                {
                    if (queue.Count < maxLength)
                    {
                        Monitor.Wait(locker);
                    }
                    for (int i = 0; i < maxLength; i++)
                    {
                        queue.Dequeue();
                        Console.WriteLine($"Consumer has consumed {queue.Count}");
                    }
                    Monitor.PulseAll(locker);
                    Thread.Sleep(2000);
                }
            }
        }
    }
}