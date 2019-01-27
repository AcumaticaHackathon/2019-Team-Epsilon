using System;
using PX.Objects;
using PX.Data;
using PX.SM;
using System.Collections;
using System.Collections.Generic;
using PX.Data.Maintenance.GI;
using PX.Data.Access.ActiveDirectory;
using System.Linq;
using PX.Objects.EP;

namespace DynamicRoles
{
    public class RoleAccess_Extension : PXGraphExtension<RoleAccess>
    {
        public PXSelect<ESDynamicRole, Where<ESDynamicRole.rolename, Equal<Current<Roles.rolename>>>> DynamicAccess;

        public PXAction<Roles> eSResetDynamicAccess;


        public PXGenericInqGrph GetGIGraph(Guid gi)
        {
            GIDesign giDesign = PXSelect<GIDesign, Where<GIDesign.designID,
                Equal<Required<GIDesign.designID>>>>.Select(Base, new object[] { gi });
            return PXGenericInqGrph.CreateInstance(giDesign.DesignID.Value.ToString(), giDesign.Name,
                parameters: new Dictionary<string, string>());
        }

        [PXButton]
        [PXUIField(DisplayName = "Reset Dynamic Access")]
        public virtual IEnumerable ESResetDynamicAccess(PXAdapter adapter)
        {
            List<Users> users = new List<Users>();

            foreach (ESDynamicRole dynamicRole in DynamicAccess.Select())
            {
                var graph = GetGIGraph(dynamicRole.DesignID.Value);
                var resultList = graph.Results.Select().FirstTableItems.ToList();
                foreach (GenericResult genericResult in resultList)
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string>();
                    var results = genericResult.Values;
                    foreach (KeyValuePair<string, object> res in results)
                    {
                        object o = res.Value;
                        if (o.GetType() == typeof(Users))
                        {
                            var user = o as Users;
                            if (!users.Exists(x => string.Equals(x.Username, user.Username)))
                            {
                                users.Add(o as Users);
                            }
                        }
                    }
                }
            }

            this.ResetAccessForUsers(users);

            return adapter.Get();
        }

        private void ResetAccessForUsers(List<Users> users)
        {
            List<UsersInRoles> usersInRoleList = Base.UsersByRole.Select().GetAsList();

            //add users which exist in the new list but do not exist in the current list on screen
            List<Users> usersToAdd =
                users.Where(u => !usersInRoleList
                    .Any(r => string.Equals(r.Username, u.Username))).ToList();

            //remove users which exist on screen but do not exist in the new list

            List<UsersInRoles> usersToRemove =
                usersInRoleList.Where(u => 
                u.GetExtension<UsersInRolesExt>().UsrESDirectAssigned != true
                && !users.Any(r => string.Equals(r.Username, u.Username))).ToList();

            foreach (UsersInRoles user in usersToRemove)
            {
                Base.UsersByRole.Delete(user);
            }


            var usersFromDB = PXSelectJoin<Users,
            LeftJoin<EPLoginType,
                On<EPLoginType.loginTypeID, Equal<Users.loginTypeID>>,
            LeftJoin<EPLoginTypeAllowsRole,
                On<EPLoginTypeAllowsRole.loginTypeID, Equal<EPLoginType.loginTypeID>>>>,
                                Where2<Where2<Where<Users.isHidden, Equal<False>>,
                                And<Where2<Where<Users.source, Equal<PXUsersSourceListAttribute.application>, Or<Users.overrideADRoles, Equal<True>>>,
                                And<Where<Required<Roles.guest>, Equal<True>, Or<Users.guest, NotEqual<True>>>>>>>,
                                And<Where<EPLoginTypeAllowsRole.rolename, Equal<Required<UsersInRoles.rolename>>,
                                Or<Users.loginTypeID, IsNull>>>>>.Select(Base, new object[] { Base.Roles.Current.Guest,
                                    Base.Roles.Current.Rolename})
                                  .GetAsList()
                          .ToDictionary(x => x.Username);


            List<Users> newUsrsToAdd = new List<Users>();
            foreach (Users user in usersToAdd)
            {
                if (usersFromDB.ContainsKey(user.Username))
                    newUsrsToAdd.Add(user);
            }


            foreach (Users user in newUsrsToAdd)
            {
                UsersInRoles newUser = Base.UsersByRole.Insert();
                Base.UsersByRole.SetValueExt<UsersInRoles.username>(newUser, user.Username);
                Base.UsersByRole.SetValueExt<UsersInRolesExt.usrESDirectAssigned>(newUser, false);
                Base.UsersByRole.Update(newUser);
            }
        }
    }
}