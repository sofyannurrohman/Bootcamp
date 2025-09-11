using Classes;
PointStruct p1 = new PointStruct(5, 10);
p1.Display();

PointStruct p2;     // Structs can be used without 'new'
p2.X = 1;
p2.Y = 2;
p2.Display();

// Compare Struct with Class
PointStruct ps1 = new PointStruct(1, 2);
PointStruct ps2 = ps1;  // copy by value
ps2.X = 99;

Console.WriteLine("Struct:");
Console.WriteLine($"ps1.X = {ps1.X}, ps1.Y = {ps1.Y}"); // ps1 unchanged
Console.WriteLine($"ps2.X = {ps2.X}, ps2.Y = {ps2.Y}"); // ps2 changed

Console.WriteLine();

// --- Class behavior (Reference Type) ---
PointClass pc1 = new PointClass(1, 2);
PointClass pc2 = pc1;  // copy reference
pc2.X = 99;

Console.WriteLine("Class:");
Console.WriteLine($"pc1.X = {pc1.X}, pc1.Y = {pc1.Y}"); // pc1 changed too
Console.WriteLine($"pc2.X = {pc2.X}, pc2.Y = {pc2.Y}"); // pc2 same object

/* 
Personal Notes:
--- Struct behavior (Value Type) ---
Struct Output:
ps1.X = 1, ps1.Y = 2
ps2.X = 99, ps2.Y = 2
Use case:
If your DTO is tiny (e.g., 2–3 fields).
If immutability is desirable.
If you’re doing high-performance low-level work (like Unity game dev, numeric processing, or very high-throughput services).

--- Class behavior (Reference Type) ---
Class Output:
pc1.X = 99, pc1.Y = 2
pc2.X = 99, pc2.Y = 2
Use case:
✅ Use class for most DTOs (especially in web apps, APIs, business logic).
✅ Use record class if DTOs are immutable and you want value-based equality:
 */