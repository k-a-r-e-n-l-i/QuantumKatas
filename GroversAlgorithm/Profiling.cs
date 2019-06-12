using Microsoft.Quantum.Simulation.XUnit;
using Microsoft.Quantum.Simulation.Simulators;
using Xunit.Abstractions;
using System.Diagnostics;

using System;

namespace Quantum.Kata.GroversAlgorithm
{
    public class ProfilingRunner
    {
        static void Main()
        {
            
            using (var sim = new QuantumSimulator()) {
                //fix startup overhead time
                Stopwatch s = new Stopwatch();
                var res = T31_GroversSearch_Test.Run(sim, 2).Result;
                var iters = 10;
                s.Start();
                for(int i = 0; i < 100; i++){
                    var res3 = T31_GroversSearch_Test.Run(sim, 5).Result;
                }
                Console.WriteLine(s.ElapsedMilliseconds/100);
                
                
                
                for (int n = 2; n < 12; n++) {
                    s.Restart();
                    
                    for(int j = 0; j < iters; j++){
                        var res2 = T31_GroversSearch_Test.Run(sim, n).Result;
                    }
                    s.Stop();
                    long ts = s.ElapsedMilliseconds;
                    Console.WriteLine(ts/iters);
                    // System.Console.WriteLine(String.Format("Time for T13 with n={1}: {0}", ts, n));
                }
            }
        }
    }
}
