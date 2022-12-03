using Microsoft.Data.Sqlite;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RMIMS.Client.ORM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SqlSugarClient client;
        private string _connstr = @"Data Source=F:/Richard/Project/测量管理系统/智慧屏在线实时显示/代码生成/TestSqlSugal/TestSqlSugal/bin/Debug/rmims_client/testcipher.db;Password=jsrm";
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //生成连接字符串
                var connectionString = new SqliteConnectionStringBuilder(_connstr)
                {
                    Mode = SqliteOpenMode.ReadWrite,
                    Password = "jsrm"
                }.ToString();

                //使用Sqlsugar连接Sqlite
                client = new SqlSugarClient(
                    new ConnectionConfig()
                    {
                        ConnectionString = connectionString,
                        IsAutoCloseConnection = true,
                        DbType = DbType.Sqlite,
                    });

                client.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Debug.WriteLine(sql);
                    Debug.WriteLine(client.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                };

            }
            catch (Exception ex)
            {
            }
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            client.DbFirst.IsCreateDefaultValue()
                          .CreateClassFile(@"F:\Richard\Project\测量管理系统\智慧屏在线实时显示\代码生成\TestSqlSugal\TestSqlSugal\bin\Debug\rmims_client", "Model");
            Debug.WriteLine("类生成成功");
        }
    }
}
