using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System;
using Applications.IntegranTI.com;

namespace Applications.IntegranTI.com.Models
{
    public class Companies
    {
        #region LOCAL VARIABLES

        private string strConnection;
        private string strCommand;

        private SqlConnection sqlConecction;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlDataReader;

        #endregion

        #region PROPERTIES

        public int ID_Company { get; set; }

        [Display(Name = "_Name", ResourceType = typeof(Properties.Resources))]
        [Required(ErrorMessageResourceName = "MessageRequiredField", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Name { get; set; }

        [Display(Name = "_FullName", ResourceType = typeof(Properties.Resources))]
        public string FullName { get; set; }

        #endregion

        public Companies()
        {
            ID_Company = 0;
            Name = "";
            FullName = "";
            strConnection = ConfigurationManager.ConnectionStrings["Applications_DB"].ConnectionString.ToString();
        }

        #region METHODS

        public List<Companies> SelectItems()
        {
            List<Companies> Items = new List<Companies>();

            using (sqlConecction = new SqlConnection(strConnection))
            {
                sqlConecction.Open();

                string strCommand = "SELECT * FROM Companies";

                sqlCommand = new SqlCommand(strCommand, sqlConecction);

                using (sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        Companies Item = new Companies();
                        Item.ID_Company = int.Parse(sqlDataReader["ID_Company"].ToString());
                        Item.Name = sqlDataReader["Name"].ToString();
                        Item.FullName = sqlDataReader["FullName"].ToString();

                        Items.Add(Item);
                    }
                }
            }

            return Items;
        }

        public Companies SelectItem(int ID_Company)
        {
            Companies Item = new Companies();

            using (sqlConecction = new SqlConnection(strConnection))
            {
                sqlConecction.Open();

                strCommand = "SELECT * FROM Companies ";
                strCommand += "WHERE ID_Company = " + ID_Company.ToString() + " ";

                sqlCommand = new SqlCommand(strCommand, sqlConecction);

                using (sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        Item = new Companies();
                        Item.ID_Company = int.Parse(sqlDataReader["ID_Company"].ToString());
                        Item.Name = sqlDataReader["Name"].ToString();
                        Item.FullName = sqlDataReader["FullName"].ToString();
                    }
                }
            }

            return Item;
        }

        public void InsertItem(Companies Item)
        {
            using (sqlConecction = new SqlConnection(strConnection))
            {
                sqlConecction.Open();
                Random rnd = new Random();
                strCommand = "INSERT INTO Companies VALUES (";
                strCommand += "" + rnd.Next() + ", ";
                strCommand += "'" + Item.Name + "', ";
                strCommand += "'" + Item.FullName + "') ";

                sqlCommand = new SqlCommand(strCommand, sqlConecction);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateItem(Companies Item)
        {
            using (sqlConecction = new SqlConnection(strConnection))
            {
                sqlConecction.Open();

                strCommand = "UPDATE Companies SET ";
                strCommand += "ID_Company = " + Item.ID_Company + ", ";
                strCommand += "Name = '" + Item.Name + "', ";
                strCommand += "FullName = '" + Item.FullName + "' ";
                strCommand += "WHERE ID_Company = " + Item.ID_Company;

                sqlCommand = new SqlCommand(strCommand, sqlConecction);
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteItem(Companies Item)
        {
            using (sqlConecction = new SqlConnection(strConnection))
            {
                sqlConecction.Open();

                strCommand = "DELETE Companies WHERE ";
                strCommand += "ID_Company = " + Item.ID_Company;

                sqlCommand = new SqlCommand(strCommand, sqlConecction);
                sqlCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}