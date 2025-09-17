using BootcampDay7.Demo;

Console.WriteLine("=== DATE AND TIME HANDLING DEMONSTRATION ===\n");

// Walk through each concept systematically, following the training material structure
DemoClassDTSB.DemonstrateTimeSpanBasics();
DemoClassDTSO.DemonstrateTimeSpanOperations();
DemoClassDTSC.DemonstrateTimeSpanConversion();
DemoClassDDTB.DemonstrateDateTimeBasics();
DemoClassDDDTO.DemonstrateDateTimeVsDateTimeOffset();
DemoClassDDTCC.DemonstrateDateTimeConstructionAndConversion();
DemoClassDDTO.DemonstrateDateTimeOperations();
DemoClassDDTF.DemonstrateDateTimeFormatting();
DemoClassDNDT.DemonstrateNullableDateTimes();
DemoClassDDOT.DemonstrateDateOnlyAndTimeOnly();
DemoClassDRWS.DemonstrateRealWorldScenarios();

Console.WriteLine("\n=== END OF DEMONSTRATION ===");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();