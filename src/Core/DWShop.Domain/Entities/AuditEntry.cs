using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DWShop.Domain.Entities
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry) {
            Entry = entry;
        }

        public EntityEntry Entry { get; set; }
        public string UserId { get; set; }
        public string TableName { get; set; }

        public Dictionary<string, object> KeyValues { get; set; } = new();
        public Dictionary<string, object> OldValues { get; set; } = new();
        public Dictionary<string, object> NewValues { get; set; } = new();
        public List<PropertyEntry> TemporaryProperties { get; } = new ();
        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; } = new ();   
        public bool HasTemporaryPropeties => TemporaryProperties.Any();

        public Audit ToAudit()
        {
            var audit = new Audit
            {
                UserId = UserId,
                TableName = TableName,
                Type = AuditType.ToString(),
                DateTime = DateTime.Now,
                PrimaryKey = JsonSerializer.Serialize(KeyValues),
                OldValues = OldValues.Any() ? JsonSerializer.Serialize(OldValues) : null,
                NewValues = NewValues.Any() ? JsonSerializer.Serialize(NewValues) : null,
                AffectedColumns = ChangedColumns.Any() ? JsonSerializer.Serialize(ChangedColumns) : null
            };

            return audit;
        }

    }

    public enum AuditType: byte
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}
