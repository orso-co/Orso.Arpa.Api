using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;

namespace Orso.Arpa.Persistence.Configurations
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder
                .Property(e => e.Type)
                .HasMaxLength(10);

            builder
                .Property(e => e.TableName)
                .HasMaxLength(200);

            var dictionaryValueComparer = new ValueComparer<Dictionary<string, object>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c);

            builder
                .Property(e => e.OldValues)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, null),
                    e => JsonSerializer.Deserialize<Dictionary<string, object>>(e, null))
                .Metadata.SetValueComparer(dictionaryValueComparer);

            builder
                .Property(e => e.NewValues)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, null),
                    e => JsonSerializer.Deserialize<Dictionary<string, object>>(e, null))
                .Metadata.SetValueComparer(dictionaryValueComparer);

            builder
                .Property(e => e.KeyValues)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, null),
                    e => JsonSerializer.Deserialize<Dictionary<string, Guid>>(e, null))
                .Metadata.SetValueComparer(new ValueComparer<Dictionary<string, Guid>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c));

            builder
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => (AuditType)Enum.Parse(typeof(AuditType), v));

            builder
                .Property(e => e.ChangedColumns)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, null),
                    e => JsonSerializer.Deserialize<List<string>>(e, null))
                .Metadata.SetValueComparer(new ValueComparer<IList<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c));
        }
    }
}
