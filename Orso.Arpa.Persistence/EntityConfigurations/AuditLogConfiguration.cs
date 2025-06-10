using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orso.Arpa.Domain.AuditLogDomain.Enums;
using Orso.Arpa.Domain.AuditLogDomain.Model;

namespace Orso.Arpa.Persistence.EntityConfigurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
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
                    e => JsonSerializer.Serialize(e, (JsonSerializerOptions)null),
                    e => JsonSerializer.Deserialize<Dictionary<string, object>>(e, (JsonSerializerOptions)null))
                .Metadata.SetValueComparer(dictionaryValueComparer);

            builder
                .Property(e => e.NewValues)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, (JsonSerializerOptions)null),
                    e => JsonSerializer.Deserialize<Dictionary<string, object>>(e, (JsonSerializerOptions)null))
                .Metadata.SetValueComparer(dictionaryValueComparer);

            builder
                .Property(e => e.Type)
                .HasConversion(
                    v => v.ToString(),
                    v => Enum.Parse<AuditLogType>(v));

            builder
                .Property(e => e.ChangedColumns)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, (JsonSerializerOptions)null),
                    e => JsonSerializer.Deserialize<List<string>>(e, (JsonSerializerOptions)null))
                .Metadata.SetValueComparer(new ValueComparer<IList<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c));
        }
    }
}
