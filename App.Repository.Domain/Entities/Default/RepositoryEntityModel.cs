using System.Diagnostics;

namespace App.Repository.Domain.Entities.Default
{
    public class RepositoryEntityModel
    {
        public Guid Id { get; set; }
        public DateTime SysDateInsert { get; set; }
        public DateTime SysDateUpdate { get; set; }
        public Guid SysUserInsert { get; set; }
        public Guid SysUserUpdate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
