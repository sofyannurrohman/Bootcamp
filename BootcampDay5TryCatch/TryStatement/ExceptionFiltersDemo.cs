using System.Net;

namespace BootcampDay5.TryStatment;
public class TryClassEFD{
    
public static void ExceptionFiltersDemo()
    {
        Console.WriteLine("3. EXCEPTION FILTERS DEMONSTRATION");
        Console.WriteLine("==================================");

        // Simulate different web exception scenarios
        Console.WriteLine("Testing exception filters with 'when' keyword:");

        SimulateWebException(WebExceptionStatus.Timeout);
        SimulateWebException(WebExceptionStatus.SendFailure);
        SimulateWebException(WebExceptionStatus.ConnectFailure);

        Console.WriteLine();
    }

        static void SimulateWebException(WebExceptionStatus status)
        {
            try
            {
                // Create and throw a WebException with specific status
                var ex = new WebException("Simulated web error", status);
                throw ex;
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
            {
                Console.WriteLine("  Handled: Request timeout - retrying with longer timeout");
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.SendFailure)
            {
                Console.WriteLine("  Handled: Send failure - checking network connection");
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.ConnectFailure)
            {
                Console.WriteLine("  Handled: Connection failure - server might be down");
            }
            catch (WebException ex)
            {
                Console.WriteLine($"  Handled: Other web exception - {ex.Status}");
            }
        }
}