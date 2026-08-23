namespace Summary.WASenderApi
{
    using Core.Security.Permissions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Permissions : IPermissionProvider
    {
        internal static Permission ManageWASenderApiSettings =
            new Permission(nameof(ManageWASenderApiSettings), "Manage WASenderApi Settings");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageWASenderApiSettings }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new []{ ManageWASenderApiSettings }
                }
            };
        }
    }
}