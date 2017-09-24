using FirebirdSql.Data.FirebirdClient;
using Vsb.UrgentApp.Infrastructure.Configuration;

namespace Vsb.UrgentApp.Infrastructure.Db
{
    public class FireBirdConnection
    {
        public static FbConnection Connection { get; private set; }

        public static void InitializeFirebird()
        {
            WebApiConfiguration config = WebApiConfiguration.CreateFromConfigFile();

            string connectionString =
               "User={0};" +
               "Password={1};" +
               @"Database={2};" +
               "DataSource={3};" +
               "Port={4};" +
               "Dialect={5};" +
               "Charset={6};" +
               "Role=;" +
               "Connection lifetime={7};" +
               "Pooling={8};" +
               "MinPoolSize={9};" +
               "MaxPoolSize={10};" +
               "Packet Size={11};" +
               "ServerType={12}";

            connectionString = string.Format(
                connectionString,
                config.User,
                config.Password,
                config.Database,
                config.DataSource,
                config.Port,
                config.Dialect,
                config.Charset,
                config.ConnectionLifetime,
                config.Pooling,
                config.MinPoolSize,
                config.MaxPoolSize,
                config.PacketSize,
                config.ServerType);

            FbConnection databaseConnection = new FbConnection(connectionString);
            databaseConnection.Open();
            Connection = databaseConnection;
        }
    }
}
