using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace appweb_back.Profiles.Domain.Model.Aggregates;

public partial class Profile : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedAt { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedAt { get; set; }
}
