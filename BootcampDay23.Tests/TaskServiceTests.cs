using Xunit;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using BootcampDay23AdvancedReading.Data;
using BootcampDay23AdvancedReading.Models;
using BootcampDay23AdvancedReading.Repository;
using BootcampDay23AdvancedReading.Services;

public class TaskServiceTests
{
    private readonly TaskService _taskService;
    private readonly AppDbContext _context;
    private readonly TaskRepository _taskRepository;
    private readonly Mock<ILogger<TaskService>> _loggerMock;

    public TaskServiceTests()
    {
        // in-memory database for isolation test runs
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"TaskServiceTests_{Guid.NewGuid()}")
            .Options;

        _context = new AppDbContext(options);
        _taskRepository = new TaskRepository(_context);

        // Mock logger and inject it into the service
        _loggerMock = new Mock<ILogger<TaskService>>();
        _taskService = new TaskService(_taskRepository, _loggerMock.Object);
    }

    [Fact]
    public async Task AddTaskAsync_Should_Add_New_Task()
    {
        // Arrange
        var title = "New Test Task";

        // Act
        await _taskService.AddTaskAsync(title);
        var tasks = await _taskService.GetTasksAsync();

        // Assert
        tasks.Should().ContainSingle(t => t.Title == title);
    }

    [Fact]
    public async Task CompleteTaskAsync_Should_Set_Task_As_Completed()
    {
        // Arrange
        var task = new TaskItem("Incomplete Task");
        await _taskRepository.AddAsync(task);

        // Act
        await _taskService.CompleteTaskAsync(task.Id);
        var completedTask = await _taskRepository.GetByIdAsync(task.Id);

        // Assert
        completedTask.Should().NotBeNull();
        completedTask!.IsCompleted.Should().BeTrue();
    }
}
