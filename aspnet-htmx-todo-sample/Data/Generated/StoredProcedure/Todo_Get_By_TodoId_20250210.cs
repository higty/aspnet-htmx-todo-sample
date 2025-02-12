﻿//Generated by DbSharpApplication.
//https://github.com/higty/higlabo
using System.Data;
using System.Data.Common;
using System.Text;
using HigLabo.Core;
using HigLabo.Data;
using HigLabo.DbSharp;

namespace AspnetHtmxTodoSample.Data
{
    public partial class Todo_Get_By_TodoId_20250210 : StoredProcedureWithResultSet<Todo_Get_By_TodoId_20250210.ResultSet>
    {
        public partial class ResultSet : StoredProcedureResultSet
        {
            private Guid _TodoId;
            private String _Title = "";
            private DateTimeOffset _CreateTime;
            private Int32 _Priority;
            private DateOnly? _DueDate;

            public Guid TodoId
            {
                get
                {
                    return _TodoId;
                }
                set
                {
                    _TodoId = value;
                }
            }
            public String Title
            {
                get
                {
                    return _Title;
                }
                set
                {
                    _Title = value ?? "";
                }
            }
            public DateTimeOffset CreateTime
            {
                get
                {
                    return _CreateTime;
                }
                set
                {
                    _CreateTime = value;
                }
            }
            public Int32 Priority
            {
                get
                {
                    return _Priority;
                }
                set
                {
                    _Priority = value;
                }
            }
            public DateOnly? DueDate
            {
                get
                {
                    return _DueDate;
                }
                set
                {
                    _DueDate = value;
                }
            }

            public ResultSet()
            {
            }
            public ResultSet(ResultSet resultSet)
            {
                var r = resultSet;
                TodoId = r.TodoId;
                Title = r.Title;
                CreateTime = r.CreateTime;
                Priority = r.Priority;
                DueDate = r.DueDate;
            }
            internal ResultSet(Todo_Get_By_TodoId_20250210 storedProcedure)
            {
                this._StoredProcedureResultSet_StoredProcedure = storedProcedure;
            }

            public override String ToString()
            {
                var sb = new StringBuilder(64);
                sb.AppendLine("<Todo_Get_By_TodoId_20250210.ResultSet>");
                sb.AppendFormat("TodoId={0}", this.TodoId); sb.AppendLine();
                sb.AppendFormat("Title={0}", this.Title); sb.AppendLine();
                sb.AppendFormat("CreateTime={0}", this.CreateTime); sb.AppendLine();
                sb.AppendFormat("Priority={0}", this.Priority); sb.AppendLine();
                sb.AppendFormat("DueDate={0}", this.DueDate); sb.AppendLine();
                return sb.ToString();
            }
        }

        public const String Name = "Todo_Get_By_TodoId_20250210";
        private Guid? _TodoId;

        public String DatabaseKey
        {
            get
            {
                return ((IDatabaseKey)this).DatabaseKey;
            }
            set
            {
                ((IDatabaseKey)this).DatabaseKey = value;
            }
        }
        public Guid? TodoId
        {
            get
            {
                return _TodoId;
            }
            set
            {
                _TodoId = value;
            }
        }

        public Todo_Get_By_TodoId_20250210()
        {
            ((IDatabaseKey)this).DatabaseKey = "DefaultDatabaseKey";
            ConstructorExecuted();
        }

        public override String GetStoredProcedureName()
        {
            return Todo_Get_By_TodoId_20250210.Name;
        }
        partial void ConstructorExecuted();
        public override DbCommand CreateCommand(Database database)
        {
            var db = database;
            var cm = db.CreateCommand();
            cm.CommandType = CommandType.StoredProcedure;
            cm.CommandText = this.GetStoredProcedureName();
            
            DbParameter? p = null;
            
            p = db.CreateParameter("@TodoId", SqlDbType.UniqueIdentifier, null, null);
            p.SourceColumn = p.ParameterName;
            p.Direction = ParameterDirection.Input;
            p.Value = this.TodoId;
            p.Value = p.Value ?? DBNull.Value;
            cm.Parameters.Add(p);
            
            return cm;
        }
        protected override void SetOutputParameterValue(DbCommand command)
        {
        }
        public override Todo_Get_By_TodoId_20250210.ResultSet CreateResultSet()
        {
            return new ResultSet(this);
        }
        protected override void SetResultSet(Todo_Get_By_TodoId_20250210.ResultSet resultSet, IDataReader reader)
        {
            var r = resultSet;
            Int32 index = -1;
            try
            {
                index += 1; r.TodoId = reader.GetGuid(index);
                index += 1; r.Title = (String)reader[index];
                index += 1; r.CreateTime = (DateTimeOffset)reader[index];
                index += 1; r.Priority = reader.GetInt32(index);
                index += 1; if (reader[index] != DBNull.Value) r.DueDate = DateOnly.FromDateTime((DateTime)reader[index]);
            }
            catch (Exception ex)
            {
                throw new StoredProcedureSchemaMismatchedException(this, index, ex);
            }
        }
        public override String ToString()
        {
            var sb = new StringBuilder(32);
            sb.AppendLine("<Todo_Get_By_TodoId_20250210>");
            sb.AppendFormat("TodoId={0}", this.TodoId); sb.AppendLine();
            return sb.ToString();
        }
    }
}
