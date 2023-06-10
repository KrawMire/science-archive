﻿using System;

namespace ScienceArchive.Core.Repositories
{
    /// <summary>
    /// Base functionality of a repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// Get entity by ID
        /// </summary>
        /// <param name="id">ID of the entity to find</param>
        /// <returns>Entity with specified ID</returns>
        Task<TEntity> GetById(Guid id);

        /// <summary>
        /// Get all existing entities
        /// </summary>
        /// <returns>Existing entities</returns>
        Task<List<TEntity>> GetAll();

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="newValue">New entity to create</param>
        /// <returns>Created entity</returns>
        Task<TEntity> Create(TEntity newValue);

        /// <summary>
        /// Update existing entity
        /// </summary>
        /// <param name="id">ID of the entity to update</param>
        /// <param name="newValue">Entity data to update</param>
        /// <returns>Updated entity</returns>
        Task<TEntity> Update(Guid id, TEntity newValue);

        /// <summary>
        /// Delete existing entity
        /// </summary>
        /// <param name="id">ID of the entity to delete</param>
        /// <returns>Deleted entity ID</returns>
        Task<Guid> Delete(Guid id);
    }
}
