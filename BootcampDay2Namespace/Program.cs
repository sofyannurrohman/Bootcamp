// Global using directives (C# 10) - Apply to entire project
global using System;
global using System.Collections.Generic;

// Regular using directives
using System.Text;
using System.Reflection;

// Using static - Import static members directly
using static System.Console;
using static System.Math;

// Namespace aliases to avoid conflicts
using MyReflection = System.Reflection;
using WinVisibility = System.ComponentModel;

// Type aliases (C# 12 feature)
using NumberList = double[];
using StringPair = (string first, string second);
using PersonInfo = System.ValueTuple<string, int, string>;

DemonstrateRealWorldOrganization();

static void DemonstrateRealWorldOrganization()
{
    WriteLine("=== REAL-WORLD ORGANIZATION ===");
    WriteLine("How professionals structure namespaces in real projects\n");

    WriteLine("--- Typical Enterprise Application Structure ---");
    WriteLine("MyCompany.ProjectName.Domain.Models");
    WriteLine("MyCompany.ProjectName.Domain.Services");
    WriteLine("MyCompany.ProjectName.Infrastructure.Database");
    WriteLine("MyCompany.ProjectName.Infrastructure.External");
    WriteLine("MyCompany.ProjectName.Application.Commands");
    WriteLine("MyCompany.ProjectName.Application.Queries");
    WriteLine("MyCompany.ProjectName.Web.Controllers");
    WriteLine("MyCompany.ProjectName.Web.Models");

    WriteLine("\n--- Creating Instances from Organized Namespaces ---");

    // Demonstrate using our organized namespace structure
    var orderService = new Company.Services.Business.OrderService();
    var orderRepo = new Company.Data.Repositories.OrderRepository();
    var orderModel = new Company.Web.Models.OrderViewModel();

    WriteLine($"Business service: {orderService.GetServiceInfo()}");
    WriteLine($"Data repository: {orderRepo.GetRepositoryInfo()}");
    WriteLine($"Web model: {orderModel.GetModelInfo()}");

    WriteLine("\n--- Best Practices for Namespace Organization ---");
    WriteLine("1. Company/Organization root namespace");
    WriteLine("2. Project or product name");
    WriteLine("3. Layer or feature area");
    WriteLine("4. Specific component type");
    WriteLine();
    WriteLine("Example breakdown:");
    WriteLine("Microsoft.AspNetCore.Mvc.Controllers");
    WriteLine("  └── Microsoft (company)");
    WriteLine("      └── AspNetCore (product)");
    WriteLine("          └── Mvc (feature area)");
    WriteLine("              └── Controllers (component type)");

    WriteLine("\n--- File Organization Tips ---");
    WriteLine("• One namespace per folder (usually)");
    WriteLine("• Folder structure mirrors namespace structure");
    WriteLine("• Use file-scoped namespaces in C# 10+ for cleaner code");
    WriteLine("• Group related classes in same namespace");
    WriteLine("• Avoid deep nesting unless necessary");

    WriteLine("\n--- Using Directives Strategy ---");
    WriteLine("• Global using for framework types (System, System.Collections.Generic)");
    WriteLine("• Regular using for project-specific namespaces");
    WriteLine("• Using static for frequently used utility classes");
    WriteLine("• Aliases for conflict resolution and complex types");

    WriteLine("\nGood namespace organization is the foundation of maintainable code!");
}
// Nested namespace examples - Company structure
    namespace Company
    {
        namespace Data
        {
            namespace Models
            {
                public class DatabaseUser
                {
                    public int Id { get; set; }
                    public string Username { get; set; }
                    
                    public DatabaseUser(int id, string username)
                    {
                        Id = id;
                        Username = username;
                    }
                    
                    public string GetInfo()
                    {
                        return $"DB User: {Username} (ID: {Id})";
                    }
                }
            }
            
            namespace Repositories
            {
                public class OrderRepository
                {
                    public string GetRepositoryInfo()
                    {
                        return "Order Repository - handles order data persistence";
                    }
                }
            }
        }
        
        namespace Api
        {
            namespace Controllers
            {
                public class UserController
                {
                    public string GetControllerInfo()
                    {
                        return "User API Controller - handles HTTP requests";
                    }
                }
            }
        }
        
        namespace Web
        {
            namespace UI
            {
                namespace Components
                {
                    public class UserCard
                    {
                        public string GetComponentInfo()
                        {
                            return "User Card UI Component - displays user information";
                        }
                    }
                }
            }
            
            namespace Models
            {
                public class OrderViewModel
                {
                    public string GetModelInfo()
                    {
                        return "Order View Model - shapes data for web presentation";
                    }
                }
            }
        }
        
        namespace Services
        {
            namespace Business
            {
                public class OrderService
                {
                    public string GetServiceInfo()
                    {
                        return "Order Business Service - implements order processing logic";
                    }
                }
            }
        }
    }

    // Compact nested namespace syntax (alternative to above)
    namespace Company.Utils.Helpers
    {
        public class StringHelper
        {
            public static string FormatUserName(string firstName, string lastName)
            {
                return $"{firstName} {lastName}".Trim();
            }
        }
    }

    namespace Company.Utils.Extensions
    {
        public static class DateTimeExtensions
        {
            public static string ToFriendlyString(this DateTime dateTime)
            {
                return dateTime.ToString("MMM dd, yyyy");
            }
        }
    }
