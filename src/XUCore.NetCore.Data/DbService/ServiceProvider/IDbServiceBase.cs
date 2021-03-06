﻿using XUCore.Paging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace XUCore.NetCore.Data.DbService.ServiceProvider
{

    /// <summary>
    /// 数据领域层接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDbServiceBase<TEntity> : IDisposable where TEntity : class, new()
    {
        /// <summary>
        /// 只读对象
        /// </summary>
        IBaseRepository<TEntity> Read { get; }
        /// <summary>
        /// 只写对象
        /// </summary>
        IBaseRepository<TEntity> Write { get; }
        /// <summary>
        /// 当前DbSet对象
        /// </summary>
        DbSet<TEntity> Table { get; }

        //同步操作

        /// <summary>
        /// 同步提交
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <returns></returns>
        int Add(TEntity entity, bool isSaveChange = true);
        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <returns></returns>
        int Add(IEnumerable<TEntity> entities, bool isSaveChange = true);
        /// <summary>
        /// 更新一条数据（全量更新）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <returns></returns>
        int Update(TEntity entity, bool isSaveChange = true);
        /// <summary>
        /// 批量更新数据（全量更新）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <returns></returns>
        int Update(IEnumerable<TEntity> entities, bool isSaveChange = true);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <returns></returns>
        int Delete(TEntity entity, bool isSaveChange = true);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <returns></returns>
        int Delete(IEnumerable<TEntity> entities, bool isSaveChange = true);

        //异步操作

        /// <summary>
        /// 异步提交
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        /// <summary>
        /// 异步插入一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> AddAsync(TEntity entity, bool isSaveChange = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量写入数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> AddAsync(IEnumerable<TEntity> entities, bool isSaveChange = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// 更新一条数据（全量更新）
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity, bool isSaveChange = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量更新数据（全量更新）
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<TEntity> entities, bool isSaveChange = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity, bool isSaveChange = true, CancellationToken cancellationToken = default);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSaveChange">是否提交</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<TEntity> entities, bool isSaveChange = true, CancellationToken cancellationToken = default);

        //同步查询

        /// <summary>
        /// 根据主键获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderby">exp:“Id desc,CreateTime desc”</param>
        /// <returns></returns>
        TEntity GetSingle(Expression<Func<TEntity, bool>> selector = null, string orderby = "");
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderby">exp:“Id desc,CreateTime desc”</param>
        /// <param name="skip">起始位置（默认为-1，不设置 一般从0开始）</param>
        /// <param name="limit">记录数（默认为0，不设置）</param>
        /// <returns></returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> selector = null, string orderby = "", int skip = -1, int limit = 0);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderby">exp:“Id desc,CreateTime desc”</param>
        /// <param name="pageNumber">页码（最小为1）</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        PagedModel<TEntity> GetPagedList(Expression<Func<TEntity, bool>> selector = null, string orderby = "", int pageNumber = 1, int pageSize = 10);
        /// <summary>
        /// Any数据检测
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        bool Any(Expression<Func<TEntity, bool>> selector = null);
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        long GetCount(Expression<Func<TEntity, bool>> selector = null);

        //异步查询

        /// <summary>
        /// 根据主键获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderby">exp:“Id desc,CreateTime desc”</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> selector = null, string orderby = "", CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderby">exp:“Id desc,CreateTime desc”</param>
        /// <param name="skip">起始位置（默认为-1，不设置 一般从0开始）</param>
        /// <param name="limit">记录数（默认为0，不设置）</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> selector = null, string orderby = "", int skip = -1, int limit = 0, CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="orderby">exp:“Id desc,CreateTime desc”</param>
        /// <param name="pageNumber">页码（最小为1）</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<PagedModel<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> selector = null, string orderby = "", int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
        /// <summary>
        /// Any数据检测
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default);
        /// <summary>
        /// 获取记录数
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> selector = null, CancellationToken cancellationToken = default);

        #region 增加bulkextensions拓展

        //同步操作

        /// <summary>
        /// 根据条件批量更新（部分字段）
        /// </summary>
        /// <param name="selector">查询条件</param>
        /// <param name="updateValues">更新的新数据数据</param>
        /// <param name="updateColumns">指定字段，如果需要更新为默认数据，那么需要指定字段，因为在内部实现会排除掉没有赋值的默认字段数据</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> selector, TEntity updateValues, List<string> updateColumns = null);
        /// <summary>
        /// 根据条件批量更新（部分字段）
        /// </summary>
        /// <param name="selector">查询条件</param>
        /// <param name="Update">更新的新数据数据</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TEntity>> Update);
        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        int Delete(Expression<Func<TEntity, bool>> selector);

        //异步操作

        /// <summary>
        /// 根据条件批量更新（部分字段）
        /// </summary>
        /// <param name="selector">查询条件</param>
        /// <param name="updateValues">更新的新数据数据</param>
        /// <param name="updateColumns">指定字段，如果需要更新为默认数据，那么需要指定字段，因为在内部实现会排除掉没有赋值的默认字段数据</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> selector, TEntity updateValues, List<string> updateColumns = null, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件批量更新（部分字段）
        /// </summary>
        /// <param name="selector">查询条件</param>
        /// <param name="Update">更新的新数据数据</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(Expression<Func<TEntity, bool>> selector, Expression<Func<TEntity, TEntity>> Update, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <param name="selector"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> selector, CancellationToken cancellationToken = default);

        #endregion

        #region adonet

        /// <summary>
        /// 通过EF执行原生SQL 返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSql(string sql, params IDataParameter[] parameters);
        /// <summary>
        /// 通过ADO.NET通过EF执行原生SQL 返回影响行数 返回查询结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T Select<T>(string sql, CommandType type, params IDataParameter[] parameters) where T : class, new();
        /// <summary>
        /// 通过ADO.NET通过EF执行原生SQL 返回影响行数 返回查询结果集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<T> SelectList<T>(string sql, CommandType type, params IDataParameter[] parameters) where T : class, new();
        /// <summary>
        /// 通过ADO.NET通过EF执行原生SQL 返回影响行数 返回查询结果集合(DataTable)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable SelectList(string sql, CommandType type, params IDataParameter[] parameters);
        /// <summary>
        /// 通过ADO.NET通过EF执行原生SQL 返回影响行数返回数据集(DataSet);
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet SelectDataSet(string sql, CommandType type, params IDataParameter[] parameters);
        /// <summary>
        /// 通过原生执行ADONET查询操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        int ExecuteAdoNet(string sql, CommandType type, params IDataParameter[] parameters);

        #endregion
    }
}
