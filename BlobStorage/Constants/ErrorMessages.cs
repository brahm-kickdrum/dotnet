namespace BlobStorage.Constants
{
    public static class ErrorMessages
    {
        public const string ContainerCreationError = "An error occurred while creating container '{0}'.";
        public const string AccessPolicyError = "An error occurred while setting access policy for container '{0}'.";
        public const string UploadFileError = "An error occurred while uploading file to container '{0}'.";
        public const string ListBlobsError = "Failed to list blobs in container '{0}'.";
        public const string DownloadBlobError = "Failed to download blob.";
        public const string ContainerDeletionError = "An error occurred while deleting container '{0}'.";
        public const string BlobDeletionError = "An error occurred while deleting blob '{0}'.";
        public const string RetrieveSecretError = "Oops! It looks like something went wrong while trying to connect.";
        public const string InvalidFilePathError = "Invalid local file path: {0}.";
        public const string FileNotFoundError = "File not found at {0}.";
        public const string BlobClientError = "An error occurred while getting BlobClient for file '{0}' in container '{1}'.";
        public const string ContainerClientError = "An error occurred while getting BlobContainerClient for container '{0}'.";
        public const string ServerError = "Server error";
        public const string ContainerNameLengthError = "Container name must be between 3 and 63 characters long.";
        public const string ContainerNameError = "Container name must only contain lowercase letters, numbers and hyphens and must begin with a letter or number. Each hyphen must be preceded and followed by a non-hyphen character.";
        public const string SecretNotFoundError = "Secret not found.";
        public const string EnvironmentVariableError = "Please set the KEY_VAULT_NAME environment variable.";
        public const string AccessLevelError = "Invalid access level specified.";
    }
}
