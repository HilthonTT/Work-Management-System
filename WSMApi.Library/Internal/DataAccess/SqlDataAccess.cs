﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace WSMApi.Library.Internal.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    private readonly ILogger<SqlDataAccess> _logger;
    private bool IsClosed;

    public SqlDataAccess(IConfiguration config,
                      ILogger<SqlDataAccess> logger)
    {
        _config = config;
        _logger = logger;
    }

    public string GetConnectionString(string name)
    {
        return _config.GetConnectionString(name);
    }

    public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        List<T> rows = connection.Query<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure).ToList();

        return rows;
    }

    public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
    {
        string connectionString = GetConnectionString(connectionStringName);

        using IDbConnection connection = new SqlConnection(connectionString);
        connection.Execute(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    private IDbConnection _connection;
    private IDbTransaction _transaction;

    public void StartTransaction(string connectionStringName)
    {
        string connectionString = _config.GetConnectionString(connectionStringName);

        _connection = new SqlConnection(connectionString);
        _connection.Open();

        _transaction = _connection.BeginTransaction();

        IsClosed = false;
    }

    public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
    {
        List<T> rows = _connection.Query<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

        return rows;
    }

    public void CommitTransaction()
    {
        _transaction?.Commit();
        _connection?.Close();

        IsClosed = true;
    }

    public void RollbackTransaction()
    {
        _transaction?.Rollback();
        _connection?.Close();

        IsClosed = true;
    }

    public void Dispose()
    {
        if (IsClosed == false)
        {
            try
            {
                CommitTransaction();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Commit transaction has failed in the dispose method.");
            }
        }

        _transaction = null;
        _connection = null;
    }

}
