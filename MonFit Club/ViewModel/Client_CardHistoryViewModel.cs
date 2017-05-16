using MonFit_Club.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonFit_Club.ViewModel
{
    class Client_CardHistoryViewModel : INotifyPropertyChanged
    {
        static private int client_id;

        public ObservableCollection<CardHistory> CardHistories { get; set; } // collection

       static public int Client_Id { get { return client_id; } set { client_id = value; } }

        // instructor 
       public Client_CardHistoryViewModel()
       {
           //NpgsqlConnection connect = new NpgsqlConnection() { ConnectionString = DataBase.connect_params };
           string query = string.Format(@"SELECT * FROM cardHistory WHERE client_id = '{0}';", client_id.ToString());
           NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
           DataBase.connect.Open();
           try
           {
               command.ExecuteNonQuery();
               NpgsqlDataReader reader = command.ExecuteReader();
               CardHistories = new ObservableCollection<CardHistory>();
               while (reader.Read())
               {
                   CardHistories.Add( 
                   new CardHistory() { Card_Type = reader["card_type"].ToString(), Client_Id = client_id, Condition = reader["condition"].ToString(), Data_Set = reader["date_set"].ToString(), Payment = Convert.ToDouble(reader["payment"].ToString()) }
                     );
               }
               reader.Close();


           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message.ToString());
           }
           finally { DataBase.connect.Close(); }
       }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
