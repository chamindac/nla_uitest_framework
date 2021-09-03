using NLA.Tests.UI.POCOs;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NLA.Tests.UI.Utils
{
    class DBHelper
    {
        private readonly IConfiguration _configs;
        public DBHelper(IConfiguration configs)
        {
            _configs = configs;
        }

        public GeneratedTestDataKeys RunDataSetup(string dataCreateScriptFileName)
        {
            GeneratedTestDataKeys generatedTestDataKeys = new GeneratedTestDataKeys();
            string dataCreateScript = File.ReadAllText(string.Concat("DataScripts/", dataCreateScriptFileName));
            
            ServerConnection serverCon = new ServerConnection(_configs["TenantDB:ServerName"],
                _configs["TenantDB:UserName"],
                _configs["TenantDB:Password"]);
            serverCon.DatabaseName = _configs["TenantDB:Database"];
            Server server = new Server(serverCon);
            var reader = server.ConnectionContext.ExecuteReader(dataCreateScript);
            reader.Read();
            for (int i=0;i<reader.FieldCount;i++)
            {
                generatedTestDataKeys.Add(new KeyValuePair<string, object>(reader.GetName(i), reader.GetSqlValue(i)));   
            }
            return generatedTestDataKeys;
        }

        public void RunDataCleanup(string dataCleanupScriptFileName, string testDataKey)
        {
            GeneratedTestDataKeys generatedTestDataKeys = new GeneratedTestDataKeys();
            string dataCleanScript = File.ReadAllText(string.Concat("DataScripts/", dataCleanupScriptFileName));
            dataCleanScript = dataCleanScript.Replace("{{TestDataKey}}", testDataKey);

            ServerConnection serverCon = new ServerConnection(_configs["TenantDB:ServerName"],
                _configs["TenantDB:UserName"],
                _configs["TenantDB:Password"]);
            serverCon.DatabaseName = _configs["TenantDB:Database"];
            Server server = new Server(serverCon);
            server.ConnectionContext.ExecuteNonQuery(dataCleanScript);
        }
    }
}
