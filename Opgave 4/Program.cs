using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Opgave_4_v2
{
    internal class Program
    {
        static Queue queue = new Queue();
        static int maxLength = 3;
        static int consumerCalls = 0;
        static int producerCalls = 0;
        static bool consumerActive = false;
        static bool producerActive = false;


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
            while (true)
            {
                while (producerCalls < 8 && consumerActive == false)
                {
                    producerActive = true;
                    if (queue.Count == maxLength)
                    {
                        Console.WriteLine($"Producer couldn't produce: {queue.Count}");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        for (int i = 0; i < maxLength; i++)
                        {
                            queue.Enqueue(i);
                            Console.WriteLine($"Producer has produced: {queue.Count}");
                        }
                    }
                    producerCalls++;
                    consumerCalls = 0;
                }
                producerActive = false;
                Thread.Sleep(2000);
            }
        }
        static void Consumer()
        {
            while (true)
            {
                while (consumerCalls < 8 && producerActive == false)
                {
                    consumerActive = true;
                    if (queue.Count == 0)
                    {
                        Console.WriteLine($"Consumer couldn't consume: {queue.Count}");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        for (int i = 0; i < maxLength; i++)
                        {
                            queue.Dequeue();
                            Console.WriteLine($"Consumer has consumed {queue.Count}");
                        }
                    }
                    consumerCalls++;
                    producerCalls = 0;
                }
                consumerActive = false;
            }
        }
    }
}
