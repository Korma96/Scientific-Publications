using ScientificPublications.Common.Exceptions;

namespace ScientificPublications.Common.Helpers
{
    public static class HelperMethods
    {
        public static void ThrowIfNull(dynamic obj)
        {
            if (obj == null)
            {
                throw new ValidationException(Constants.ExceptionMessages.DoesNotExist);
            }
        }

        public static void ThrowIfNotNull(dynamic obj)
        {
            if (obj != null)
            {
                throw new ValidationException(Constants.ExceptionMessages.AlreadyExists);
            }
        }
    }
}
