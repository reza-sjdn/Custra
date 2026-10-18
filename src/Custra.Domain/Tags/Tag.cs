using Custra.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Custra.Domain.Tags;


public class Tag : OrganizationOwnedEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Color { get; private set; } = "#808080";

    public ICollection<CustomerTag> CustomerTags { get; private set; } = new List<CustomerTag>();
    public ICollection<ContactTag> ContactTags { get; private set; } = new List<ContactTag>();
    public ICollection<LeadTag> LeadTags { get; private set; } = new List<LeadTag>();
    public ICollection<OpportunityTag> OpportunityTags { get; private set; } = new List<OpportunityTag>();

    private Tag()
    {
    }

    public Tag(string name, string color)
    {
        SetDetails(name, color);
    }

    public void Update(string name, string color)
    {
        SetDetails(name, color);
    }

    private void SetDetails(string name, string color)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tag name is required.", nameof(name));

        if (name.Trim().Length > 100)
            throw new ArgumentException("Tag name cannot exceed 100 characters.", nameof(name));

        if (string.IsNullOrWhiteSpace(color))
            throw new ArgumentException("Tag color is required.", nameof(color));

        var normalizedColor = color.Trim();

        if (!System.Text.RegularExpressions.Regex.IsMatch(
                normalizedColor,
                "^#[0-9a-fA-F]{6}$"))
        {
            throw new ArgumentException(
                "Tag color must be a valid hexadecimal color, such as #3498DB.",
                nameof(color));
        }

        Name = name.Trim();
        Color = normalizedColor.ToUpperInvariant();
    }
}
