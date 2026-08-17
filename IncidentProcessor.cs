using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SA_Pulse
{
    // Process incidents on a background thread using a standard Queue
    public class IncidentProcessor
    {
        private Queue<Incident> queue = new Queue<Incident>();
        private bool isRunning;

        public void Start()
        {
            isRunning = true;
            // Start background worker thread
            Thread workerThread = new Thread(ProcessQueue);
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        public void Stop()
        {
            isRunning = false;
        }

        public void AddIncident(Incident incident)
        {
            // Lock queue so multiple threads don't modify it at the same time
            lock (queue)
            {
                queue.Enqueue(incident);
            }
            Console.WriteLine("[Queue] Incident added to processing queue.");
        }

        private void ProcessQueue()
        {
            while (isRunning)
            {
                Incident currentIncident = null;

                // Safely dequeue one item
                lock (queue)
                {
                    if (queue.Count > 0)
                    {
                        currentIncident = queue.Dequeue();
                    }
                }

                if (currentIncident != null)
                {
                    Console.WriteLine("[Worker] Processing incident...");
                    Thread.Sleep(1500); // Simulate processing work

                    // Bonus Feature: File I/O logging
                    string logText = $"[{DateTime.Now}] Processed incident: {currentIncident.GetType().Name}";
                    File.AppendAllText("incident_log.txt", logText);

                    Console.WriteLine("[Worker] Incident resolved and logged to file.");
                }
                else
                {
                    Thread.Sleep(500); // Wait briefly when queue is empty
                }
            }
        }
    }
}
