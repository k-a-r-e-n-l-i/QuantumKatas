using Microsoft.Quantum.Simulation.XUnit;
using Microsoft.Quantum.Simulation.Simulators;
using Xunit.Abstractions;
using System.Diagnostics;

using System;


// using Microsoft.Quantum.Katas;
using Microsoft.Quantum.Simulation.XUnit;
using Microsoft.Quantum.Simulation.Simulators;

using Xunit.Abstractions;


namespace Quantum.Kata.GroversAlgorithm
{
    public class ProfilingRunner
    {
        static void Main()
        {
            
            using (var sim = new QuantumSimulator()) {
                for (int n = 2; n < 12; n++) {
                    Stopwatch s = new Stopwatch();
                    s.Start();
                    var res = T13_Oracle_ArbitraryPattern_Test.Run(sim, n).Result;
                    if(n == 5){
                        for(int j = 0; j < 101; j++){
                            var res1 = T13_Oracle_ArbitraryPattern_Test.Run(sim, n).Result;
                            if (j%10 == 0){
                                Console.WriteLine(s.ElapsedMilliseconds);
                            }
                        }
                    }
                    s.Stop();
                    long ts = s.ElapsedMilliseconds;
                    // Console.WriteLine(ts);
                    // System.Console.WriteLine(String.Format("Time for T13 with n={1}: {0}", ts, n));
                }
                
            }
        }
    }
}
