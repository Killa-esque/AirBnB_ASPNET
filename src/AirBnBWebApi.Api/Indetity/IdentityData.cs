namespace AirBnBWebApi.Api.Indetity
{
    public static class IdentityData
    {
        public static class Roles
        {
            public const string Admin = "Admin";
            public const string Host = "Host";
            public const string User = "User";

        }
        public static class Claims
        {
            public const string Role = "Role";
        }
        public static class Policies
        {
            public const string AdminPolicy = "RequireAdmin";
            public const string HostPolicy = "RequireHost";
            public const string UserPolicy = "UserPolicy";
        }
    }
}
