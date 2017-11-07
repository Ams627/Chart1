using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.ObjectModel;

namespace Chart1
{
    class ViewModel
    {
        public ObservableCollection<int> TransactionCount { get; set; } = new ObservableCollection<int>();
        public ViewModel()
        {
            var con = new OracleConnection("Data Source=" + "raildev:1521/ukrail" + ";User Id=" + "HexSnap2017" + ";Password=" + "hexsnap2017");
            con.Open();
            var cmd = new OracleCommand("select month, count(1) from (select to_char(trans_date, 'yyyy-mm') month from transaction) group by month order by month", con);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var month = dr.GetString(0);
                var count = dr.GetInt32(1);
                System.Diagnostics.Debug.WriteLine($"month: {month} count: {count}");
                TransactionCount.Add(count);
            }
            Console.WriteLine();
        }
    }
}
