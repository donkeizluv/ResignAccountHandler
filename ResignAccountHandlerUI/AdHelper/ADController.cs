//using MyAD.Log;
using System;
using System.Collections;
using System.Data;
using System.DirectoryServices;
using System.Text;

namespace ResignAccountHandlerUI.AdHelper
{
    public class AdController : IDisposable
    {
        private DirectoryEntry oDE;
        private DirectoryEntry oDEC;
        private DataSet oDs;
        private DirectorySearcher oDS;
        private DataSet oDsUser;
        private DataRow oNewCustomersRow;
        private SearchResultCollection oResults;
        private DataRow oRwResult;
        private DataRow oRwUser;
        private DataTable oTb;
        //private ILogger _logger = LogManager.GetLogger(typeof(AdController));

        public AdController(string userName, string pwd)
        {
            sADUser = userName;
            sADPassword = pwd;
            var result = Login(userName, pwd);
            if (result != LoginResult.LOGIN_OK)
                throw new UnauthorizedAccessException("Fail to login, reason: " + result);
        }

        #region Private Variables

        private string sADPath = "LDAP://prd-vn-ad05.sgvf.sgcf";
        private readonly string sADPathPrefix = "";
        private string sADUser = string.Empty;
        private string sADPassword = string.Empty;
        private string sADServer = "prd-vn-ad05.sgvf.sgcf";
        private string sCharactersToTrim = string.Empty;

        #endregion Private Variables

        #region Enumerations

        public enum ADAccountOptions
        {
            UF_TEMP_DUPLICATE_ACCOUNT = 0x0100,
            UF_NORMAL_ACCOUNT = 0x0200,
            UF_INTERDOMAIN_TRUST_ACCOUNT = 0x0800,
            UF_WORKSTATION_TRUST_ACCOUNT = 0x1000,
            UF_SERVER_TRUST_ACCOUNT = 0x2000,
            UF_DONT_EXPIRE_PASSWD = 0x10000,
            UF_SCRIPT = 0x0001,
            UF_ACCOUNTDISABLE = 0x0002,
            UF_HOMEDIR_REQUIRED = 0x0008,
            UF_LOCKOUT = 0x0010,
            UF_PASSWD_NOTREQD = 0x0020,
            UF_PASSWD_CANT_CHANGE = 0x0040,
            UF_ACCOUNT_LOCKOUT = 0X0010,
            UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0X0080,
            UF_EXPIRE_USER_PASSWORD = 0x800000
        }

        public enum GroupType : uint
        {
            UniversalGroup = 0x08,
            DomainLocalGroup = 0x04,
            GlobalGroup = 0x02,
            SecurityGroup = 0x80000000
        }

        public enum LoginResult
        {
            LOGIN_OK = 0,
            LOGIN_USER_DOESNT_EXIST,
            LOGIN_USER_ACCOUNT_INACTIVE
        }

        #endregion Enumerations

        #region Methods

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
            }
            // Free your own state.
            // Set large fields to null.

            sADPath = null;
            sADUser = null;
            sADPassword = null;
            sADServer = null;
            sCharactersToTrim = null;

            oDE = null;
            oDEC = null;
            oDS = null;
            oResults = null;
            oDs = null;
            oDsUser = null;
            oTb = null;
            oRwUser = null;
            oRwResult = null;
            oNewCustomersRow = null;
        }

        ~AdController()
        {
            //Simply call Dispose(false).
            Dispose(false);
        }

        #region Validate Methods

        public LoginResult Login(string sUserName, string sPassword)
        {
            //Check if the Logon exists Based on the Username and Password
            if (IsUserValid(sUserName, sPassword))
            {
                oDE = GetUser(sUserName);
                if (oDE != null)
                {
                    //Check the Account Status
                    int iUserAccountControl = Convert.ToInt32(oDE.Properties["userAccountControl"][0]);
                    oDE.Close();

                    //If the Disabled Item does not Exist then the Account is Active
                    if (!IsAccountActive(iUserAccountControl))
                        return LoginResult.LOGIN_USER_ACCOUNT_INACTIVE;
                    return LoginResult.LOGIN_OK;
                }
                return LoginResult.LOGIN_USER_DOESNT_EXIST;
            }
            return LoginResult.LOGIN_USER_DOESNT_EXIST;
        }

        public bool IsAccountActive(int iUserAccountControl)
        {
            int iUserAccountControl_Disabled = Convert.ToInt32(ADAccountOptions.UF_ACCOUNTDISABLE);
            int iFlagExists = iUserAccountControl & iUserAccountControl_Disabled;

            //If a Match is Found, then the Disabled Flag Exists within the Control Flags
            if (iFlagExists > 0)
                return false;
            return true;
        }

        public bool IsAccountActive(string sUserName)
        {
            oDE = GetUser(sUserName);
            if (oDE != null)
            {
                //to check of the Disabled option exists.
                int iUserAccountControl = Convert.ToInt32(oDE.Properties["userAccountControl"][0]);
                oDE.Close();

                //Check if the Disabled Item does not Exist then the Account is Active
                if (!IsAccountActive(iUserAccountControl))
                    return false;
                return true;
            }
            return false;
        }

        public bool IsUserValid(string sUserName, string sPassword)
        {
            try
            {
                oDE = GetUser(sUserName, sPassword);
                oDE.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Validate Methods

        #region Search Methods

        public DirectoryEntry GetUserByHRCode(string hrCode)
        {
            oDE = GetDirectoryObject();
            oDS = new DirectorySearcher();
            oDS.SearchRoot = oDE;
            oDS.Filter = string.Format("(&(objectClass=user)(description=*{0}))", hrCode);
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;
            //debug
            //var allResult = oDS.FindAll();
            //_logger.Log(allResult.Count.ToString());

            var oResults = oDS.FindOne();

            if (oResults != null)
            {
                oDE = new DirectoryEntry(oResults.Path, sADUser, sADPassword, AuthenticationTypes.Secure);
                return oDE;
            }
            return null;
        }

        public DirectoryEntry GetUser(string sUserName)
        {
            //Create an Instance of the DirectoryEntry
            oDE = GetDirectoryObject();

            //Create Instance fo the Direcory Searcher
            oDS = new DirectorySearcher();

            oDS.SearchRoot = oDE;
            //Set the Search Filter
            oDS.Filter = "(&(objectClass=user)(sAMAccountName=" + sUserName + "))";
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;

            //Find the First Instance
            var oResults = oDS.FindOne();

            //If found then Return Directory Object, otherwise return Null
            if (oResults != null)
            {
                oDE = new DirectoryEntry(oResults.Path, sADUser, sADPassword, AuthenticationTypes.Secure);
                return oDE;
            }
            return null;
        }

        public DirectoryEntry GetUser(string sUserName, string sPassword)
        {
            //Create an Instance of the DirectoryEntry
            oDE = GetDirectoryObject(sUserName, sPassword);

            //Create Instance fo the Direcory Searcher
            oDS = new DirectorySearcher();
            oDS.SearchRoot = oDE;

            //Set the Search Filter
            oDS.Filter = "(&(objectClass=user)(sAMAccountName=" + sUserName + "))";
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;

            //Find the First Instance
            var oResults = oDS.FindOne();

            //If a Match is Found, Return Directory Object, Otherwise return Null
            if (oResults != null)
            {
                oDE = new DirectoryEntry(oResults.Path, sADUser, sADPassword, AuthenticationTypes.Secure);
                return oDE;
            }
            return null;
        }

        public DataSet GetUserDataSet(string sUserName)
        {
            oDE = GetDirectoryObject();

            //Create Instance fo the Direcory Searcher
            oDS = new DirectorySearcher();
            oDS.SearchRoot = oDE;

            //Set the Search Filter
            oDS.Filter = "(&(objectClass=user)(sAMAccountName=" + sUserName + "))";
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;

            //Find the First Instance
            var oResults = oDS.FindOne();

            //Create Empty User Dataset
            oDsUser = CreateUserDataSet();

            //If Record is not Null, Then Populate DataSet
            if (oResults != null)
            {
                oNewCustomersRow = oDsUser.Tables["User"].NewRow();
                oNewCustomersRow = PopulateUserDataSet(oResults, oDsUser.Tables["User"]);

                oDsUser.Tables["User"].Rows.Add(oNewCustomersRow);
            }
            oDE.Close();

            return oDsUser;
        }

        public DataSet GetUsersDataSet(string sCriteria)
        {
            oDE = GetDirectoryObject();

            //Create Instance fo the Direcory Searcher
            oDS = new DirectorySearcher();
            oDS.SearchRoot = oDE;

            //Set the Search Filter
            oDS.Filter = "(&(objectClass=user)(objectCategory=person)(" + sCriteria + "))";
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;

            //Find the First Instance
            oResults = oDS.FindAll();

            //Create Empty User Dataset
            oDsUser = CreateUserDataSet();

            //If Record is not Null, Then Populate DataSet
            try
            {
                if (oResults.Count > 0)
                    foreach (SearchResult oResult in oResults)
                        oDsUser.Tables["User"].Rows.Add(PopulateUserDataSet(oResult, oDsUser.Tables["User"]));
            }
            catch
            {
            }

            oDE.Close();
            return oDsUser;
        }

        #endregion Search Methods

        #region User Account Methods

        public void SetUserPassword(DirectoryEntry oDE, string sPassword, out string sMessage)
        {
            try
            {
                //Set The new Password
                oDE.Invoke("SetPassword", sPassword);
                sMessage = "";

                oDE.CommitChanges();
                oDE.Close();
            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
                sMessage += ex.InnerException?.Message ?? string.Empty;
            }
        }

        public void EnableUserAccount(string sUserName)
        {
            //Get the Directory Entry fot the User and Enable the Password
            EnableUserAccount(GetUser(sUserName));
        }

        public void EnableUserAccount(DirectoryEntry oDE)
        {
            oDE.Properties["userAccountControl"][0] = ADAccountOptions.UF_NORMAL_ACCOUNT;
            oDE.CommitChanges();
            oDE.Close();
        }

        public void ExpireUserPassword(DirectoryEntry oDE)
        {
            //Set the Password Last Set to 0, this will Expire the Password
            oDE.Properties["pwdLastSet"][0] = 0;
            oDE.CommitChanges();
            oDE.Close();
        }

        //public bool DisableUserAccount(string sUserName, out string erorr)
        //{
        //    return DisableUserAccount(GetUser(sUserName), out erorr);
        //}

        public bool DisableUserAccount(DirectoryEntry oDE, out string erorr)
        {
            erorr = string.Empty;
            try
            {
                //oDE.Properties["userAccountControl"][0] = ADAccountOptions.UF_NORMAL_ACCOUNT |
                //                                          ADAccountOptions.UF_DONT_EXPIRE_PASSWD |
                //                                          ADAccountOptions.UF_ACCOUNTDISABLE;
                oDE.Properties["userAccountControl"][0] = ADAccountOptions.UF_NORMAL_ACCOUNT |
                                                          ADAccountOptions.UF_ACCOUNTDISABLE;
                oDE.CommitChanges();
                oDE.Close();
                return true;
            }
            catch (Exception ex)
            {
                erorr = ex.Message;
                return false;
            }
        }

        public void MoveUserAccount(DirectoryEntry oDE, string sNewOUPath)
        {
            DirectoryEntry myNewPath = null;
            //Define the new Path
            myNewPath = new DirectoryEntry("LDAP://" + sADServer + "/" + sNewOUPath, sADUser, sADPassword,
                AuthenticationTypes.Secure);

            oDE.MoveTo(myNewPath);
            oDE.CommitChanges();
            oDE.Close();
        }

        public DateTime GetPasswordExpirationDate(DirectoryEntry oDE)
        {
            return (DateTime)oDE.InvokeGet("PasswordExpirationDate");
        }

        public bool IsAccountLocked(DirectoryEntry oDE)
        {
            return Convert.ToBoolean(oDE.InvokeGet("IsAccountLocked"));
        }

        public bool UnlockUserAccount(DirectoryEntry oDE)
        {
            SetProperty(oDE, "lockoutTime", "0", out string ex);
            return ex == string.Empty;
        }

        public bool IsUserExpired(DirectoryEntry oDE)
        {
            int iDecimalValue = int.Parse(GetProperty(oDE, "userAccountControl"));
            string sBinaryValue = Convert.ToString(iDecimalValue, 2);

            //Reverse the Binary Value to get the Location for all 1's
            var str = sBinaryValue.ToCharArray();
            Array.Reverse(str);
            string sBinaryValueReversed = new string(str);

            //24th 1 is the Switch for the Expired Account
            if (sBinaryValueReversed.Length >= 24)
            {
                if (sBinaryValueReversed.Substring(24, 1) == "1")
                    return true;
                return false;
            }
            return false;
        }

        public DirectoryEntry CreateNewUser(string sCN)
        {
            //Set the LDAP Path so that the user will be Created under the Users Container
            string LDAPDomain = "/CN=Users," + GetLDAPDomain();

            oDE = GetDirectoryObject();
            oDEC = oDE.Children.Add("CN=" + sCN, "user");
            oDE.Close();
            return oDEC;
        }

        public DirectoryEntry CreateNewUser(string sUserName, string sLDAPDomain)
        {
            //Set the LDAP qualification so that the user will be Created under the Users Container
            string LDAPDomain = "/CN=Users," + sLDAPDomain;
            oDE = new DirectoryEntry("LDAP://" + sADServer + "/" + sLDAPDomain, sADUser, sADPassword,
                AuthenticationTypes.Secure);

            oDEC = oDE.Children.Add("CN=" + sUserName, "user");
            oDE.Close();
            return oDEC;
        }

        //public bool DeleteUser(string sUserName, out string erorr)
        //{
        //    string sParentPath = GetUser(sUserName).Parent.Path;
        //    return DeleteUser(sUserName, sParentPath, out erorr);
        //}
        public bool DeleteUser2(DirectoryEntry user, out string error)
        {
            error = string.Empty;
            try
            {
                //test out DeleteTree
                oDE = user.Parent;
                oDE.Children.Remove(user);
                oDE.CommitChanges();
                oDE.Close();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool DeleteTree(DirectoryEntry user, out string error)
        {
            error = string.Empty;
            try
            {
                user.DeleteTree();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }


        }
        public bool DeleteUser(string sUserName, string sParentPath, out string error)
        {
            error = string.Empty;
            try
            {
                oDE = new DirectoryEntry(sParentPath, sADUser, sADPassword, AuthenticationTypes.Secure);

                oDE.Children.Remove(GetUser(sUserName));

                oDE.CommitChanges();
                oDE.Close();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        #endregion User Account Methods

        #region Group Methods

        public DirectoryEntry CreateNewGroup(string sOULocation, string sGroupName, string sDescription,
            GroupType oGroupTypeInput, bool bSecurityGroup)
        {
            GroupType oGroupType;

            oDE = new DirectoryEntry("LDAP://" + sADServer + "/" + sOULocation, sADUser, sADPassword,
                AuthenticationTypes.Secure);

            //Check if the Requested group is a Security Group or Distribution Group
            if (bSecurityGroup)
                oGroupType = oGroupTypeInput | GroupType.SecurityGroup;
            else
                oGroupType = oGroupTypeInput;
            int typeNum = (int)oGroupType;

            //Add Properties to the Group
            var myGroup = oDE.Children.Add("cn=" + sGroupName, "group");
            myGroup.Properties["sAMAccountName"].Add(sGroupName);
            myGroup.Properties["description"].Add(sDescription);
            myGroup.Properties["groupType"].Add(typeNum);
            myGroup.CommitChanges();

            return myGroup;
        }

        public void AddUserToGroup(string sDN, string sGroupDN)
        {
            oDE = new DirectoryEntry("LDAP://" + sADServer + "/" + sGroupDN, sADUser, sADPassword,
                AuthenticationTypes.Secure);

            //Adds the User to the Group
            oDE.Properties["member"].Add(sDN);
            oDE.CommitChanges();
            oDE.Close();
        }

        public void RemoveUserFromGroup(string sDN, string sGroupDN)
        {
            oDE = new DirectoryEntry("LDAP://" + sADServer + "/" + sGroupDN, sADUser, sADPassword,
                AuthenticationTypes.Secure);

            //Removes the User to the Group
            oDE.Properties["member"].Remove(sDN);
            oDE.CommitChanges();
            oDE.Close();
        }

        public bool IsUserGroupMember(string sDN, string sGroupDN)
        {
            oDE = new DirectoryEntry("LDAP://" + sADServer + "/" + sDN, sADUser, sADPassword, AuthenticationTypes.Secure);

            string sUserName = GetProperty(oDE, "sAMAccountName");

            var oUserGroups = GetUserGroups(sUserName);
            int iGroupsCount = oUserGroups.Count;

            if (iGroupsCount != 0)
            {
                for (int i = 0; i < iGroupsCount; i++)
                    if (sGroupDN == oUserGroups[i].ToString())
                        return true;
                return false;
            }
            return false;
        }

        public ArrayList GetUserGroups(string sUserName)
        {
            var oGroupMemberships = new ArrayList();
            return AttributeValuesMultiString("memberOf", sUserName, oGroupMemberships);
        }

        #endregion Group Methods

        #region Helper Methods

        public string GetProperty(DirectoryEntry oDE, string sPropertyName)
        {
            if (oDE.Properties.Contains(sPropertyName))
                return oDE.Properties[sPropertyName][0].ToString();
            return string.Empty;
        }

        public string GetUserName(DirectoryEntry oDE)
        {
            return oDE.Properties["sAMAccountName"].Value.ToString();
        }

        public ArrayList GetProperty_Array(DirectoryEntry oDE, string sPropertyName)
        {
            var myItems = new ArrayList();
            if (oDE.Properties.Contains(sPropertyName))
            {
                for (int i = 0; i < oDE.Properties[sPropertyName].Count; i++)
                    myItems.Add(oDE.Properties[sPropertyName][i].ToString());
                return myItems;
            }
            return myItems;
        }

        public byte[] GetProperty_Byte(DirectoryEntry oDE, string sPropertyName)
        {
            if (oDE.Properties.Contains(sPropertyName))
                return (byte[])oDE.Properties[sPropertyName].Value;
            return null;
        }

        public string GetProperty(SearchResult oSearchResult, string sPropertyName)
        {
            if (oSearchResult.Properties.Contains(sPropertyName))
                return oSearchResult.Properties[sPropertyName][0].ToString();
            return string.Empty;
        }

        public bool ClearProperty(DirectoryEntry oDE, string sPropertyName, out string ex)
        {
            ex = string.Empty;
            try
            {
                if (oDE.Properties.Contains(sPropertyName))
                {
                    oDE.Properties[sPropertyName].Clear();
                    oDE.CommitChanges();
                    oDE.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                ex = e.Message;
                return false;
            }
            return false;
        }

        public bool SetProperty(DirectoryEntry oDE, string sPropertyName, string sPropertyValue, out string ex)
        {
            //Check if the Value is Valid
            ex = string.Empty;
            if (sPropertyValue != string.Empty)
                try
                {
                    if (oDE.Properties.Contains(sPropertyName))
                    {
                        oDE.Properties[sPropertyName].Value = sPropertyValue;
                        oDE.CommitChanges();
                        oDE.Close();
                    }
                    else
                    {
                        oDE.Properties[sPropertyName].Add(sPropertyValue);
                        oDE.CommitChanges();
                        oDE.Close();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    ex = e.Message;
                    return false;
                }
            return ClearProperty(oDE, sPropertyName, out ex);
        }

        public void SetProperty(DirectoryEntry oDE, string sPropertyName, byte[] bPropertyValue)
        {
            //Clear Binary Data if Exists
            oDE.Properties[sPropertyName].Clear();

            //Update Attribute with Binary Data from File
            oDE.Properties[sPropertyName].Add(bPropertyValue);
            oDE.CommitChanges();
            oDE.Dispose();
        }

        public void SetProperty(DirectoryEntry oDE, string sPropertyName, ArrayList aPropertyValue)
        {
            //Check if the Value is Valid
            if (aPropertyValue.Count != 0)
                foreach (string sPropertyValue in aPropertyValue)
                {
                    oDE.Properties[sPropertyName].Add(sPropertyValue);
                    oDE.CommitChanges();
                    oDE.Close();
                }
        }

        public void ClearProperty(DirectoryEntry oDE, string sPropertyName)
        {
            //Check if the Property Exists
            if (oDE.Properties.Contains(sPropertyName))
            {
                oDE.Properties[sPropertyName].Clear();
                oDE.CommitChanges();
                oDE.Close();
            }
        }

        private DirectoryEntry GetDirectoryObject()
        {
            oDE = new DirectoryEntry(sADPath, sADUser, sADPassword, AuthenticationTypes.Secure);
            return oDE;
        }

        private DirectoryEntry GetDirectoryObject(string sUserName, string sPassword)
        {
            oDE = new DirectoryEntry(sADPath, sUserName, sPassword, AuthenticationTypes.Secure);
            return oDE;
        }

        private DirectoryEntry GetDirectoryObject(string sDomainReference)
        {
            oDE = new DirectoryEntry(sADPath + sDomainReference, sADUser, sADPassword, AuthenticationTypes.Secure);
            return oDE;
        }

        public DirectoryEntry GetDirectoryObject_ByPath(string sPath)
        {
            oDE = new DirectoryEntry(sADPathPrefix + sPath, sADUser, sADPassword, AuthenticationTypes.Secure);
            return oDE;
        }

        private DirectoryEntry GetDirectoryObject(string sDomainReference, string sUserName, string sPassword)
        {
            oDE = new DirectoryEntry(sADPath + sDomainReference, sUserName, sPassword, AuthenticationTypes.Secure);
            return oDE;
        }

        public string GetDistinguishedName(DirectoryEntry oDE)
        {
            if (oDE.Properties.Contains("distinguishedName"))
                return oDE.Properties["distinguishedName"][0].ToString();
            return string.Empty;
        }

        public string GetDistinguishedName(string sUserName)
        {
            oDE = GetUser(sUserName);

            if (oDE.Properties.Contains("distinguishedName"))
                return oDE.Properties["distinguishedName"][0].ToString();
            return string.Empty;
        }

        public ArrayList AttributeValuesMultiString(string sAttributeName, string sUserName, ArrayList oValuesCollection)
        {
            oDE = GetUser(sUserName);

            var oValueCollection = oDE.Properties[sAttributeName];
            var oIEn = oValueCollection.GetEnumerator();

            while (oIEn.MoveNext())
                if (oIEn.Current != null)
                    if (!oValuesCollection.Contains(oIEn.Current.ToString()))
                        oValuesCollection.Add(oIEn.Current.ToString());
            oDE.Close();
            oDE.Dispose();
            return oValuesCollection;
        }

        #endregion Helper Methods

        #region Internal Methods

        private string GetLDAPDomain()
        {
            var LDAPDomain = new StringBuilder();
            var LDAPDC = sADServer.Split('.');

            for (int i = 0; i < LDAPDC.GetUpperBound(0) + 1; i++)
            {
                LDAPDomain.Append("DC=" + LDAPDC[i]);
                if (i < LDAPDC.GetUpperBound(0))
                    LDAPDomain.Append(",");
            }
            return LDAPDomain.ToString();
        }

        private DataSet CreateUserDataSet()
        {
            oDs = new DataSet();

            oTb = oDs.Tables.Add("User");

            //Create All the Columns
            oTb.Columns.Add("company");
            oTb.Columns.Add("department");
            oTb.Columns.Add("description");
            oTb.Columns.Add("displayName");
            oTb.Columns.Add("facsimileTelephoneNumber");
            oTb.Columns.Add("givenName");
            oTb.Columns.Add("homePhone");
            oTb.Columns.Add("employeeNumber");
            oTb.Columns.Add("initials");
            oTb.Columns.Add("ipPhone");
            oTb.Columns.Add("l");
            oTb.Columns.Add("mail");
            oTb.Columns.Add("manager");
            oTb.Columns.Add("mobile");
            oTb.Columns.Add("name");
            oTb.Columns.Add("pager");
            oTb.Columns.Add("physicalDeliveryOfficeName");
            oTb.Columns.Add("postalAddress");
            oTb.Columns.Add("postalCode");
            oTb.Columns.Add("postOfficeBox");
            oTb.Columns.Add("sAMAccountName");
            oTb.Columns.Add("sn");
            oTb.Columns.Add("st");
            oTb.Columns.Add("street");
            oTb.Columns.Add("streetAddress");
            oTb.Columns.Add("telephoneNumber");
            oTb.Columns.Add("title");
            oTb.Columns.Add("userPrincipalName");
            oTb.Columns.Add("wWWHomePage");
            oTb.Columns.Add("whenCreated");
            oTb.Columns.Add("whenChanged");
            oTb.Columns.Add("distinguishedName");
            oTb.Columns.Add("info");

            return oDs;
        }

        private DataSet CreateGroupDataSet(string sTableName)
        {
            oDs = new DataSet();

            oTb = oDs.Tables.Add(sTableName);

            //Create all the Columns
            oTb.Columns.Add("distinguishedName");
            oTb.Columns.Add("name");
            oTb.Columns.Add("friendlyname");
            oTb.Columns.Add("description");
            oTb.Columns.Add("domainType");
            oTb.Columns.Add("groupType");
            oTb.Columns.Add("groupTypeDesc");

            return oDs;
        }

        private DataRow PopulateUserDataSet(SearchResult oUserSearchResult, DataTable oUserTable)
        {
            //Sets a New Empty Row
            oRwUser = oUserTable.NewRow();

            oRwUser["company"] = GetProperty(oUserSearchResult, "company");
            oRwUser["department"] = GetProperty(oUserSearchResult, "department");
            oRwUser["description"] = GetProperty(oUserSearchResult, "description");
            oRwUser["displayName"] = GetProperty(oUserSearchResult, "displayName");
            oRwUser["facsimileTelephoneNumber"] = GetProperty(oUserSearchResult, "facsimileTelephoneNumber");
            oRwUser["givenName"] = GetProperty(oUserSearchResult, "givenName");
            oRwUser["homePhone"] = GetProperty(oUserSearchResult, "homePhone");
            oRwUser["employeeNumber"] = GetProperty(oUserSearchResult, "employeeNumber");
            oRwUser["initials"] = GetProperty(oUserSearchResult, "initials");
            oRwUser["ipPhone"] = GetProperty(oUserSearchResult, "ipPhone");
            oRwUser["l"] = GetProperty(oUserSearchResult, "l");
            oRwUser["mail"] = GetProperty(oUserSearchResult, "mail");
            oRwUser["manager"] = GetProperty(oUserSearchResult, "manager");
            oRwUser["mobile"] = GetProperty(oUserSearchResult, "mobile");
            oRwUser["name"] = GetProperty(oUserSearchResult, "name");
            oRwUser["pager"] = GetProperty(oUserSearchResult, "pager");
            oRwUser["physicalDeliveryOfficeName"] = GetProperty(oUserSearchResult, "physicalDeliveryOfficeName");
            oRwUser["postalAddress"] = GetProperty(oUserSearchResult, "postalAddress");
            oRwUser["postalCode"] = GetProperty(oUserSearchResult, "postalCode");
            oRwUser["postOfficeBox"] = GetProperty(oUserSearchResult, "postOfficeBox");
            oRwUser["sAMAccountName"] = GetProperty(oUserSearchResult, "sAMAccountName");
            oRwUser["sn"] = GetProperty(oUserSearchResult, "sn");
            oRwUser["st"] = GetProperty(oUserSearchResult, "st");
            oRwUser["street"] = GetProperty(oUserSearchResult, "street");
            oRwUser["streetAddress"] = GetProperty(oUserSearchResult, "streetAddress");
            oRwUser["telephoneNumber"] = GetProperty(oUserSearchResult, "telephoneNumber");
            oRwUser["title"] = GetProperty(oUserSearchResult, "title");
            oRwUser["userPrincipalName"] = GetProperty(oUserSearchResult, "userPrincipalName");
            oRwUser["wWWHomePage"] = GetProperty(oUserSearchResult, "wWWHomePage");
            oRwUser["whenCreated"] = GetProperty(oUserSearchResult, "whenCreated");
            oRwUser["whenChanged"] = GetProperty(oUserSearchResult, "whenChanged");
            oRwUser["distinguishedName"] = GetProperty(oUserSearchResult, "distinguishedName");
            oRwUser["info"] = GetProperty(oUserSearchResult, "info");

            return oRwUser;
        }

        private DataRow PopulateGroupDataSet(SearchResult oSearchResult, DataTable oTable)
        {
            //Sets a New Empty Row
            oRwResult = oTable.NewRow();

            string sFullOU = GetProperty(oSearchResult, "distinguishedName");
            var splita = sCharactersToTrim.Split(';');
            foreach (string sa in splita)
                sFullOU = sFullOU.Replace(sa, "");

            string sDisplayName = "";
            string sRawString = "";
            var split1 = sFullOU.Split(',');
            foreach (string s1 in split1)
            {
                sRawString = s1;
                sRawString = sRawString.Replace("OU=", "");
                sRawString = sRawString.Replace("DC=", "");
                sRawString = sRawString.Replace("CN=", "");
                sDisplayName = sRawString + "/" + sDisplayName;
            }

            oRwResult["distinguishedName"] = GetProperty(oSearchResult, "distinguishedName");
            oRwResult["name"] = GetProperty(oSearchResult, "name");
            oRwResult["friendlyname"] = sDisplayName.Substring(0, sDisplayName.Length - 1);
            ;
            oRwResult["description"] = GetProperty(oSearchResult, "description");
            oRwResult["domainType"] = sADServer;

            string sGroupType = GetProperty(oSearchResult, "groupType");
            oRwResult["groupType"] = sGroupType;

            switch (sGroupType)
            {
                case "2":
                    oRwResult["groupTypeDesc"] = "Global, Distribution";
                    break;

                case "4":
                    oRwResult["groupTypeDesc"] = "Domain, Distribution";
                    break;

                case "8":
                    oRwResult["groupTypeDesc"] = "Universal, Distribution";
                    break;

                case "-2147483640":
                    oRwResult["groupTypeDesc"] = "Universal, Security";
                    break;

                case "-2147483646":
                    oRwResult["groupTypeDesc"] = "Global, Security";
                    break;

                case "-2147483644":
                    oRwResult["groupTypeDesc"] = "Domain, Security";
                    break;

                default:
                    oRwResult["groupTypeDesc"] = "";
                    break;
            }

            return oRwResult;
        }

        #endregion Internal Methods

        #endregion Methods
    }
}