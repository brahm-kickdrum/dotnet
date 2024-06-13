using Azure.Storage.Blobs.Models;
using BlobStorage.Constants;
using BlobStorage.Enums;
using BlobStorage.Exceptions;

namespace BlobStorage.Utils
{
    public static class BlobAccessLevelConverter
    {
        public static PublicAccessType ConvertToPublicAccessType(BlobAccessLevel accessLevel)
        {
            switch (accessLevel)
            {
                case BlobAccessLevel.None:
                    return PublicAccessType.None;
                case BlobAccessLevel.Blob:
                    return PublicAccessType.Blob;
                case BlobAccessLevel.Container:
                    return PublicAccessType.BlobContainer;
                default:
                    throw new BlobStorageOperationException(ErrorMessages.AccessLevelError);
            }
        }
    }
}
