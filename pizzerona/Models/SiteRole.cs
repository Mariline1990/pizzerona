using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;using System.Data.Entity;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Helpers;

namespace pizzerona.Models
{
    public class SiteRole : RoleProvider
    {
       
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
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

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string Username)

        {
            System.Diagnostics.Debug.WriteLine($"ìììììììììììììììììììììììììììììììììììììììììììììììììììììììììììììììValore di email: {Username}");
            using (Model1 context = new Model1())
            {
                

                // Utilizzo EF per ottenere i ruoli dell'utente
                var user = context.CLIENTE.FirstOrDefault(c => c.EMAIL == Username);
                System.Diagnostics.Debug.WriteLine($"ìììììììììììììììììììììììììììììììììììììììììììììììììììììììììììììììValore di email: {Username}");


                if (user != null)
                {
                  //CLIENTE leggo la colonna RUOLO
                    
                    return new string[] { user.RUOLO };
                }
            }

            return new string[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}