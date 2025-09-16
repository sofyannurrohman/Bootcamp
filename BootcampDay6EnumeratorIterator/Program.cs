using Demos;

Console.WriteLine("=== ENUMERATION AND ITERATORS DEMONSTRATION ===\n");

// 1. Basic enumeration with foreach
DemoClassDBE.DemonstrateBasicEnumeration();

// 2. Manual enumeration (what foreach does under the hood)
DemoClassDME.DemonstrateManualEnumeration();

// 3. Custom enumerable and enumerator
DemoClassDCE.DemonstrateCustomEnumerable();

// 4. Collection initializers
DemoClassDCI.DemonstrateCollectionInitializers();

// 5. Collection expressions (C# 12+)
DemoClassDCEX.DemonstrateCollectionExpressions();

// 6. Iterator methods with yield
DemoClassDI.DemonstrateIterators();

// 7. Composing sequences
DemoClassDSC.DemonstrateSequenceComposition();

// 8. Iterator with try-finally
DemoClassDIF.DemonstrateIteratorWithFinally();

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();