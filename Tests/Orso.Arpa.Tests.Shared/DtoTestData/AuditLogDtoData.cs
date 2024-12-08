using System;
using System.Collections.Generic;
using Orso.Arpa.Application.AuditLogApplication.Model;
using Orso.Arpa.Domain.AuditLogDomain.Enums;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class AuditLogDtoData
    {
        public static IList<AuditLogDto> Regions
        {
            get
            {
                return
                [
                    CreateRegion,
                    UpdateRegion,
                    DeleteRegion,
                ];
            }
        }

        public static AuditLogDto CreateRegion
        {
            get
            {
                var auditLog = new AuditLogDto
                {
                    Id = Guid.Parse("2b3023cf-d5d6-49e2-b45d-69614164e31c"),
                    CreatedBy = "Staff member",
                    CreatedAt = new DateTime(2030, 02, 02),
                    Type = AuditLogType.Create,
                    TableName = "Region",
                };

                auditLog.NewValues.Add("CreatedAt", "2030-02-02T17:58:39.6445856");
                auditLog.NewValues.Add("CreatedBy", "Staff Member");
                auditLog.NewValues.Add("Deleted", false);
                auditLog.NewValues.Add("ModifiedAt", null);
                auditLog.NewValues.Add("ModifiedBy", null);
                auditLog.NewValues.Add("Name", "Kauai");

                return auditLog;
            }
        }
        public static AuditLogDto UpdateRegion
        {
            get
            {
                var auditLog = new AuditLogDto
                {
                    Id = Guid.Parse("102a4884-df63-4c41-a9e5-36356e0740d7"),
                    CreatedBy = "Staff member",
                    CreatedAt = new DateTime(2030, 02, 02),
                    Type = AuditLogType.Update,
                    TableName = "RegionProxy",
                };

                auditLog.OldValues.Add("ModifiedAt", "2030-02-02T17:58:40.678602");
                auditLog.OldValues.Add("ModifiedBy", "Staff Member");
                auditLog.OldValues.Add("Name", "Kauai");

                auditLog.NewValues.Add("ModifiedAt", "2030-02-02T17:58:40.6961763");
                auditLog.NewValues.Add("ModifiedBy", "Staff Member");
                auditLog.NewValues.Add("Name", "Maui");

                auditLog.ChangedColumns.Add("ModifiedAt");
                auditLog.ChangedColumns.Add("ModifiedBy");
                auditLog.ChangedColumns.Add("Name");

                return auditLog;
            }
        }
        public static AuditLogDto DeleteRegion
        {
            get
            {
                var auditLog = new AuditLogDto
                {
                    Id = Guid.Parse("67671b83-3b61-4e70-b6ae-4bd46ff0f64e"),
                    CreatedBy = "Staff member",
                    CreatedAt = new DateTime(2030, 02, 02),
                    Type = AuditLogType.Update,
                    TableName = "RegionProxy",
                };

                auditLog.OldValues.Add("CreatedAt", "2030-02-02T17:58:39.644585");
                auditLog.OldValues.Add("CreatedBy", "Staff Member");
                auditLog.OldValues.Add("Deleted", false);
                auditLog.OldValues.Add("ModifiedAt", "2030-02-02T17:58:40.696176");
                auditLog.OldValues.Add("ModifiedBy", "Staff Member");
                auditLog.OldValues.Add("Name", "Kauai");

                auditLog.NewValues.Add("CreatedAt", "2030-02-02T17:58:39.644585");
                auditLog.NewValues.Add("CreatedBy", "Staff Member");
                auditLog.NewValues.Add("Deleted", true);
                auditLog.NewValues.Add("ModifiedAt", "2030-02-02T17:58:40.7213786");
                auditLog.NewValues.Add("ModifiedBy", "Staff Member");
                auditLog.NewValues.Add("Name", "Maui");

                auditLog.ChangedColumns.Add("CreatedAt");
                auditLog.ChangedColumns.Add("CreatedBy");
                auditLog.ChangedColumns.Add("Deleted");
                auditLog.ChangedColumns.Add("ModifiedAt");
                auditLog.ChangedColumns.Add("ModifiedBy");
                auditLog.ChangedColumns.Add("Name");

                return auditLog;
            }
        }
    }
}
