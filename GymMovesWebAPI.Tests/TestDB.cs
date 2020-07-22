using GymMovesWebAPI.Data.DatabaseContexts.MainDatabaseContext;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymMovesWebAPI.Tests
{
    public abstract class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly MainDatabaseContext DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<MainDatabaseContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new MainDatabaseContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }

    }
}
