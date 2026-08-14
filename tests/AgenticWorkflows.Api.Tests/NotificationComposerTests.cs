using AgenticWorkflows.Api.Models;
using AgenticWorkflows.Api.Services;

namespace AgenticWorkflows.Api.Tests;

public sealed class NotificationComposerTests
{
    [Fact]
    public void BuildCreatedNotification_truncates_descriptions_longer_than_90_characters_with_ellipsis()
    {
        var longDescription = new string('x', 100);
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            longDescription,
            3,
            WorkItemStatus.Todo,
            null);

        var result = NotificationComposer.BuildCreatedNotification(item);

        Assert.Contains("Description: " + new string('x', 87) + "...", result);
        Assert.DoesNotContain(new string('x', 88), result);
    }

    [Fact]
    public void BuildDueSoonNotification_truncates_descriptions_longer_than_90_characters_with_ellipsis()
    {
        var longDescription = new string('y', 95);
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            longDescription,
            3,
            WorkItemStatus.InProgress,
            new DateOnly(2026, 6, 15));

        var result = NotificationComposer.BuildDueSoonNotification(item);

        Assert.Contains("Description: " + new string('y', 87) + "...", result);
        Assert.DoesNotContain(new string('y', 88), result);
    }

    [Fact]
    public void BuildCreatedNotification_omits_due_date_line_when_DueDate_is_null()
    {
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            "Description text",
            3,
            WorkItemStatus.Todo,
            null);

        var result = NotificationComposer.BuildCreatedNotification(item);

        Assert.DoesNotContain("Due date:", result);
    }

    [Fact]
    public void BuildDueSoonNotification_omits_due_date_line_when_DueDate_is_null()
    {
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            "Description text",
            3,
            WorkItemStatus.InProgress,
            null);

        var result = NotificationComposer.BuildDueSoonNotification(item);

        Assert.DoesNotContain("Due date:", result);
    }

    [Fact]
    public void BuildCreatedNotification_includes_due_date_line_when_DueDate_is_set()
    {
        var dueDate = new DateOnly(2026, 6, 15);
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            "Description text",
            3,
            WorkItemStatus.Todo,
            dueDate);

        var result = NotificationComposer.BuildCreatedNotification(item);

        Assert.Contains("Due date: 2026-06-15", result);
    }

    [Fact]
    public void BuildDueSoonNotification_includes_due_date_line_when_DueDate_is_set()
    {
        var dueDate = new DateOnly(2026, 6, 20);
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            "Description text",
            3,
            WorkItemStatus.InProgress,
            dueDate);

        var result = NotificationComposer.BuildDueSoonNotification(item);

        Assert.Contains("Due date: 2026-06-20", result);
    }

    [Fact]
    public void BuildCreatedNotification_contains_expected_next_step_text()
    {
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            "Description text",
            3,
            WorkItemStatus.Todo,
            null);

        var result = NotificationComposer.BuildCreatedNotification(item);

        Assert.Contains("Next step: Review the backlog and assign an owner.", result);
    }

    [Fact]
    public void BuildDueSoonNotification_contains_expected_next_step_text()
    {
        var item = new WorkItem(
            Guid.NewGuid(),
            "Test Item",
            "Description text",
            3,
            WorkItemStatus.InProgress,
            new DateOnly(2026, 6, 15));

        var result = NotificationComposer.BuildDueSoonNotification(item);

        Assert.Contains("Next step: Confirm the item still belongs in this sprint.", result);
    }
}
