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

            Stopwatch s = new Stopwatch();
            using (var sim = new CounterSimulator()) {
                for (int numQubits = 1; numQubits < maxNumQubits; numQubits++) {
                    System.Console.WriteLine(String.Format("Testing with {0} qubits: ", numQubits));

                    s.Start();
                    for (int i = 0; i < numTrials; i++) {
                        var r = DJ_Algorithm_F_0_Test.Run(sim, numQubits).Result;
                    }
                    s.Stop();
                    System.Console.WriteLine(String.Format("Time for f(x) = 0 averaged over {0} trials: {1}", numTrials, s.ElapsedMilliseconds));
                    s.Reset();

                    s.Start();
                    for (int i = 0; i < numTrials; i++) {
                        var r = DJ_Algorithm_F_1_Test.Run(sim, numQubits).Result;
                    }
                    s.Stop();
                    System.Console.WriteLine(String.Format("Time for f(x) = 1 averaged over {0} trials: {1}", numTrials, s.ElapsedMilliseconds));
                    s.Reset();
                }
            }
        }
    }
}
