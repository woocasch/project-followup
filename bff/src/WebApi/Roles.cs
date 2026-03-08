namespace ProjectFollowUp.BFF.WebApi;

public static class Roles
{
    public static class UserManagement
    {
        public const string CreateUser = "create-user";

        public const string SearchUsers = "search-user";
    }

    public static class ProjectManagement
    {
        public const string CreateProject = "create-project";

        public const string UpdateProject = "update-project";
    }
}
