// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

//////////////////////////////////////////////////////////////////////
// This file contains testing harness for all tasks.
// You should not modify anything in this file.
// The tasks themselves can be found in Tasks.qs file.
//////////////////////////////////////////////////////////////////////

namespace Quantum.Kata.DeutschJozsaAlgorithm {
    
    open Microsoft.Quantum.Convert;
    open Microsoft.Quantum.Diagnostics;

    open Quantum.Kata.Utils;
    
    
    // ------------------------------------------------------
    operation ApplyOracle (qs : Qubit[], oracle : ((Qubit[], Qubit) => Unit)) : Unit {
        let N = Length(qs);
        oracle(qs[0 .. N - 2], qs[N - 1]);
    }
    
    
    // ------------------------------------------------------
    operation ApplyOracleA (qs : Qubit[], oracle : ((Qubit[], Qubit) => Unit is Adj)) : Unit
    is Adj {        
        let N = Length(qs);
        oracle(qs[0 .. N - 2], qs[N - 1]);
    }
    
    
    // ------------------------------------------------------
    operation AssertTwoOraclesAreEqual (nQubits : Range, 
        oracle1 : ((Qubit[], Qubit) => Unit), 
        oracle2 : ((Qubit[], Qubit) => Unit is Adj)) : Unit {
        let sol = ApplyOracle(_, oracle1);
        let refSol = ApplyOracleA(_, oracle2);
        
        for (i in nQubits) {
            AssertOperationsEqualReferenced(i + 1, sol, refSol);
        }
    }
    
    // ------------------------------------------------------
    operation AssertTwoOraclesWithIntAreEqual (r : Int[], 
        oracle1 : ((Qubit[], Qubit, Int[]) => Unit), 
        oracle2 : ((Qubit[], Qubit, Int[]) => Unit is Adj)) : Unit {
        AssertTwoOraclesAreEqual(Length(r) .. Length(r), oracle1(_, _, r), oracle2(_, _, r));
    }
        
    // ------------------------------------------------------
    function AllEqualityFactI (actual : Int[], expected : Int[], message : String) : Unit {
        
        let n = Length(actual);
        if (n != Length(expected)) {
            fail message;
        }
        
        for (idx in 0 .. n - 1) {
            if (actual[idx] != expected[idx]) {
                fail message;
            }
        }
    }
    
    
    // ------------------------------------------------------
    function IntArrFromPositiveInt (n : Int, bits : Int) : Int[] {
        
        let rbool = IntAsBoolArray(n, bits);
        mutable r = new Int[bits];
        
        for (i in 0 .. bits - 1) {
            if (rbool[i]) {
                set r w/= i <- 1;
            }
        }
        
        return r;
    }
    
    
    // ------------------------------------------------------
    operation AssertBVAlgorithmWorks (r : Int[]) : Unit {
        let oracle = Oracle_ProductFunction_Reference(_, _, r);
        AllEqualityFactI(BV_Algorithm(Length(r), oracle), r, "Bernstein-Vazirani algorithm failed");

        let nu = GetOracleCallsCount(oracle);
        EqualityFactB(nu <= 1, true, $"You are allowed to call the oracle at most once, and you called it {nu} times");
    }
    
    
    operation T22_BV_Algorithm_Test () : Unit {
        ResetOracleCallsCount();
        
        // test BV the way we suggest the learner to test it:
        // apply the algorithm to reference oracles and check that the output is as expected
        for (bits in 1 .. 4) {
            for (n in 0 .. 2 ^ bits - 1) {
                let r = IntArrFromPositiveInt(n, bits);
                AssertBVAlgorithmWorks(r);
            }
        }
        
        AssertBVAlgorithmWorks([1, 1, 1, 0, 0]);
        AssertBVAlgorithmWorks([1, 0, 1, 0, 1, 0]);
    }
    
    
    // ------------------------------------------------------
    operation AssertDJAlgorithmWorks (N : Int, oracle : ((Qubit[], Qubit) => Unit), expected : Bool, msg : String) : Unit {
        EqualityFactB(DJ_Algorithm(N, oracle), expected, msg);
        
        let nu = GetOracleCallsCount(oracle);
        EqualityFactB(nu <= 1, true, $"You are allowed to call the oracle at most once, and you called it {nu} times");
    }
    
    
    operation T31_DJ_Algorithm_Test () : Unit {

        ResetOracleCallsCount();
        
        // test DJ the way we suggest the learner to test it:
        // apply the algorithm to reference oracles and check that the output is as expected
        AssertDJAlgorithmWorks(4, Oracle_Zero_Reference,
                               true,  "f(x) = 0 not identified as constant");
        AssertDJAlgorithmWorks(4, Oracle_One_Reference, 
                               true,  "f(x) = 1 not identified as constant");
        AssertDJAlgorithmWorks(4, Oracle_Kth_Qubit_Reference(_, _, 1), 
                               false, "f(x) = x_k not identified as balanced");
        AssertDJAlgorithmWorks(4, Oracle_OddNumberOfOnes_Reference, 
                               false, "f(x) = sum of x_i not identified as balanced");
        AssertDJAlgorithmWorks(4, Oracle_ProductFunction_Reference(_, _, [1, 0, 1, 1]), 
                               false, "f(x) = sum of r_i x_i not identified as balanced");
        AssertDJAlgorithmWorks(4, Oracle_ProductWithNegationFunction_Reference(_, _, [1, 0, 1, 1]), 
                               false, "f(x) = sum of r_i x_i + (1 - r_i)(1 - x_i) not identified as balanced");
        AssertDJAlgorithmWorks(4, Oracle_HammingWithPrefix_Reference(_, _, [0, 1]), 
                               false, "f(x) = sum of x_i + 1 if prefix equals given not identified as balanced");
        AssertDJAlgorithmWorks(3, Oracle_MajorityFunction_Reference, 
                               false, "f(x) = majority function not identified as balanced");
    }
    
}

