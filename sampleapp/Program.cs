using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Etlx;

namespace SampleApp
{
    internal class Program
    {
        static ManualResetEvent s_event = new ManualResetEvent(false);

        delegate void GenerateEventsCallback(long currentKeyword);
        delegate void ProfilerDoneCallback();

        [DllImport("CorProfiler")]
        private static extern void PassCallbacks(GenerateEventsCallback events, ProfilerDoneCallback done);

        private static void DoOneEventLoop(long currentKeyword)
        {
            DiagnosticsClient client = new DiagnosticsClient(Environment.ProcessId);
            List<EventPipeProvider> providers = new List<EventPipeProvider>()
            {
                new EventPipeProvider("TestEventSource", eventLevel: EventLevel.Verbose, keywords: currentKeyword)
            };

            int totalEvents = 0;
            using (EventPipeSession session = client.StartEventPipeSession(providers, false))
            using (TestEventSource eventSource = new TestEventSource())
            {
                eventSource.Event1();
                eventSource.Event2();
                eventSource.Event3();
                eventSource.Event4();
                eventSource.Event5();
                eventSource.Event6();
                eventSource.Event7();
                eventSource.Event8();
                eventSource.Event9();
                eventSource.Event10();
                eventSource.Event11();
                eventSource.Event12();
                eventSource.Event13();
                eventSource.Event14();
                eventSource.Event15();
                eventSource.Event16();
                eventSource.Event17();
                eventSource.Event18();
                eventSource.Event19();
                eventSource.Event20();
                eventSource.Event21();
                eventSource.Event22();
                eventSource.Event23();
                eventSource.Event24();
                eventSource.Event25();
                eventSource.Event26();
                eventSource.Event27();
                eventSource.Event28();
                eventSource.Event29();
                eventSource.Event30();
                eventSource.Event31();
                eventSource.Event32();
                eventSource.Event33();
                eventSource.Event34();
                eventSource.Event35();
                eventSource.Event36();
                eventSource.Event37();
                eventSource.Event38();
                eventSource.Event39();
                eventSource.Event40();
                eventSource.Event41();
                eventSource.Event42();
                eventSource.Event43();
                //eventSource.Event44();
                //eventSource.Event45();
                //eventSource.Event46();
                //eventSource.Event47();
                eventSource.Event48();
                eventSource.Event49();
                eventSource.Event50();
                eventSource.Event51();
                eventSource.Event52();
                eventSource.Event53();
                eventSource.Event54();
                eventSource.Event55();
                eventSource.Event56();
                eventSource.Event57();
                eventSource.Event58();
                eventSource.Event59();
                eventSource.Event60();
                eventSource.Event61();
                eventSource.Event62();
                eventSource.Event63();
                eventSource.Event64();
                eventSource.Event65();

                EventPipeEventSource source = new EventPipeEventSource(session.EventStream);
                source.Dynamic.All += (TraceEvent traceEvent) =>
                {
                    if (!traceEvent.ProviderName.Equals("TestEventSource"))
                    {
                        return;
                    }

                    ++totalEvents;

                    long expectedEventKeyword = GetKeywordFromName(traceEvent.EventName);
                    long actualKeyword = (long)traceEvent.Keywords;
                    if (currentKeyword != 0 && actualKeyword != expectedEventKeyword)
                    {
                        Console.WriteLine($"FAILURE in keywords: saw 0x{actualKeyword:X} expected 0x{expectedEventKeyword:X} name:{traceEvent.EventName}");
                    }
                };

                Thread processingThread = new Thread(new ThreadStart(() =>
                {
                    source.Process();
                }));
                processingThread.Start();

                session.Stop();

                processingThread.Join();

                if (currentKeyword == 0 && totalEvents != 65)
                {
                    Console.WriteLine($"FAILURE: Saw keywords==0, totalEvents={totalEvents}");
                }

                if (currentKeyword != 0 && totalEvents != 1)
                {
                    Console.WriteLine($"FAILURE: Saw keywords==0x{currentKeyword:X}, totalEvents={totalEvents}");
                }
            }
        }

        static void Main(string[] args)
        {
            //PassCallbacks(DoOneEventLoop, () => s_event.Set());
            //s_event.WaitOne();
            //Console.WriteLine("Done in managed code!");

            for (int i = 0; i < 64; ++i)
            {
                if (i == 44
                    || i == 45
                    || i == 46
                    || i == 47)
                {
                    continue;
                }

                Console.WriteLine($"Iteration {i}");
                long keyword = 1L << i;
                DoOneEventLoop(keyword);
            }

            Console.WriteLine("Iteration with keywords=0"); 
            DoOneEventLoop(0);

            foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
            {
                if (module.FileName.Contains("coreclr"))
                {
                    Console.WriteLine(module.FileName);
                }
            }
        }

        private static long GetKeywordFromName(string eventName)
        {
            if (!eventName.StartsWith("Event")) throw new Exception();

            int eventNumber = int.Parse(eventName.Substring(5));
            if (eventNumber == 65)
            {
                return 0;
            }

            return 1L << eventNumber;
        }
    }
}