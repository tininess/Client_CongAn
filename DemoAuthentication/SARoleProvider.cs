using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using DemoAuthentication.Models;
using DemoAuthentication.ServiceReference1;
using System.Runtime.Serialization;
namespace DemoAuthentication
{
    public class SARoleProvider : RoleProvider
    {
     
        Level2ServiceClient proxy = new Level2ServiceClient();
        public override bool IsUserInRole(string username, string roleName)
        {
            return true;
        }
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        //public class RoleViewModel
        //{
        //    public string RoleName { get; set; }
        //}
       
        public override string[] GetRolesForUser(string username)
        {
            
            int rolesCount = 0;
         
            IList<string> rolesNames;

            try
            {
                // get roles for this user from DB...
                rolesNames = proxy.GetRolesForUser(username);
                rolesCount = rolesNames.Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            string[] roles = new string[rolesCount];
            int counter = 0;
            foreach (var item in rolesNames)
            {
                roles[counter] = item.ToString();
                counter++;
            }

            return roles;

        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();

        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();

        }

    }
}