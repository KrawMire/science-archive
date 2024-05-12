using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;

namespace ScienceArchive.Application.Events.EventWrappers;

internal record UserRegisteredEventWrapper(UserRegisteredEvent Event) 
    : EventWrapper<UserRegisteredEvent>(Event);