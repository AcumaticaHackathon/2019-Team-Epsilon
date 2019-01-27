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
    public class Access_Extension : PXGraphExtension<Access>
    {
        public void UsersInRoles_RowInserting(PXCache cache, PXRowInsertingEventArgs e)
        {
            UsersInRoles userInRoles = e.Row as UsersInRoles;
            if (userInRoles == null)
                return;
            else
            {
                 userInRoles.GetExtension<UsersInRolesExt>().UsrESDirectAssigned = true;
            }
        }
    }
}