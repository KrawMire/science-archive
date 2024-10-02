using ScienceArchive.Core.Domain.Aggregates.News.ValueObjects;
using ScienceArchive.Core.Domain.Common;

namespace ScienceArchive.Core.Domain.Aggregates.News.Repositories;

/// <summary>
/// News repository functionality
/// </summary>
public interface INewsRepository : ICrudRepository<NewsId, News> { }