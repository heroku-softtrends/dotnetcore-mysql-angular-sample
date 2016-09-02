using Pomelo.Data.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Loyalty.Models;
using System.Text;
using System.Globalization;

namespace Loyalty.Repository
{
    public class UserRepository
    {
        //Login
        public List<Login> GetLogin(Login login)
        {
            List<Login> lstLogin = new List<Login>();
            try
            {
                Login _login;
                using (MySqlConnection conn = new MySqlConnection(Config.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("select UserID,FirstName,LastName from users where emailid='" + login.UserName + "' and password='" + login.Password + "'", conn);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _login = new Login();
                        _login.UserID = reader.GetString(0);
                        _login.FirstName = reader.GetString(1);
                        _login.LastName = reader.GetString(2);

                        lstLogin.Add(_login);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {

            }
            return lstLogin;
        }



        //Get database used size calculation
        public double Getdbsize()
        {
            double _dbsize = 0;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Config.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT ROUND(SUM(data_length + index_length) / 1024 / 1024, 2) AS  'Size (MB)' FROM information_schema.TABLES Where table_schema='" + Config.Database + "'", conn);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _dbsize = double.Parse(reader.GetString(0));
                    }
                    reader.Close();

                }
            }
            catch (Exception ex)
            {

            }
            return _dbsize;
        }


        //Update user details
        public void UpdateUser(Users _UsrAcc)
        {
            try
            {
                MySqlConnection conn;
                MySqlCommand cmd;
                using (conn = new MySqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    string DOB = _UsrAcc.DateofBirth.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    using (cmd = new MySqlCommand("Update users set FirstName='" + _UsrAcc.FirstName + "',LastName='" + _UsrAcc.LastName + "',Gender='" + _UsrAcc.Gender + "', DateofBirth='" + DOB + "', MobileNumber='" + _UsrAcc.MobileNumber + "',Password='" + _UsrAcc.Password + "'  where userid='" + _UsrAcc.UserID + "'", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }


        public List<Users> GetUserByMailID(string mailid)
        {
            List<Users> lstUser = new List<Users>();

            try
            {
                Users _User;
                using (MySqlConnection conn = new MySqlConnection(Config.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("select * from users where emailid='" + mailid + "'", conn);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _User = new Users();
                        _User.UserID = reader.GetString(0);
                        _User.FirstName = reader.GetString(1);
                        _User.LastName = reader.GetString(2);
                        _User.Gender = reader.GetString(3);
                        _User.DateofBirth = Convert.ToDateTime(reader.GetString(4));
                        _User.MobileNumber = reader.GetString(5);
                        _User.EmailID = reader.GetString(6);
                        _User.Password = reader.GetString(7);
                        lstUser.Add(_User);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return lstUser;
        }


        //Create User Table
        public void CreateUserTable()
        {
            try
            {
                MySqlConnection conn;
                MySqlCommand cmd;
                using (conn = new MySqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    using (cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS `users` (`UserID` varchar(50) NOT NULL,`FirstName` varchar(50) DEFAULT NULL,`LastName` varchar(50) DEFAULT NULL,`Gender` varchar(10) DEFAULT NULL,`DateofBirth` date DEFAULT NULL,`MobileNumber` varchar(50) DEFAULT NULL,`EmailID` varchar(100) DEFAULT NULL,`Password` varchar(50) NOT NULL,`CreateDate` datetime DEFAULT NULL,`LastModifiedDate` datetime DEFAULT NULL,`IsActive` bit(1) DEFAULT b'1', PRIMARY KEY(`UserID`)) ENGINE = InnoDB DEFAULT CHARSET = utf8;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        //Create Membership table
        public void CreateMembershipTable()
        {
            try
            {
                MySqlConnection conn;
                MySqlCommand cmd;
                using (conn = new MySqlConnection(Config.ConnectionString))
                {
                    conn.Open();
                    using (cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS `membership` (`MembershipID` varchar(50) NOT NULL,`UserID` varchar(50) DEFAULT NULL,`MemberID` varchar(50) DEFAULT NULL,`LoyaltyCardNo` varchar(50) DEFAULT NULL,`ExpirationDate` date DEFAULT NULL,`MemberSiteURL` varchar(100) DEFAULT NULL,`CreateDate` datetime DEFAULT NULL,`LastModifiedDate` datetime DEFAULT NULL,PRIMARY KEY (`MembershipID`),KEY `FK_Users` (`UserID`),CONSTRAINT `FK_Users` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`) ON DELETE NO ACTION ON UPDATE NO ACTION) ENGINE = InnoDB DEFAULT CHARSET = utf8;", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //Get User by User Id
        public List<Users> GetUserByID(string userid)
        {
            List<Users> lstUser = new List<Users>();
            try
            {
                Users _User;
                using (MySqlConnection conn = new MySqlConnection(Config.ConnectionString))
                {
                    MySqlCommand cmd = new MySqlCommand("select * from users where userid='" + userid + "'", conn);
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _User = new Users();
                        _User.UserID = reader.GetString(0);
                        _User.FirstName = reader.GetString(1);
                        _User.LastName = reader.GetString(2);
                        _User.Gender = reader.GetString(3);
                        _User.DateofBirth = Convert.ToDateTime(reader.GetString(4));
                        _User.MobileNumber = reader.GetString(5);
                        _User.EmailID = reader.GetString(6);
                        _User.Password = reader.GetString(7);
                        lstUser.Add(_User);
                    }
                    reader.Close();                   
                }
            }
            catch(Exception ex)
            {

            }
            return lstUser;
        }


        //Register user details
        public void Register(Users usr)
        {
            try
            {
                MySqlConnection conn;
                MySqlCommand cmd;
                using (conn = new MySqlConnection(Config.ConnectionString))
                {
                    conn.Open();

                    string UserID = GetUniqueId();

                    string DOB = usr.DateofBirth.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    using (cmd = new MySqlCommand("Insert into users (userid,firstname,lastname,gender,dateofbirth,mobilenumber,emailid,password,isactive) values ('" + UserID + "','" + usr.FirstName + "','" + usr.LastName + "','" + usr.Gender + "','" + DOB + "', '" + usr.MobileNumber + "','" + usr.EmailID + "','" + usr.Password + "',1)", conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public string GetUniqueId()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, false));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            //if (lowerCase)
            //    return builder.ToString().ToLower();
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
