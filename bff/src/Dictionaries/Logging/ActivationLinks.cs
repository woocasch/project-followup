namespace ProjectFollowUp.BFF.Dictionaries.Logging;

public static class ActivationLinks
{
    public static class Application
    {
        public const int LinkCreatedProjectionWorker_Started = 6001;

        public const int LinkCreatedProjectionWorker_UpdatingExistingLink = 6002;

        public const int LinkCreatedProjectionWorker_CreatingNewLink = 6003;

        public const int LinkCreatedProjectionWorker_Completed = 6004;

        public const int CreateActivationLinkCommand_Started = 6005;

        public const int CreateActivationLinkCommand_AggregateCreated = 6006;

        public const int CreateActivationLinkCommand_StreamStored = 6007;

        public const int CreateActivationLinkCommand_LinkGeneratedEventPublished = 6008;

        public const int CreateActivateionLinkCommand_Completed = 6009;

        public const int GetActivationLinkDataQuery_Started = 6010;

        public const int GetActivationLinkDataQuery_ActivationLinkNotFound = 6011;

        public const int GetActivationLinkDataQuery_ActivationLinkRetrieved = 6012;

        public const int GetActivationLinkDataQuery_UserNotFound = 6013;

        public const int GetActivationLinkDataQuery_Completed = 6014;
    }
}