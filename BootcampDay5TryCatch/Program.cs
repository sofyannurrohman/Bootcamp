
using BootcampDay5.TryStatment;


Console.WriteLine("=== Exception Handling in C# - Complete Training Demonstration ===\n");
Console.WriteLine("This program demonstrates all major concepts of exception handling:");
Console.WriteLine("• Basic try-catch blocks");
Console.WriteLine("• Multiple catch clauses with specific exception types");
Console.WriteLine("• Exception filters with 'when' keyword");
Console.WriteLine("• Finally blocks for cleanup");
Console.WriteLine("• Using statements for automatic resource disposal");
Console.WriteLine("• Throwing and rethrowing exceptions");
Console.WriteLine("• TryXXX pattern as alternative to exceptions");
Console.WriteLine("• Real-world exception handling scenarios\n");

// Run all demonstrations in order
TryClassBTC.BasicTryCatchDemo();
TryClassMCB.MultipleCatchBlocksDemo();
TryClassEFD.ExceptionFiltersDemo();
TryClassFB.FinallyBlockDemo();
TryClassUST.UsingStatementDemo();
TryClassUSD.UsingDeclarationDemo(); // New C# 8+ feature
TryClassTEX.ThrowingExceptionsDemo();
TryClassTE.ThrowExpressionsDemo(); // C# 7+ feature
TryClassRE.RethrowingExceptionsDemo();
TryClassCET.CommonExceptionTypesDemo(); // New section
TryClassTPP.TryParsePatternDemo();
TryClassANTIN.ArgumentNullThrowIfNullDemo(); // .NET 6+ feature
TryClassRCA.ReturnCodesAlternativeDemo();
TryClassRWS.RealWorldScenarioDemo();

Console.WriteLine("=== Training Summary ===");
Console.WriteLine("You've now seen comprehensive examples of exception handling in C#.");
Console.WriteLine("Remember: Use exceptions for truly exceptional cases, not for normal program flow!");
Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();