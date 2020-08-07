using ScientificPublications.Common.Enums;
using ScientificPublications.Common.Exceptions;
using System;
using System.Collections.Generic;

namespace ScientificPublications.Common.Helpers
{
    public static class HelperMethods
    {
        private static Dictionary<PublicationStatus, Dictionary<PublicationStatus, Role>> _nextStatesDict = new Dictionary<PublicationStatus, Dictionary<PublicationStatus, Role>>
        {
            { PublicationStatus.SUBMITED, new Dictionary<PublicationStatus, Role>
                {
                    //{ PublicationStatus.ASSIGNED, Role.Editor }, // should be done automatically, on POST workflow endpoint
                    { PublicationStatus.DENIED, Role.Editor },
                    { PublicationStatus.WITHDRAWN, Role.Author }
                }
            },
            { PublicationStatus.ASSIGNED, new Dictionary<PublicationStatus, Role>
                {
                    { PublicationStatus.IN_REVIEW, Role.Reviewer },
                    //{ PublicationStatus.SUBMITED, Role.Editor }, // should be done automatically, when all reviewers assigned to publication decline
                    { PublicationStatus.WITHDRAWN, Role.Author }
                }
            },
            { PublicationStatus.IN_REVIEW, new Dictionary<PublicationStatus, Role>
                {
                    { PublicationStatus.REVIEWED, Role.Reviewer },
                    { PublicationStatus.WITHDRAWN, Role.Author }
                }
            },
            { PublicationStatus.REVIEWED, new Dictionary<PublicationStatus, Role>
                {
                    { PublicationStatus.SHOULD_REVISE, Role.Editor },
                    { PublicationStatus.ACCEPTED, Role.Editor },
                    { PublicationStatus.DENIED, Role.Editor },
                    { PublicationStatus.WITHDRAWN, Role.Author }
                }
            },
            { PublicationStatus.SHOULD_REVISE, new Dictionary<PublicationStatus, Role>
                {
                    //{ PublicationStatus.REVISED, Role.Author }, // should be done automatically, when author uploads new version
                    { PublicationStatus.WITHDRAWN, Role.Author }
                }
            },
            { PublicationStatus.REVISED, new Dictionary<PublicationStatus, Role>
                {
                    { PublicationStatus.IN_REVIEW, Role.Editor },
                    { PublicationStatus.WITHDRAWN, Role.Author }
                }
            }
        };

        public static void CheckIsNextStateValid(PublicationStatus currentState, string nextState, string userRole)
        {
            CheckIsNextStateValid(currentState, StatusStringToEnum(nextState), RoleStringToEnum(userRole));
        }

        public static void CheckIsNextStateValid(PublicationStatus currentState, PublicationStatus nextState, Role userRole)
        {
            var nextStatesDict = _nextStatesDict.GetValueOrDefault(currentState);
            if (nextStatesDict == null)
                throw new ValidationException("Next state does not exist for current state");

            if (!nextStatesDict.TryGetValue(nextState, out Role role))
                throw new ValidationException("Next state is not valid for current state");

            if (role != userRole)
                throw new ValidationException("User does not have premission to switch state");
        }

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

        public static PublicationStatus StatusStringToEnum(string statusStr)
        {
            if (!Enum.TryParse(statusStr.ToUpper(), out PublicationStatus status))
            {
                throw new ValidationException(Constants.ExceptionMessages.InvalidValue);
            }
            return status;
        }

        public static Role RoleStringToEnum(string roleStr)
        {
            if (!Enum.TryParse(roleStr, out Role role))
            {
                throw new ValidationException(Constants.ExceptionMessages.InvalidValue);
            }
            return role;
        }
    }
}
