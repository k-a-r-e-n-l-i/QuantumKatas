// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

//////////////////////////////////////////////////////////////////////
// This file contains profiling code.
// The tasks themselves can be found in Tasks.qs file.
//////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;

using Microsoft.Quantum.Katas;
using Microsoft.Quantum.Simulation.XUnit;
using Microsoft.Quantum.Simulation.Simulators;

using Xunit.Abstractions;


namespace Quantum.Kata.DeutschJozsaAlgorithm
{
    public class ProfilingRunner
    {
        static void Main()
        {
            int numTrials = 10;
            int maxNumQubits = 23;
            int numBVTests = 5;

            Stopwatch s = new Stopwatch();
            using (var sim = new CounterSimulator()) {
                for (int numQubits = 1; numQubits < maxNumQubits; numQubits++) {
                    // DEUTSCH-JOSZA ////////////////////////////////////////////////////////////////

                    System.Console.WriteLine(String.Format("Testing with {0} qubits: ", numQubits));

                    // constant: f(x) = 0
                    s.Start();
                    for (int i = 0; i < numTrials; i++) {
                        var r = DJ_Algorithm_F_0_Test.Run(sim, numQubits).Result;
                    }
                    s.Stop();
                    System.Console.WriteLine(String.Format("Time for f(x) = 0 averaged over {0} trials: {1}", numTrials, s.ElapsedMilliseconds));
                    s.Reset();

                    // constant: f(x) = 1  
                    s.Start();
                    for (int i = 0; i < numTrials; i++) {
                        var r = DJ_Algorithm_F_1_Test.Run(sim, numQubits).Result;
                    }
                    s.Stop();
                    System.Console.WriteLine(String.Format("Time for f(x) = 1 averaged over {0} trials: {1}", numTrials, s.ElapsedMilliseconds));
                    s.Reset();

                    // balanced: f(x) = 1 if x has odd number of 1s, 0 otherwise
                    s.Start();
                    for (int i = 0; i < numTrials; i++) {
                        var r = DJ_Algorithm_OddNumberOfOnes_Test.Run(sim, numQubits).Result;
                    }
                    s.Stop();
                    System.Console.WriteLine(String.Format("Time for balanced f(x) (odd number ones) averaged over {0} trials: {1}", numTrials, s.ElapsedMilliseconds));
                    s.Reset();

                    // BERNSTEIN-VAZIRANI ////////////////////////////////////////////////////////////////

                    // test with many random f(x) = ax

                    Random rnd = new Random();
                    for (int j = 0; j < numBVTests; j++) {
                        s.Start();
                        // generate random a
                        int n = rnd.Next(0, 2 ^ numQubits-1);
                        var a = IntArrFromPositiveInt.Run(sim, n, numQubits).Result;
                        for (int i = 0; i < numTrials; i++) {
                            var r = BV_Algorithm_Test.Run(sim, a).Result;
                        }
                        s.Stop();
                        System.Console.WriteLine(String.Format("Time for BV with a = {2} averaged over {0} trials: {1}", numTrials, s.ElapsedMilliseconds, n));
                        s.Reset();
                    }
                }
            }
        }
    }
}
