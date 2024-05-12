using ScienceArchive.Application.Interfaces;
using ScienceArchive.Core.Domain.Events;

namespace ScienceArchive.Application.Events.EventWrappers;

internal record ArticleStatusChangedEventWrapper(ArticleStatusChangedEvent Event) 
    : EventWrapper<ArticleStatusChangedEvent>(Event);