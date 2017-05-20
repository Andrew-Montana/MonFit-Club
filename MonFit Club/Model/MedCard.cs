using MonFit_Club.Command;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonFit_Club.Models
{
    class MedCard : INotifyPropertyChanged
    {
        private int id;
        private int client_id;
        private string weight;
        private string recommend;
        private string height;
        private string problems;
        private string bodytype;

        public int Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public int Client_Id { get { return client_id; } set { client_id = value; OnPropertyChanged("Client_Id"); } }
        public string Weight { get { return weight; } set { weight = value; OnPropertyChanged("Weight"); } }
        public string Recommend { get { return recommend; } set { recommend = value; OnPropertyChanged("Recommend"); } }
        public string Height { get { return height; } set { height = value; OnPropertyChanged("Height"); } }
        public string Problems { get { return problems; } set { problems = value; OnPropertyChanged("Problems"); } }
        public string BodyType { get { return bodytype; } set { bodytype = value; OnPropertyChanged("BodyType"); } }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private RelayCommand updateDataCommand;
        public RelayCommand UpdateDataCommand
        {
            get
            {
                return updateDataCommand ??
                    (updateDataCommand = new RelayCommand(obj =>
                    {
                        if (Convert.ToString(Client_Id) != null && Convert.ToString(Weight) != null && Convert.ToString(Height) != null && Problems != null && Recommend != null && BodyType != null)
                        { UpdateDataToDB(Client_Id, Weight, Height, Problems, Recommend, BodyType, Id); }
                        else
                        {
                            MessageBox.Show("Заполнены не все поля", "Ошибка!");
                        }
                    }));
            }
        }

        private void UpdateDataToDB(int p1, string p2, string p3, string p4, string p5, string p6, int p7)
        {
            var _width = p2.ToString();
            var _height = p3.ToString();
            if (_width.Contains(','))
            {
                _width.Replace(',', '.');
                _width = _width.Replace(',', '.');
            }
            if (_height.Contains(','))
            {
                _height.Replace(',', '.');
                _height = _width.Replace(',', '.');
            }
            
            string query = string.Format(@"UPDATE medCard SET weight = {0}, height = {1}, problems = '{2}', recommend = '{3}', bodytype = '{4}' WHERE client_id = {5} AND id = {6};", _width, _height, p4, p5, p6, p1, p7);
            NpgsqlCommand command = new NpgsqlCommand(query, DataBase.connect);
            DataBase.connect.Open();
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Мед.Карта успешно обновлена", "Сообщение");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            finally { DataBase.connect.Close(); }
        }

    }
}
