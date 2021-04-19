using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Labixa.HashMD
{
    public class HashMD5
    {
        public static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private string SERVER_IP = System.Configuration.ConfigurationManager.AppSettings["server_ip"];
        private string API_KEY = System.Configuration.ConfigurationManager.AppSettings["api_key"];
        private string API_PWD = System.Configuration.ConfigurationManager.AppSettings["api_password"];
        private string SYSTEM_EVO = System.Configuration.ConfigurationManager.AppSettings["system_evo"];
        private string SYSTEM_EZUGI = System.Configuration.ConfigurationManager.AppSettings["system_ezugi"];
        private string CURRENCY = System.Configuration.ConfigurationManager.AppSettings["currency"];
        private string LANGUAGE = System.Configuration.ConfigurationManager.AppSettings["language"];
        private string URL_API_FUNDIST = System.Configuration.ConfigurationManager.AppSettings["language"];

        public string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        #region dùng để hash ngoài view class static
        public static string MD5HashView(string input)
        {
            byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            var value = BitConverter.ToUInt32(encoded, 0) % 1000000;
            return value.ToString();
        }
        #endregion
        #region hash create new user
        /// <summary>
        /// Hash of create User
        /// User/Add/[IP]/[TID]/[KEY]/[LOGIN]/[PASSWORD]/[CURRENCY]/[PWD]
        /// </summary>
        /// <param name="login">UserName</param>
        /// <param name="Password"></param>
        /// <param name="TID">TimeStampValue</param>
        /// <returns></returns>
        public string Fundist_Hash(string login, string Password, string TID)
        {
            log.Info("User/Add/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password + "/" + CURRENCY + "/" + API_PWD);
            string str = "User/Add/"+SERVER_IP+"/"+TID+ "/"+API_KEY+"/"+login+"/"+Password+"/"+CURRENCY+"/"+API_PWD;
            //string str = "User/Add/14.161.36.53/96778c3a/96778c3a2b606891cf5f9da390cb9d71/longt/123456789/USD/6895168756832161";
            return MD5Hash(str);
        }
        #endregion
        #region hash update user info
        public string HashUpdateUserInfo(string login, string Password, string TID)
        {
            log.Info("User/Update/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password + "/" + CURRENCY + "/" + API_PWD);
            string str = "User/Update/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password + "/" + CURRENCY + "/" + API_PWD;
            //string str = "User/Add/14.161.36.53/96778c3a/96778c3a2b606891cf5f9da390cb9d71/longt/123456789/USD/6895168756832161";
            return MD5Hash(str);
        }
        #endregion
        /// <summary>
        /// Set Balance User
        /// Balance/Set/[CASINO_SERVER_IP]/[TID]/[KEY]/[SYSTEM]/[AMOUNT]/[LOGIN]/[CURRENCY]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <param name="amount"></param>
        /// <param name="system">System ID of target Merchant</param>
        /// <returns></returns>
        public string Hash_SetBalanceUser(string login, string TID, string amount, string system)
        {
            log.Info("Balance/Set/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + system + "/" + amount + "/" + login + "/" + CURRENCY + "/" + API_PWD);
            string str = "Balance/Set/"+SERVER_IP+"/"+TID+"/"+API_KEY+"/"+system+"/"+amount+"/"+login+"/"+CURRENCY+"/"+ API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// get balance của system id
        /// Balance/Get/[CASINO_SERVER_IP]/[TID]/[KEY]/[SYSTEM]/[LOGIN]/[PWD] 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <param name="amount"></param>
        /// <param name="system">System ID of target Merchant</param>
        /// <returns></returns>
        public string Hash_getbalance(string login, string TID, string system)
        {
            log.Info("Balance/Get/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + system + "/" + login + "/" + API_PWD);
            string str = "Balance/Get/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + system + "/" + login + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Balance/Withdraw/[CASINO_SERVER_IP]/[TID]/[KEY]/[SYSTEM]/[LOGIN]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public string Hash_getWidthDraw(string login, string TID, string system)
        {
            log.Info("Balance/Withdraw/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + system + "/" + login + "/" + API_PWD);
            string str = "Balance/Withdraw/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + system + "/" + login + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash DirectAuth
        /// User/DirectAuth/[CASINO_SERVER_IP]/[TID]/[KEY]/[LOGIN]/[PASSWORD]/[SYSTEM]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="Password"></param>
        /// <param name="TID"></param>
        /// <param name="system">System ID of target Merchant</param>
        /// <returns></returns>
        public string Hash_setbalance(string login, string Password, string TID, string system)
        {
            log.Info("User/DirectAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password + "/" + system + "/" + API_PWD);
            string str = "User/DirectAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password+ "/"+ system + "/"+API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash AuthHTML
        /// User/AuthHTML/[CASINO_SERVER_IP]/[TID]/[KEY]/[LOGIN]/[PASSWORD]/[SYSTEM]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="Password"></param>
        /// <param name="TID"></param>
        /// <param name="system">System ID of target Merchant</param>
        /// <returns></returns>
        public string Hash_HashAuthHTML(string login, string Password, string TID, string system)
        {
            log.Info("User/AuthHTML/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password + "/" + system + "/" + API_PWD);
            string str = "User/AuthHTML/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + Password + "/" + system + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash Kill Auth
        /// User/KillAuth/[CASINO_SERVER_IP]/[TID]/[KEY]/[LOGIN]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <returns></returns>
        public string Hash_HashKillAuth(string login, string TID)
        {
            log.Info("User/KillAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD);
            string str = "User/KillAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash Universal Bets Summary
        /// Stats/BetsSummary/[CASINO_SERVER_IP]/[TID]/[KEY]/[DATE]/[PWD] 
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string Hash_HashUniversalBetsAuth(string TID, string date)
        {
            log.Info("Stats/BetsSummary/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + date + "/" + API_PWD);
            string str = "Stats/BetsSummary/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + date + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash Stats Bets
        /// Stats/Bets/[CASINO_SERVER_IP]/[TID]/ /[KEY]/[DATE]/[PWD] 
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string Hash_HashStatsBetssAuth(string TID, string date)
        {
            log.Info("Stats/Bets/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + date + "/" + API_PWD);
            string str = "Stats/Bets/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + date + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash Detail Stats
        /// Stats/Bets/[CASINO_SERVER_IP]/[TID]/[KEY]/[DATE]/[PWD]
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string Hash_HashDetailStatssAuth(string TID, string date)
        {
            log.Info("Stats/Detailed/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + date + "/" + API_PWD);
            string str = "Stats/Detailed/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + date + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash Game Detail
        /// Stats/GameDetails/[CASINO_SERVER_IP]/[TID]/ /[KEY]/[GAMEID]/[PWD]
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string Hash_HashGameDetail(string TID,string gameID)
        {
            log.Info("Stats/GameDetails/" + SERVER_IP + "/" + TID + "/ /" + API_KEY + "/" + gameID + "/" + API_PWD);
            string str = "Stats/GameDetails/" + SERVER_IP + "/" + TID + "/ /" + API_KEY + "/" + gameID + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash enable user
        /// User/Enable/[CASINO_SERVER_IP]/[TID]/[KEY]/[LOGIN]/[PWD]
        /// </summary>
        /// <param name="TID"></param>
        /// <param name="login"></param>
        /// <returns></returns>
        public string HashEnableUser(string TID, string login)
        {
            log.Info("User/Enable/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD);
            string str = "User/Enable/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Direct_Authorization
        /// User/DirectAuth/[CASINO_SERVER_IP]/[TID]/[KEY]/[LOGIN]/[PASSWORD]/[SYSTEM]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <param name="password"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public string Hash_Direct_Authorization(string login, string TID, string password, string system)
        {
            log.Info("User/DirectAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + password + "/" + system + "/" + API_PWD);
            string str = "User/DirectAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login+"/"+password+"/"+ system+ "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash_Kill_Authorization
        /// User/KillAuth/[CASINO_SERVER_IP]/[TID]/[KEY]/[LOGIN]/[PWD]
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <returns></returns>
        public string Hash_Kill_Authorization(string login, string TID)
        {
            log.Info("User/KillAuth/" + SERVER_IP + "/" + TID + "/ /" + API_KEY + "/" + login + "/" + API_PWD);
            string str = "User/KillAuth/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// HashAuthorization_with_HTML
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <param name="password"></param>
        /// <param name="system"></param>
        /// <returns></returns>
        public string HashAuthorization_with_HTML(string login, string TID, string password, string system)
        {
            log.Info("User/AuthHTML/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + password + "/" + system + "/" + API_PWD);
            string str = "User/AuthHTML/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login+"/"+password+"/"+system + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash api get game full list fundits
        /// </summary>
        /// <param name="TID"></param>
        /// <returns></returns>
        public string HashGameFullList(string TID)
        {
            log.Info("Game/FullList/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD);
            string str = "Game/FullList/" + SERVER_IP + "/" + TID + "/"+ API_KEY+"/"+ API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Hash thanh toan khach hang
        /// </summary>
        /// <param name="system">998/993</param>
        /// <param name="amount"></param>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <returns></returns>
        public string HashLoyaltypayments(string system, string amount, string login, string TID)
        {
            log.Info("Loyalty/Payment" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + system + "/" + amount + "/" + login + "/" + API_PWD);
            string str = "Loyalty/Payment" + SERVER_IP + "/" + TID + "/" + API_KEY+"/"+system+"/"+amount+"/"+login + "/" + API_PWD;
            return MD5Hash(str);
        }
        /// <summary>
        /// Chuyen khoan (Bonus Select)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="TID"></param>
        /// <returns></returns>
        public string HashBonusSelect_BonusCancel(string login, string TID)
        {
            log.Info("WLCInfo/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD);
            string str = "WLCInfo/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + login + "/" + API_PWD;
            return MD5Hash(str);
        }
        public string HashGameCategories(string TID)
        {
            log.Info("Game/Categories/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD);
            string str = "Game/Categories/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD;
            return MD5Hash(str);
        }
        public string HashGameAvailable(string TID)
        {
            log.Info("Game/List/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD);
            string str = "Game/List/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD;
            return MD5Hash(str);
        }
        public string HashLobbyState(string TID)
        {
            log.Info("Tables/LobbyState/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD);
            string str = "Tables/LobbyState/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD;
            return MD5Hash(str);
        }
        public string HashTableInfo(string TID)
        {
            log.Info("Providers/TablesInfo/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD);
            string str = "Providers/TablesInfo/" + SERVER_IP + "/" + TID + "/" + API_KEY + "/" + API_PWD;
            return MD5Hash(str);
        }
    }
}