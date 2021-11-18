namespace Microsoft.Teams.Apps.CompanyCommunicator.Controllers
{
    public class EventFilesController
    {
        public ConnectorBuilder ConfigureConnector()
{
    var connectorBuilder = new ConnectorBuilder();
    connectorBuilder
        .SetRequestConfiguration(
            (request, config) =>
            {
                config.AddProxyBackend("local", new LocalStorage(@"MyFiles"));
                config.AddResourceType("Files", resourceBuilder => resourceBuilder.SetBackend("local", "files"));
                config.AddAclRule(new AclRule(
                    new StringMatcher("*"), new StringMatcher("/"), new StringMatcher("*"),
                    new Dictionary<Permission, PermissionType>
                    {
                        { Permission.FolderView, PermissionType.Allow },
                        { Permission.FolderCreate, PermissionType.Allow },
                        { Permission.FolderRename, PermissionType.Allow },
                        { Permission.FolderDelete, PermissionType.Allow },
 
                        { Permission.FileView, PermissionType.Allow },
                        { Permission.FileCreate, PermissionType.Allow },
                        { Permission.FileRename, PermissionType.Allow },
                        { Permission.FileDelete, PermissionType.Allow },
 
                        { Permission.ImageResize, PermissionType.Allow },
                        { Permission.ImageResizeCustom, PermissionType.Allow }
                    }));
            })
        .SetAuthenticator(new MyAuthenticator());
 
    return connectorBuilder;
}
        
    }
}