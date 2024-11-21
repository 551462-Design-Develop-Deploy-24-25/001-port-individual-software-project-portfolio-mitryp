using ACW1.Core.XML;
using ACW1.Features.Users.Data.Entity.User;

namespace ACW1.Features.Users.Data.Storage;

public class UserStorage(string file) : XmlFileStorageBase<User>(file, "users");
