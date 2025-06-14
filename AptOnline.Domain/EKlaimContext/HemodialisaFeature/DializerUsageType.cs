﻿using AptOnline.Application.EKlaimContext.HemodialisaFeature;
using Ardalis.GuardClauses;

namespace AptOnline.Domain.EKlaimContext.HemodialisaFeature;

public record DializerUsageType(string DializerUsageId, string DializerUsageName) : IDializerUsageKey
{
    public static DializerUsageType Create(string id, string name)
    {
        Guard.Against.NullOrWhiteSpace(id, nameof(id));
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return new DializerUsageType(id, name);
    }
    public static DializerUsageType Default => new("-", "-");
    public static IDializerUsageKey Key(string id)
        => Default with {DializerUsageId = id};
}