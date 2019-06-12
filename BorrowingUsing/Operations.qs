namespace Quantum.BU {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Canon;

    operation UsingDriver () : (Result, Result) {
    	mutable before = Zero;
    	mutable after = Zero;
        using (qubit = Qubit()) {
        	set before = M(qubit);
            X(qubit);
            set after = M(qubit);
        }
        return (before, after);
    }

    operation BorrowingDriver () : (Result, Result) {
    	mutable before = Zero;
    	mutable after = Zero;
        borrowing (qubit = Qubit()) {
        	set before = M(qubit);
            X(qubit);
            set after = M(qubit);
        }
        return (before, after);
    }
}